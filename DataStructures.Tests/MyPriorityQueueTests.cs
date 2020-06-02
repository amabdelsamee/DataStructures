using DataStructures.Main;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DataStructures.Tests
{
    public class MyPriorityQueueTests
    {
        public MyPriorityQueue<int> Queue { get; set; }
        public int[] RandArr { get; set; }

        public MyPriorityQueueTests()
        {
            RandArr = GenerateRandArr();
        }

        // Constructor

        [Fact]

        public void Test_empty_constructor()
        {
            Queue = new MyPriorityQueue<int>();

            Assert.True(Queue.IsEmpty());
        }

        [Fact]
        public void Test_constructor1()
        {
            Queue = new MyPriorityQueue<int>();

            Assert.True(Queue.IsEmpty());
            Assert.Equal(10, Queue.HeapCapacity);

        }

        [Fact]
        public void Test_constructor2()
        {
            Queue = new MyPriorityQueue<int>(RandArr);

            Assert.False(Queue.IsEmpty());
            Assert.Equal(RandArr.Length, Queue.HeapCapacity);
            Assert.Equal(RandArr.Length, Queue.HeapSize);
        }

        // Add

        [Fact]
        public void Test_add()
        {
            Queue = new MyPriorityQueue<int>();

            for (int i = RandArr.Length-1; i >= 0 ;i--) Queue.Add(RandArr[i]);
            for (int i = 0; i < RandArr.Length; i++) Queue.Add(RandArr[i]);

            Assert.True(Queue.IsMinHeap(0));
            Assert.False(Queue.IsEmpty());
            Assert.Equal(2*RandArr.Length, Queue.HeapSize);

        }

        [Fact]
        public void Test_add_null()
        {
            MyPriorityQueue<string> queue = new MyPriorityQueue<string>();
            Assert.Throws<ArgumentNullException>(() => queue.Add(null));
        }

        // Remove

        [Fact]
        public void Test_remove()
        {
            Queue = new MyPriorityQueue<int>(RandArr);

            for (int i = 0; i < RandArr.Length; i++) Assert.True(Queue.Remove(i));

            Assert.True(Queue.IsEmpty());
        }

        [Fact]
        public void Test_remove_from_empty()
        {
            Queue = new MyPriorityQueue<int>();

            Assert.Throws<Exception>(() => Queue.Remove(1));
        }

        // Poll


        [Fact]
        public void Test_poll()
        {
            Queue = new MyPriorityQueue<int>();

            foreach (var e in RandArr) Queue.Add(e);

            while (!Queue.IsEmpty()) Queue.Poll();

            Assert.True(Queue.IsEmpty());
        }

        [Fact]
        public void Test_poll_from_empty()
        {
            Queue = new MyPriorityQueue<int>();

            Assert.Throws<Exception>(() => Queue.Poll());
        }

        // Contains

        [Fact]
        public void Test_contains()
        {
            MyPriorityQueue<char> queue = new MyPriorityQueue<char>();

            queue.Add('a');
            queue.Add('d');
            queue.Add('g');
            queue.Add('x');
            queue.Add('z');

            Assert.True(queue.Contains('a'));
            Assert.True(queue.Contains('z'));
            Assert.True(queue.Contains('g'));

            Assert.False(queue.Contains('r'));
            Assert.False(queue.Contains('u'));
            Assert.False(queue.Contains('i'));
        }


        // Peek

        [Fact]
        public void Test_peek()
        {
            Queue = new MyPriorityQueue<int>(RandArr);

            for (int i = 0; i < RandArr.Length ;i++)
            {
                Assert.Equal(Queue.Peek(), Queue.Poll());
            }

        }

        [Fact]
        public void Test_peek_from_empty()
        {
            Queue = new MyPriorityQueue<int>();

            Assert.Equal(default, Queue.Peek());
        }

        // Exhaustively


        [Fact]
        public void Test_exhaustively()
        {
            Queue = new MyPriorityQueue<int>();

            foreach (var e in RandArr) Queue.Add(e);

            Queue.IsMinHeap(0);

            Assert.Equal(RandArr.Length, Queue.HeapSize);


            for (int i = 0; i < RandArr.Length; i++) 
                Assert.Equal(Queue.Peek(), Queue.Poll());

            Assert.True(Queue.IsEmpty());

            Queue.Add(30);
            Queue.Add(20);
            Queue.Add(10);
            Queue.Add(100);
            Queue.Add(5);

            Assert.Equal(5, Queue.HeapSize);

            Assert.True(Queue.IsMinHeap(0));

            Assert.True(Queue.Contains(20));
            Assert.False(Queue.Contains(50));

            Assert.Equal(5, Queue.Poll());
            Assert.True(Queue.Remove(30));
            Assert.False(Queue.Remove(70));

            Assert.Equal(10, Queue.Peek());
            Assert.Equal(3, Queue.HeapSize);

            Assert.True(Queue.IsMinHeap(0));

            Queue.Remove(20);

            Assert.Equal(2, Queue.HeapSize);
            Assert.True(Queue.IsMinHeap(0));

            Assert.Equal(10, Queue.Peek());
            Assert.Equal(10, Queue.Poll());
            Assert.False(Queue.Remove(10));

            Assert.True(Queue.IsMinHeap(0));

            Assert.True(Queue.Remove(100));

            Assert.True(Queue.IsEmpty());

            foreach (var e in RandArr) Queue.Add(e);

            Queue.IsMinHeap(0);

            Queue.Clear();

            Assert.True(Queue.IsEmpty());

        }


        // Private

        private int[] GenerateRandArr()
        {
            var rand = new Random();
            int randLength = rand.Next(1,1000);
            int[] resultArr = new int[randLength];

            for (int i = 0; i < randLength; i++) resultArr[i] = i;

            return resultArr;
        } 
    }
}
