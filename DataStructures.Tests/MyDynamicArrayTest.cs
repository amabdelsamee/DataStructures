using Learning.DataStructures;
using System;
using System.Linq;
using Xunit;

namespace DataStructures.Tests
{
    public class MyDynamicArrayTest
    {
        // Constructor

        [Fact]
        public void Test_parameterless_constructor()
        {
            int expectedCapacity = 20;
            int expectedSize = 0;

            MyDynamicArray<int> arr = new MyDynamicArray<int>();

            int actualCapacity = arr.Capacity;
            int actualSize = arr.Size;

            // assert

            Assert.Equal(expectedCapacity, actualCapacity);
            Assert.Equal(expectedSize, actualSize);
            Assert.True(arr.IsEmpty());
        }
        [Fact]
        public void Test_parameterized_constructor1()
        {
            int expectedCapacity = 100;
            int expectedSize = 0;


            MyDynamicArray<int> arr = new MyDynamicArray<int>(expectedCapacity);

            int actualSize = arr.Size;
            int actualCapacity = arr.Capacity;

            // assert

            Assert.Equal(expectedCapacity, actualCapacity);
            Assert.Equal(expectedSize, actualSize);
            Assert.True(arr.IsEmpty());
        }
        [Fact]
        public void Test_parameterized_constructor2()
        {
            // assert

            Assert.Throws<ArgumentException>(() => new MyDynamicArray<int>(-1));
        }

        // Add

        [Fact]
        public void Test_add()
        {
            int[] elements = {1,2,3,4,5,6,7,8,9,10 };

            MyDynamicArray<int> arr = new MyDynamicArray<int>();

            foreach (var e in elements) arr.Add(e);

            // assert

            for (int i = 0; i < arr.Size; i++) Assert.Equal(elements[i], arr[i]);
        }
        [Fact]
        public void Test_add_and_remove()
        {

            MyDynamicArray<int> arr = new MyDynamicArray<int>();

            for (int i = 0; i < 100; i++) arr.Add(i);
            for (int i = 0; i < 100; i++) arr.Remove(i);

            Assert.True(arr.IsEmpty());


            for (int i = 0; i < 1000; i++) arr.Add(i);
            for (int i = 0; i < 1000; i++) arr.Remove(i);

            Assert.True(arr.IsEmpty());


            for (int i = 0; i < 10000; i++) arr.Add(i);
            for (int i = 0; i < 10000; i++) arr.Remove(i);

            Assert.True(arr.IsEmpty());

        }
        [Fact]
        public void Test_add_and_clear()
        {

            MyDynamicArray<int> arr = new MyDynamicArray<int>();

            for (int i = 0; i < 100; i++) arr.Add(i);
            arr.Clear();

            Assert.True(arr.IsEmpty());

            for (int i = 0; i < 1000; i++) arr.Add(i);
            arr.Clear();

            Assert.True(arr.IsEmpty());

            for (int i = 0; i < 10000; i++) arr.Add(i);
            arr.Clear();

            Assert.True(arr.IsEmpty());

        }


        // Resize

        [Theory]
        [InlineData(10,11)]
        [InlineData(10, 19)]
        [InlineData(10, 20)]
        [InlineData(1000, 1001)]
        public void Test_resize_add(int originalCapacity,int elementsLength)
        {
            MyDynamicArray<int> arr = new MyDynamicArray<int>(originalCapacity);
            FillDynamicArray(arr, elementsLength, 1);
            // assert

            Assert.Equal(2* originalCapacity, arr.Capacity);
            Assert.Equal(elementsLength, arr.Size);


            for (int i = 0; i < elementsLength; i++) Assert.Equal(1,arr[i]);
        }
        [Fact]
        public void Test_resize_remove()
        {
            int initialCapacity = 50;
            MyDynamicArray<int> arr = new MyDynamicArray<int>(initialCapacity);
            FillDynamicArray(arr, initialCapacity*5, 1);

            for (int i = 0; i < initialCapacity * 5; i++) arr.Remove(1);

            // assert
            Assert.Equal(0, arr.Size);
            Assert.Equal(initialCapacity, arr.Capacity);
        }

        // Remove

        [Fact]
        public void Test_removing_at_empty()
        {

            MyDynamicArray<int> arr = new MyDynamicArray<int>();

            // assert
            Assert.Throws<ArgumentOutOfRangeException>(() => arr.RemoveAt(0));
        }
        [Fact]
        public void Test_remove_at1()
        {

            MyDynamicArray<int> arr = new MyDynamicArray<int>();
            FillDynamicArray(arr,3, 1);

            // assert
            Assert.Throws<ArgumentOutOfRangeException>(() => arr.RemoveAt(3));
        }
        [Fact]
        public void Test_remove_at2()
        {

            MyDynamicArray<int> arr = new MyDynamicArray<int>();            
            FillDynamicArray(arr, 1000, 1);

            // assert
            Assert.Throws<ArgumentOutOfRangeException>(() => arr.RemoveAt(-1));
        }
        [Fact]
        public void Test_remove_at3()
        {

            MyDynamicArray<int> arr = new MyDynamicArray<int>();
            FillDynamicArray(arr, 1000, 1);

            // assert
            Assert.Throws<ArgumentOutOfRangeException>(() => arr.RemoveAt(1500));
        }
        [Fact]
        public void Test_remove_at4()
        {

            MyDynamicArray<int> arr = new MyDynamicArray<int>();
            FillDynamicArray(arr, 1000, 1);

            // assert
            Assert.Throws<ArgumentOutOfRangeException>(() => arr.RemoveAt(-500));
        }

        // Clear

        [Fact]
        public void Test_clear()
        {
            MyDynamicArray<int> arr = new MyDynamicArray<int>();

            FillDynamicArray(arr, 1000, 100);
            arr.Clear();

            // assert

            Assert.True(arr.IsEmpty());
        }


        // Indexer

        [Fact]
        public void Test_indexer_set_get()
        {
            MyDynamicArray<int> arr = new MyDynamicArray<int>();
            int length = 1000000;

            FillDynamicArray(arr, length, 1);

            for (int i = 0; i < length; i++) arr[i] = i;

            for (int i = 0; i < length; i++) Assert.Equal(i, arr[i]);
        }
        [Theory]
        [InlineData(100)]
        [InlineData(110)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Test_indexer_set_out_of_range(int index)
        {
            MyDynamicArray<int> arr = new MyDynamicArray<int>();

            FillDynamicArray(arr, 100, 1);

            Assert.Throws<IndexOutOfRangeException>(() => arr[index] = 10);
        }

        [Theory]
        [InlineData(100)]
        [InlineData(101)]
        [InlineData(-1)]
        [InlineData(-100)]
        public void Test_indexer_get_out_of_range(int index)
        {
            MyDynamicArray<int> arr = new MyDynamicArray<int>();

            FillDynamicArray(arr, 100, 1);

            Assert.Throws<IndexOutOfRangeException>(() => arr[index]);
        }

        // Get Enumerator

        [Fact]
        public void Test_get_enumerator()
        {
            int[] elements = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            MyDynamicArray<int> arr = new MyDynamicArray<int>(100);


            foreach (var e in elements) arr.Add(e);

            int size = 0;
            foreach (var a in arr)
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
            MyDynamicArray<int> arr = new MyDynamicArray<int>(100);

            using (var e = arr.GetEnumerator())
                Assert.NotNull(e);

        }

        // Private helper methods for testing
        private void FillDynamicArray<T>(MyDynamicArray<T> arr, int length, T val)
        {
            for (int i = 0; i < length; i++) arr.Add(val);
        }
    }
}
