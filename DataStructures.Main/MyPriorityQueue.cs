using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DataStructures.Main
{
    public class MyPriorityQueue<T> where T : IComparable
    {
        public int HeapSize { get; private set; }
        public int HeapCapacity { get; private set; }
        private T[] Heap { get; set; }

        private const int DEFAULT_CAPACITY = 10;

        public MyPriorityQueue(int size)
        {
            HeapSize = 0;
            HeapCapacity = size;
            Heap = new T[HeapCapacity];
        }

        public MyPriorityQueue() : this(DEFAULT_CAPACITY)
        {}

        public MyPriorityQueue(T[] arr)
        {
            HeapCapacity = arr.Length;
            HeapSize = 0;
            Heap = new T[HeapCapacity];

            foreach (var e in arr) Add(e);
        }

        public void Add (T elem)
        {
            if (elem == null) throw new ArgumentNullException();

            if (HeapSize+1 > HeapCapacity) Resize();

            // add to last in arr
            Heap[HeapSize] = elem;
            HeapSize++;

            // bubble up
            BubbleUp(elem, HeapSize - 1);
        }

        private void Resize()
        {
            T[] newArr = new T[HeapCapacity*2];

            Array.Copy(Heap, newArr, HeapSize);

            Heap = newArr;
            HeapCapacity = Heap.Length;
        }

        public void BubbleUp(T elem, int index)
        {
            // check if elem is less than its parent
            // get parent index 
            int parentIndex = (index-1) / 2;
            T parent = Heap[parentIndex];

            if (parent.CompareTo(elem) > 0)
            {
                Swap(parentIndex, index);
                BubbleUp(elem, parentIndex);
            }
            var t = IsMinHeap(0);
        }
        
        public bool Remove(T elem)
        {
            if (IsEmpty()) throw new Exception("Queue is already empty");

            if (elem == null) return false;

            int index = IndexOf(elem);

            if (index == -1) return false;

            RemoveAt(index);

            return true;
        }

        private T RemoveAt(int index)
        {

            T result = Heap[index];
            // swap elem with last elem
            Swap(index, HeapSize-1);

            Heap[HeapSize-1] = default;
            HeapSize--;

            // bubble down swapped last element
            BubbleDown(Heap[index], index);

            return result;
        }

        private void BubbleDown(T elem, int index)
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;

            int smallest = left;

            if (right < HeapSize &&  Less(right, left)) smallest = right;

            if (left < HeapSize || (smallest < HeapSize && Less(smallest, index)))
            {
                Swap(index, smallest);
                BubbleDown(elem, smallest);
            }
        }

        public T Poll()
        {
            if (IsEmpty()) throw new Exception("Queue is already empty");
            return RemoveAt(0);
        }

        public T Peek()
        {
            if (IsEmpty()) return default;
            return Heap[0];
        }
        public void Clear()
        {
            for (int i = 0; i < HeapSize; i++) Heap[i] = default;
            HeapSize = 0;
        }
        private bool Less(int i, int j) => Heap[i].CompareTo(Heap[j]) < 0;
        private void Swap(int parentIndex, int index)
        {
            T temp = Heap[parentIndex];
            Heap[parentIndex] = Heap[index];
            Heap[index] = temp;
        }

        public bool IsEmpty() => HeapSize == 0;
        private int IndexOf(T elem) => Array.IndexOf(Heap, elem);

        public bool Contains (T elem)
        {
            for (int i = 0; i < HeapSize ;i++)
            {
                if (Heap[i].CompareTo(elem) == 0) return true;
            }

            return false;
        }

        // To make that sure the heap invariant (for testing purposes)
        // start from root (index=0)
        public bool IsMinHeap(int index)
        {
            int left = 2 * index + 1;
            int right = 2 * index + 2;

            if (index >= HeapSize) return true;

            if (right < HeapSize && Less(right, index)) 
                return false;
            if (left < HeapSize && Less(left, index)) 
                return false;

            return IsMinHeap(left) && IsMinHeap(right);
        }
    }
}
