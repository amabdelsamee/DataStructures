using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace DataStructures.Main
{
    public class MyBST<T> where T : IComparable
    {
        public BSTNode<T> Root { get; set; }
        public int Size { get; private set; }

        public MyBST()
        {
            Root = null;
            Size = 0;
        }

        public bool IsEmpty() => Size == 0;

        public bool Contains(T elem)
        {
            if (Root == null) return false;
            var trav = Root;

            while (trav != null)
            {
                int compareFactor = trav.Data.CompareTo(elem);
                if (compareFactor == 0) return true;
                else if (compareFactor > 0) trav = trav.Left;
                else trav = trav.Right;
            }

            return false;
        }
        public bool Add (T elem)
        {
            if (elem == null) throw new ArgumentNullException();

            if (Contains(elem)) return false;

            Root = Add(Root, elem);
            Size++;

            return true;
        }

        private BSTNode<T> Add (BSTNode<T> node, T elem)
        {
            if (node != null)
            {
                if (elem.CompareTo(node.Data) > 0)
                    node.Right = Add(node.Right, elem);
                else if (elem.CompareTo(node.Data) < 0)
                    node.Left = Add(node.Left, elem);

                return node;
            }

            return new BSTNode<T>(elem, null, null);
        }

        public bool Remove(T elem)
        {
            if (IsEmpty()) throw new Exception("Tree is already empty");

            if (elem == null) throw new ArgumentNullException();

            // check if contains true
            if (!Contains(elem)) return false;

            // remove
            Root = Remove(Root, elem);

            Size--;
            return true;
        }

        private BSTNode<T> Remove(BSTNode<T> node, T elem)
        {
            if (node == null) return null;

            // if it's the target element
            if (node.Data.CompareTo(elem) == 0)
            {
                if (node.Left != null && node.Right == null)
                    // return left branch and skip current
                    return node.Left;
                else if (node.Left == null && node.Right != null)
                    // return right branch and skip current
                    return node.Right;
                else if (node.Left != null && node.Right != null)
                {
                    // get successor (smallest element in right branch)
                    var successor = GetSuccessor(node.Right);
                    // swap nodes
                    Swap(successor, node);
                    // remove the target element from right branch again
                    // recursion till the target node is being removed totally
                    node.Right = Remove(node.Right, elem);
                }
                else
                    return null;
            }
            else
            {
                // if it's the element is less than the current node
                // search in left branch

                if (node.Data.CompareTo(elem) > 0)
                    node.Left = Remove(node.Left, elem);
                // search in right branch
                else
                    node.Right = Remove(node.Right, elem);
            }

            return node;
        }

        private void Swap (BSTNode<T> n1, BSTNode<T> n2)
        {
            T tempData = n1.Data;
            n1.Data = n2.Data;
            n2.Data = tempData;
        }
        private BSTNode<T> GetSuccessor (BSTNode<T> node)
        {
            var trav = node;
            while (trav.Left != null) trav = trav.Left;

            return trav;
  
        }

        public int GetHeight()
        {
            if (IsEmpty()) return 0;

            return GetHeight(Root);
        }

        private int GetHeight (BSTNode<T> node)
        {
            if (node == null) return 0;

            return 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));

        }

        public MyBSTEnumerator<T> GetEnumerator() => new MyBSTEnumerator<T>(Root);

        // Testing method

        public bool IsVariant() => IsVariant(Root);
        private bool IsVariant (BSTNode<T> node)
        {
            if (node == null) return true;

            bool left = true, right = true;

            if (node.Left != null)
            {
                if ((node.Data.CompareTo(node.Left.Data) > 0))
                    left =  IsVariant(node.Left);
                else
                    left = false;
            }


            if (node.Right != null)
            {
                if ((node.Data.CompareTo(node.Right.Data) < 0))
                    right =  IsVariant(node.Right);
                else
                    right =  false;
            }

            return left && right;
        }
    }

    public class BSTNode <T>
    {
        public T Data { get; set; }
        public BSTNode<T> Left { get; set; }
        public BSTNode<T> Right { get; set; }

        public BSTNode(T data, BSTNode<T> left, BSTNode<T> right)
        {
            Data = data;
            Left = left;
            Right = right;
        }
    }
    public class MyBSTEnumerator<T> : IEnumerator<T>
    {
        public T Current => List[Position];

        object IEnumerator.Current => Current;

        private BSTNode<T> Node { get; set; }
        public List<T> List { get; set; }
        public int Position { get; set; }

        private readonly IntPtr _managedResources;
        private readonly SafeHandle _unmanagedResources;
        public MyBSTEnumerator(BSTNode<T> node)
        {
            Node = node;
            List = new List<T>();
            _unmanagedResources = new SafeFileHandle(new IntPtr(), true);
            _managedResources = Marshal.AllocHGlobal(sizeof(int));
            InOrderTraverse(Node);
            Position = -1;
        }


        public bool MoveNext()
        {
            if (++Position >= List.Count)
            {
                Reset();
                return false;
            }

            return true;
        }

        private void InOrderTraverse (BSTNode<T> node)
        {
            if (node != null)
            {
                InOrderTraverse(node.Left);
                List.Add(node.Data);
                InOrderTraverse(node.Right);
            }

        }
        public void Reset()
        {
            Position = -1;
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
