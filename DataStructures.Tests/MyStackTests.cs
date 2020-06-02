using Learning.DataStructures;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DataStructures.Tests
{
    public class MyStackTests
    {
        public MyStack<int> Stack { get; set; }

        public MyStackTests()
        {
            Stack = new MyStack<int>();
        }

        // Constructor

        [Fact]
        public void Test_parameterless_constructor()
        {
            Assert.True(Stack.IsEmpty());
            Assert.Equal(0, Stack.Size);

        }

        [Fact]
        public void Test_parameterized_constructor()
        {
            Stack = new MyStack<int>(100);
            Assert.True(!Stack.IsEmpty());
            Assert.Equal(1, Stack.Size);
            Assert.Equal(100, Stack[0]);
        }


        // Indexer

        [Fact]
        public void Test_indexer_set_get()
        {
            int length = 1000000;

            for (int i = 0; i < length; i++) Stack.Push(1);

            for (int i = 0; i < length; i++) Stack[i] = i;

            for (int i = 0; i < length; i++) Assert.Equal(i, Stack[i]);
        }
        [Theory]
        [InlineData(100)]
        [InlineData(110)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Test_indexer_set_out_of_range(int index)
        {
            for (int i = 0; i < 100; i++) Stack.Push(1);

            Assert.Throws<IndexOutOfRangeException>(() => Stack[index] = 10);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(101)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Test_indexer_get_out_of_range(int index)
        {
            for (int i = 0; i < 100; i++) Stack.Push(1);

            Assert.Throws<IndexOutOfRangeException>(() => Stack[index]);
        }

        // Push

        [Theory]
        [InlineData(new int[] {1})]
        [InlineData(new int[] {1,2,3})]
        public void Test_push(int[] elements)
        {
            for (int i = 0; i < elements.Length; i++) Stack.Push(elements[i]);

            Assert.True(!Stack.IsEmpty());
            Assert.Equal(elements.Length, Stack.Size);

        }

        // Pop

        [Fact]
        public void Test_pop()
        {
            for (int i = 0; i < 10 ;i++) Stack.Push(i);

            for (int i = 9; i >= 0; i--) Assert.True(Stack.Pop() == i);

            Assert.Equal(0, Stack.Size);

        }

        [Fact]
        public void Test_pop_empty()
        {
            Assert.Throws<Exception>(() => Stack.Pop());
        }

        // Peak

        [Fact]
        public void Test_peek()
        {
            for (int i = 0; i < 10; i++) Stack.Push(i);

            Assert.Equal(9, Stack.Peek());

        }

        [Fact]
        public void Test_peek_empty()
        {
            Assert.Throws<Exception>(() => Stack.Peek());
        }

        // Exhaustively

        [Fact]
        public void Test_exaustively()
        {
            Assert.True(Stack.IsEmpty());
            Stack.Push(1);
            Assert.True(!Stack.IsEmpty());
            Stack.Push(2);
            Assert.Equal(2, Stack.Size);
            Assert.True(Stack.Peek() == 2);
            Assert.Equal(2, Stack.Size);
            Assert.True(Stack.Pop() == 2);
            Assert.Equal(1, Stack.Size);
            Assert.True(Stack.Peek() == 1);
            Assert.Equal(1, Stack.Size);
            Assert.True(Stack.Pop() == 1);
            Assert.Equal(0, Stack.Size);
            Assert.True(Stack.IsEmpty());
        }
    }
}
