using DataStructures.Main;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DataStructures.Tests
{
    public class MyDoublyLinkedList
    {
        const int LOOPS = 1000;

        MyDoubyLinkedList<int> list;
        public MyDoublyLinkedList()
        {
            list = new MyDoubyLinkedList<int>();
        }
        // Constructor
        [Fact]
        public void Test_parameterless_constructor()
        {
            Assert.True(list.IsEmpty());
        }

        [Fact]
        public void Test_parameterized_constructor()
        {
            MyDoubyLinkedList<int> list = new MyDoubyLinkedList<int>(500);

            Assert.True(!list.IsEmpty());
            Assert.Equal(1, list.Size);
        }

        // Add

        [Fact]
        public void Test_add()
        {
            for (int i = 0; i < LOOPS; i++) list.Add(i);

            Assert.Equal(LOOPS, list.Size);
        }

        [Fact]
        public void Test_add_clear()
        {
            for (int i = 0; i < LOOPS; i++) list.Add(i);
            Assert.Equal(LOOPS, list.Size);

            list.Clear();

            for (int i = 0; i < 50; i++) list.Add(i);
            Assert.Equal(50, list.Size);

        }

        // Remove

        [Fact]
        public void Test_remove()
        {
            for (int i = 0; i < LOOPS; i++) list.Add(i);

            for (int i = 0; i < LOOPS; i++)
            {
                list.Remove(i);
                Assert.Equal(LOOPS-i-1, list.Size);
            }

            Assert.True(list.IsEmpty());

        }

        [Fact]
        public void Test_remove_from_empty()
        {
            Assert.True(!list.Remove(2));
        }

        [Fact]
        public void Test_remove_at()
        {
            for (int i = 0; i < LOOPS; i++) list.Add(1000);

            for (int i = 0; i < LOOPS; i++)
            {
                Assert.Equal(1000, list.RemoveAt(0));
                Assert.Equal(LOOPS - i - 1, list.Size);
            }

            Assert.True(list.IsEmpty());

            list.Add(10);
            list.Add(20);
            list.Add(30);
            list.RemoveAt(2);
            list.RemoveAt(1);
            list.RemoveAt(0);

            Assert.True(list.IsEmpty());
        }


        [Fact]
        public void Test_remove_at_from_empty()
        {
            Assert.Throws<Exception>(() => list.RemoveAt(2));
        }

        [Fact]
        public void Test_remove_first()
        {
            list.Add(1);
            list.Add(2);
            list.Add(3);

            Assert.True(list.RemoveFirst());
            Assert.Equal(2, list.Size);
            Assert.Equal(2, list[0]);
        }

        [Fact]
        public void Test_remove_first_from_empty()
        {
            Assert.Throws<Exception>(() => list.RemoveFirst());
        }

        [Fact]
        public void Test_remove_last()
        {
            list.Add(1);
            list.Add(2);
            list.Add(3);

            Assert.True(list.RemoveLast());
            Assert.Equal(2, list.Size);
            Assert.Equal(2, list[1]);
        }

        [Fact]
        public void Test_remove_last_from_empty()
        {
            Assert.Throws<Exception>(()=> list.RemoveLast());
        }


        // Peek

        [Fact]
        public void Test_peek_first()
        {
            list.Add(1);
            list.Add(2);
            list.Add(3);

            Assert.Equal(1, list.PeekFirst());
        }

        [Fact]
        public void Test_peek_first_from_empty()
        {
            Assert.Throws<Exception>(() => list.PeekFirst());
        }
        [Fact]
        public void Test_peek_last()
        {
            list.Add(1);
            list.Add(2);
            list.Add(3);

            Assert.Equal(3, list.PeekLast());
        }

        [Fact]
        public void Test_peek_last_from_empty()
        {
            Assert.Throws<Exception>(() => list.PeekLast());
        }


        // Indexer

        [Fact]
         public void Test_indexer_get()
        {
            for (int i = 0; i < LOOPS; i++) list.Add(i);

            for (int i = 0; i < list.Size; i++) Assert.Equal(i, list[i]);
        }

        // IndexOf

        [Fact]
        public void Test_index_of()
        {
            for (int i = 0; i < LOOPS; i++) list.Add(i);

            for (int i = 0; i < LOOPS; i++) Assert.True(list.IndexOf(i) != -1);
        }

        [Fact]
        public void Test_index_of_not_exist()
        {
            list.Add(1);
            list.Add(2);
            list.Add(3);

            Assert.True(list.IndexOf(4) == -1);
        }


        // Clear

        [Fact]
        public void Test_clear()
        {
            for (int i = 0; i < LOOPS; i++) list.Add(i);
            list.Clear();
            Assert.True(list.IsEmpty());

            for (int i = 0; i < 50; i++) list.Add(i);
            list.Clear();
            Assert.True(list.IsEmpty());
        }


        // Get Enumerator

        [Fact]
        public void Test_get_enumerator()
        {
            int[] elements = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            foreach (var e in elements) list.Add(e);

            int size = 0;
            foreach (var a in list)
            {
                size++;
                Assert.True(Array.IndexOf(elements, a) != -1);
            }

            Assert.Equal(elements.Length, size);

        }


        [Fact]
        public void Test_using_enumerator()
        {
            int[] elements = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            foreach (var e in elements) list.Add(e);

            using (var e = list.GetEnumerator())
                Assert.NotNull(e);

        }

    }
}
