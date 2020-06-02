using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Learning.DataStructures
{
    public class MyQueue<T>
    {
        private List<T> List { get; set; }
        public int Size { get => List.Count; }

        public T this[int i]
        {
            get
            {
                if (i < 0 || i >= Size)
                    throw new IndexOutOfRangeException();

                return List[i];
            }
            set
            {
                if (i < Size && i >= 0) List[i] = value;
                else throw new IndexOutOfRangeException();
            }
        }

        public MyQueue()
        {
            List = new List<T>();
        }

        public MyQueue(T elem) : this()
        {
            Enqueue(elem);
        }

        public void Enqueue(T elem) => List.Add(elem);

        // Get the element in the front of the queue without removing it
        public T Peek()
        {
            if (Size == 0) throw new Exception("The queue is already empty");
            return List.FirstOrDefault();
        }

        // Get the element on the front of the queue and remove it
        public T Dequeue()
        {
            T elem = Peek();
            if (elem != null)
            {
                List.Remove(elem);
            }

            return elem;
        }

        // Check if the queue is empty
        public bool IsEmpty() => Size == 0;

        // Get enumerator so that we can iterate over the queue
        public IEnumerator<T> GetEnumerator()
        {
            return List.GetEnumerator();
        }

    }
}
