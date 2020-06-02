using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Learning.DataStructures
{
    public class MyStack<T>
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

        // Create empty stack
        public MyStack()
        {
            List = new List<T>();
        }

        // Create stack and push the first element
        public MyStack(T elem) : this()
        {
            Push(elem);
        }

        // Add element to the top of the stack
        public void Push(T elem) => List.Add(elem);

        // Get the element on the top of the stack without removing it
        public T Peek()
        {
            if (Size == 0) throw new Exception("The stack is already empty");
            return List.LastOrDefault();
        }

        // Get the element on the top of the stack and remove it
        public T Pop() 
        {
            T elem = Peek();
            if (elem != null)
            {
                List.Remove(elem);
            }

            return elem;
        }

        // Check if the stack is empty
        public bool IsEmpty() => Size == 0;

        // Get enumerator so that we can iterate over the stack
        public IEnumerator<T> GetEnumerator()
        {
            return List.GetEnumerator();
        }

    }
}
