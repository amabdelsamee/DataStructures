using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Learning.DataStructures
{
    public class MyDynamicArray <T>
    {
        public int Size { get; private set; }
        public int Capacity { get => Arr.Length; }
        private int InitialCapacity { get; set; }
        private T[] Arr { get; set; }
        public T this[int i]
        {
            get
            {
                if (i < 0 || i >= Size)
                    throw new IndexOutOfRangeException();

                return Arr[i];
            }
            set
            {
                if (i < Size && i >= 0) Arr[i] = value;
                else throw new IndexOutOfRangeException();
            }
        }
        public MyDynamicArray(int capacity)
        {
            if (capacity < 0) 
                throw new ArgumentException("Capacity must be greater than or equal to zero");

            Arr = new T[capacity];
            InitialCapacity = capacity;
            Size = 0;
        }
        public MyDynamicArray(): this(20)
        { }
        public void Add(T elem)
        {
            // Check if the capacity of the array is full, we need to resize it
            if (Size + 1 > Capacity)
                Resize(2);
            Size++;
            Arr[Size - 1] = elem;
        }
        public T RemoveAt (int i)
        {
            if (i < 0 || i >= Size)
                throw new ArgumentOutOfRangeException();

            T elem = Arr[i];

            if (Size-1 < (Capacity / 2) && (Capacity >= InitialCapacity*2)) Resize(0.5);

            T[] newArr = new T[Capacity];

            for (int j = 0, k = 0; j < Size ;j++,k++)
            {
                if (j != i)  
                    newArr[k] = Arr[j];
                else --k;
            }

            Arr = newArr;
            Size--;
            return elem;
        }
        public bool Remove (T elem)
        {
            int index = IndexOf(elem);
            if (index == -1) return false;

            RemoveAt(index);

            return true;
        }

        // Create new array with new capacity
        // Clone the elements of original array to it
        private void Resize(double factor)
        {
            T[] newArr = new T[(int) Math.Ceiling(Capacity * factor)];
            Array.Copy(Arr, newArr, Size);
            Arr = newArr;
        }
        public bool IsEmpty() => Size == 0;
        public void Clear()
        {
            Array.Clear(Arr, 0, Size);
            Size = 0;
        }
        public int IndexOf (T elem) => Array.IndexOf(Arr, elem);

        // If IndexOf returns -1 then the element not exist
        public bool Contains(T elem) => IndexOf(elem) != -1;

        // Get enumerator for iterating over the array
        public IEnumerator<T> GetEnumerator()
        {
            return new MyDynamicArrayEnumerator<T>(Arr, Size);
        }
    }

    // An enumerator to iterate over the dynamic array
    public class MyDynamicArrayEnumerator <T> : IEnumerator<T>
    {
        private readonly int _size;

        private readonly T[] _arr;
        public T Current => _arr[_position];
        object IEnumerator.Current => Current;

        private int _position;
        private readonly IntPtr _managedResources;
        private readonly SafeHandle _unmanagedResources;

        public MyDynamicArrayEnumerator(T[] arr, int size)
        {
            _size = size;
            _arr = arr;
            _position = -1;
            _unmanagedResources = new SafeFileHandle(new IntPtr(), true);
            _managedResources = Marshal.AllocHGlobal(sizeof(int));
        }

        public bool MoveNext()
        {
            if (_position < _size - 1)
            {
                _position++;
                return true;
            }
            Reset();
            return false;
        }
        public void Reset()
        {
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
