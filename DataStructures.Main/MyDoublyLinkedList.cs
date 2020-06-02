using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DataStructures.Main
{
    public class MyDoubyLinkedList<T> where T : IComparable<T>
    {
        public int Size { get; private set; }
        private Node<T> Head { get; set; }
        private Node<T> Tail { get; set; }

        public T this[int i]
        {
            get
            {
                if (i < 0 || i >= Size)
                    throw new IndexOutOfRangeException();

                return GetAt(i);
            }
            set
            {
                if (i < Size && i >= 0) SetAt(i, value);
                else throw new IndexOutOfRangeException();
            }
        }

        // Constructor

        public MyDoubyLinkedList()
        {
            Head = Tail = null;
            Size = 0;
        }

        public MyDoubyLinkedList(T data)
        {
            Head = Tail = new Node<T>(data, null, null);
            Size = 1;
        }

        // Add 
        // Add element to the tail
        public void Add(T data)
        {
            Node<T> node = new Node<T>(data, Tail, null);

            if (!IsEmpty()) Tail.Next = node;
            else Head = node;
            Tail = node;

            Size++;
        }

        public bool Remove(T elem)
        {
            int index = IndexOf(elem);

            if (index == -1) return false;

            RemoveAt(index);

            return true;
        }

        public bool RemoveFirst()
        {
            if (IsEmpty()) throw new Exception("You cannot remove from an empty list");

            RemoveAt(0);

            return true;
        }

        public bool RemoveLast()
        {
            if (IsEmpty()) throw new Exception("You cannot remove from an empty list");

            RemoveAt(Size-1);

            return true;
        }

        public T RemoveAt(int i)
        {
            if (IsEmpty()) throw new Exception("You cannot remove from an empty list");

            if (i >= Size || i < 0) throw new IndexOutOfRangeException();

            var trav = TraverseTo(i);

            // Skip node

            if (trav.Prev != null)
                trav.Prev.Next = trav.Next;
            // first node case
            else
                Head = trav.Next;

            if (trav.Next != null)
                trav.Next.Prev = trav.Prev;
            // Last node case
            else
                Tail = trav.Prev;

            T data = trav.Data;

            trav.Prev = null;
            trav.Next = null;

            Size--;

            return data;
        }
        // Peek

        public T PeekFirst()
        {
            if (IsEmpty()) throw new Exception("You cannot peek from an empty list");

            return Head.Data;
        }

        public T PeekLast()
        {
            if (IsEmpty()) throw new Exception("You cannot peek from an empty list");

            return Tail.Data;
        }

        public bool IsEmpty() => Size == 0;
        public int IndexOf (T elem)
        {
            if (IsEmpty()) return -1;

            int counter = 0;
            Node<T> trav = Head;

            while (trav != null)
            {
                if (trav.Data.CompareTo(elem) == 0) return counter;
                trav = trav.Next;
            }

            return -1;
        }
        public bool Contains (T elem) => IndexOf(elem) != -1;
        public void Clear()
        {
            Node<T> trav = Head;
            while (trav != null)
            {
                var next = trav.Next;
                trav.Next = trav.Prev = null;
                trav = next;

            }
            Head = Tail = trav = null;
            Size = 0;
        }

        public MyDoubyLinkedListEnumerator<T> GetEnumerator() => new MyDoubyLinkedListEnumerator<T>(Head);

        // Private methods
        private Node<T> TraverseTo(int index)
        {
            Node<T> trav = Head;
            for (int i = 0; i <= index; i++)
            {
                if (i > 0) trav = trav.Next;
            }

            return trav;
        }
        private T GetAt(int index) => TraverseTo(index).Data;
        private void SetAt(int index, T data)
        {
            Node<T> trav = TraverseTo(index);
            trav.Data = data;
        }

    }

    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T> Prev { get; set; }
        public Node<T> Next { get; set; }

        public Node(T data, Node<T> prev, Node<T> next)
        {
            Data = data;
            Prev = prev;
            Next = next;
        }

    }

    public class MyDoubyLinkedListEnumerator<T> : IEnumerator<T>
    {
        private Node<T> _node;
        public T Current => _node.Data;
        object IEnumerator.Current => Current;

        private int _position;
        private readonly IntPtr _managedResources;
        private readonly SafeHandle _unmanagedResources;

        public MyDoubyLinkedListEnumerator(Node<T> node)
        {
            _node = node;
            _position = -1;
            _unmanagedResources = new SafeFileHandle(new IntPtr(), true);
            _managedResources = Marshal.AllocHGlobal(sizeof(int));
        }

        public bool MoveNext()
        {
            if (_position == -1)
            {
                _position++;
                return true;
            }
            else if (_node.Next != null)
            {
                _node = _node.Next;
                _position++;
                return true;
            }

            Reset();
            return false;
        }

        public void Reset()
        {
            while (_node.Prev != null) _node = _node.Prev;
            _position = -1;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool isDisposing)
        {
            ReleaseUnmanagedResources(_unmanagedResources);
            if (isDisposing) ReleaseManagedResources(_managedResources);
        }
        private void ReleaseUnmanagedResources(SafeHandle safeHandle)
        {
            if (safeHandle != null) safeHandle.Dispose();
        }
        private void ReleaseManagedResources(IntPtr intPtr)
        {
            Marshal.FreeHGlobal(intPtr);
        }
    }
}
