using Learning.DataStructures;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DataStructures.Tests
{
    public class MyQueueTests
    {
        public MyQueue<int> Queue { get; set; }

        public MyQueueTests()
        {
            Queue = new MyQueue<int>();

        }
        // Constructor

        [Fact]
        public void Test_parameterless_constructor()
        {
            Assert.True(Queue.IsEmpty());
            Assert.Equal(0, Queue.Size);

        }

        [Fact]
        public void Test_parameterized_constructor()
        {
            MyStack<int> Queue = new MyStack<int>(100);
            Assert.True(!Queue.IsEmpty());
            Assert.Equal(1, Queue.Size);
            Assert.Equal(100, Queue[0]);

        }


        // Indexer

        [Fact]
        public void Test_indexer_set_get()
        {
            int length = 1000000;

            for (int i = 0; i < length; i++) Queue.Enqueue(1);

            for (int i = 0; i < length; i++) Queue[i] = i;

            for (int i = 0; i < length; i++) Assert.Equal(i, Queue[i]);
        }
        [Theory]
        [InlineData(100)]
        [InlineData(110)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Test_indexer_set_out_of_range(int index)
        {
            for (int i = 0; i < 100; i++) Queue.Enqueue(1);

            Assert.Throws<IndexOutOfRangeException>(() => Queue[index] = 10);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(101)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Test_indexer_get_out_of_range(int index)
        {
            for (int i = 0; i < 100; i++) Queue.Enqueue(1);

            Assert.Throws<IndexOutOfRangeException>(() => Queue[index]);
        }

        // Enqueue

        [Theory]
        [InlineData(new int[] { 1 })]
        [InlineData(new int[] { 1, 2, 3 })]
        public void Test_enqueue(int[] elements)
        {
            for (int i = 0; i < elements.Length; i++) Queue.Enqueue(elements[i]);

            Assert.True(!Queue.IsEmpty());
            Assert.Equal(elements.Length, Queue.Size);

        }

        // Dequeue

        [Fact]
        public void Test_dequeue()
        {
            for (int i = 0; i < 10; i++) Queue.Enqueue(i);

            for (int i = 0; i  < 10; i++) Assert.True(Queue.Dequeue() == i);

            Assert.Equal(0, Queue.Size);

        }

        [Fact]
        public void Test_dequeue_empty()
        {
            Assert.Throws<Exception>(() => Queue.Dequeue());
        }

        // Peak

        [Fact]
        public void Test_peek()
        {
            for (int i = 0; i < 10; i++) Queue.Enqueue(i);

            Assert.Equal(0, Queue.Peek());

        }

        [Fact]
        public void Test_peek_empty()
        {
            Assert.Throws<Exception>(() => Queue.Peek());
        }

        // Exhaustively

        [Fact]
        public void Test_exaustively()
        {
            Assert.True(Queue.IsEmpty());
            Queue.Enqueue(1);
            Assert.True(!Queue.IsEmpty());
            Queue.Enqueue(2);
            Assert.Equal(2, Queue.Size);
            Assert.True(Queue.Peek() == 1);
            Assert.Equal(2, Queue.Size);
            Assert.True(Queue.Dequeue() == 1);
            Assert.Equal(1, Queue.Size);
            Assert.True(Queue.Peek() == 2);
            Assert.Equal(1, Queue.Size);
            Assert.True(Queue.Dequeue() == 2);
            Assert.Equal(0, Queue.Size);
            Assert.True(Queue.IsEmpty());
        }
    }
}
