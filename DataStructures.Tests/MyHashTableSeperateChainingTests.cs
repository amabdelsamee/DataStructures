using DataStructures.Main;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DataStructures.Tests
{
    public class MyHashTableSeperateChainingTests
    {
        public MyHashTableSeperateChaining<int, int> Table { get; set; }

        public MyHashTableSeperateChainingTests()
        {
            Table = new MyHashTableSeperateChaining<int, int>();
        }

        // constructor

        [Fact]
        void Test_constructor_empty()
        {
            Assert.Equal(10, Table.Capacity);
            Assert.Equal(0.75, Table.MaxLoadFactor);
            Assert.Equal(Convert.ToInt32(10 * 0.75), Table.Threshold);
            Assert.True(Table.IsEmpty());
        }

        [Fact]
        void Test_constructor1()
        {
            Table = new MyHashTableSeperateChaining<int, int>(20);
            Assert.Equal(20, Table.Capacity);
            Assert.Equal(0.75, Table.MaxLoadFactor);
            Assert.Equal(Convert.ToInt32(20 * 0.75), Table.Threshold);
            Assert.True(Table.IsEmpty());

        }

        [Fact]
        void Test_constructor2()
        {
            Table = new MyHashTableSeperateChaining<int, int>(20,0.9);
            Assert.Equal(20, Table.Capacity);
            Assert.Equal(0.9, Table.MaxLoadFactor);
            Assert.Equal(Convert.ToInt32(20 * 0.9), Table.Threshold);
            Assert.True(Table.IsEmpty());
        }

        [Fact]
        void Test_constructor3()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new MyHashTableSeperateChaining<string, int>(-20, 0);
            });

            Assert.Throws<ArgumentOutOfRangeException>(()=>
            {
                new MyHashTableSeperateChaining<string, int>(20, -0.9);
            });

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new MyHashTableSeperateChaining<string, int>(-20, 0.9);
            });

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new MyHashTableSeperateChaining<string, int>(-20, -0.9);
            });

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new MyHashTableSeperateChaining<string, int>(20, 0);
            });

        }

        [Theory]
        [InlineData(5)]
        [InlineData(-5)]
        [InlineData(0)]
        [InlineData(10)]
        [InlineData(-10)]
        [InlineData(20)]
        [InlineData(-20)]

        // normalize index
        void Test_normalize_index(int key)
        {
            int index = Table.NormalizeIndex(key);

            Assert.InRange(index, 0, Table.Capacity-1);
        }

        // insert

        [Fact]
        void Test_insert_null_key()
        {
            MyHashTableSeperateChaining<string, string> tbl = new MyHashTableSeperateChaining<string, string>();
            Assert.Throws<ArgumentNullException>(() => tbl.Insert(null, "test"));
        }

        [Fact]
        void Test_insert()
        {
            Table.Insert(10, 5000);
            Table.Insert(100, 5000);

            Assert.False(Table.IsEmpty());
            Assert.Equal(2, Table.Size);

            Table.Insert(10, 6000);

            Assert.False(Table.IsEmpty());
            Assert.Equal(2, Table.Size);

            Table.Insert(50, 5000);

            Assert.False(Table.IsEmpty());
            Assert.Equal(3, Table.Size);

        }

        // clear

        [Fact]
        void Test_clear()
        {
            Table.Insert(10, 5000);
            Table.Insert(1000, 6000);
            Table.Insert(100, 5000);

            Assert.Equal(3, Table.Size);

            Table.Clear();

            Assert.True(Table.IsEmpty());
        }

        // Remove

        [Fact]
        void Test_remove_from_empty()
        {
            Assert.Throws<ArgumentException>(() => Table.Remove(5));
        }

        [Fact]
        void Test_remove_null()
        {
            MyHashTableSeperateChaining<string, int> table = new MyHashTableSeperateChaining<string, int>();
            table.Insert("a",1);
            table.Insert("b", 2);
            table.Insert("c", 3);

            Assert.Throws<ArgumentNullException>(() => table.Remove(null));

        }


        [Fact]
        void Test_remove_not_existing()
        {
            MyHashTableSeperateChaining<string, int> table = new MyHashTableSeperateChaining<string, int>();
            table.Insert("a", 1);
            table.Insert("b", 2);
            table.Insert("c", 3);

            Assert.Throws<KeyNotFoundException>(() => table.Remove("z"));

        }

        [Fact]
        void Test_remove()
        {
            MyHashTableSeperateChaining<string, int> table = new MyHashTableSeperateChaining<string, int>();
            table.Insert("a", 1);
            table.Insert("b", 2);
            table.Insert("c", 3);
            table.Insert("d", 4);


            Assert.Equal(2, table.Remove("b"));

            Assert.True(table.ContainsKey("a"));
            Assert.False(table.ContainsKey("b"));
            Assert.True(table.ContainsKey("c"));
            Assert.True(table.ContainsKey("d"));

            Assert.Equal(3, table.Remove("c"));

            Assert.True(table.ContainsKey("a"));
            Assert.False(table.ContainsKey("b"));
            Assert.False(table.ContainsKey("c"));
            Assert.True(table.ContainsKey("d"));

            Assert.Equal(1, table.Remove("a"));

            Assert.False(table.ContainsKey("a"));
            Assert.False(table.ContainsKey("b"));
            Assert.False(table.ContainsKey("c"));
            Assert.True(table.ContainsKey("d"));


            Assert.Equal(4, table.Remove("d"));

            Assert.False(table.ContainsKey("a"));
            Assert.False(table.ContainsKey("b"));
            Assert.False(table.ContainsKey("c"));
            Assert.False(table.ContainsKey("d"));


            Assert.True(table.IsEmpty());

        }

        // Keys

        [Fact]
        void Test_keys()
        {
            string[] expectedKeys = { "a", "b","c"};
            MyHashTableSeperateChaining<string, int> table = new MyHashTableSeperateChaining<string, int>();
            table.Insert("a", 1);
            table.Insert("b", 2);
            table.Insert("c", 3);

            var keys = table.Keys();

            Assert.Equal(3, keys.Count);
            keys.ForEach(k => {
                int index = Array.IndexOf(expectedKeys,k);
                Assert.NotEqual(-1, index);
                expectedKeys[index] = null;
            });

            foreach (var k in expectedKeys)
                Assert.Null(k);
        }

        [Fact]
        void Test_keys_empty()
        {
            var keys = Table.Keys();

            Assert.Empty(keys);

        }

        // Keys

        [Fact]
        void Test_values()
        {
            string[] expectedValues = { "first", "second", "third" };
            MyHashTableSeperateChaining<string, string> table = new MyHashTableSeperateChaining<string, string>();
            table.Insert("a", "first");
            table.Insert("b", "second");
            table.Insert("c", "third");

            var values = table.Values();

            Assert.Equal(3, values.Count);
            values.ForEach(v => {
                int index = Array.IndexOf(expectedValues, v);
                Assert.NotEqual(-1, index);
                expectedValues[index] = null;
            });

            foreach (var k in expectedValues)
                Assert.Null(k);
        }

        [Fact]
        void Test_values_empty()
        {
            var values = Table.Values();

            Assert.Empty(values);

        }


        // Exhaustively

        [Fact]
        void Test_exhaustively()
        {
            MyHashTableSeperateChaining<string, string> table = new MyHashTableSeperateChaining<string, string>();

            table.Insert("a", "1");
            table.Insert("b", "2");
            table.Insert("c", "3");

            Assert.Equal(3, table.Size);
            Assert.False(table.IsEmpty());

            table.Remove("a");

            Assert.False(table.ContainsKey("a"));
            Assert.True(table.ContainsKey("b"));
            Assert.True(table.ContainsKey("c"));

            Assert.Equal(2, table.Size);

            var keys = table.Keys();
            var values = table.Values();

            Assert.Equal(2, keys.Count);
            Assert.Equal(2, values.Count);

            table.Clear();

            Assert.True(table.IsEmpty());
            Assert.False(table.ContainsKey("a"));
            Assert.False(table.ContainsKey("b"));
            Assert.False(table.ContainsKey("c"));

            var keys2 = table.Keys();
            var values2 = table.Values();

            Assert.Empty(keys2);
            Assert.Empty(values2);


            table.Insert("h", "100");
            table.Insert("z", "255");
            table.Insert("z", "300");

            Assert.Equal(2, table.Size);
            Assert.False(table.IsEmpty());
            Assert.Equal("300", table.GetValue("z"));

            Assert.False(table.ContainsKey("a"));
            Assert.False(table.ContainsKey("b"));
            Assert.False(table.ContainsKey("c"));
            Assert.True(table.ContainsKey("h"));
            Assert.True(table.ContainsKey("z"));

            var keys3 = table.Keys();
            var values3 = table.Values();

            Assert.Equal(2, keys3.Count);
            Assert.Equal(2, values3.Count);


            table.Remove("z");

            Assert.Equal(1, table.Size);
            Assert.False(table.IsEmpty());

            Assert.True(table.ContainsKey("h"));
            Assert.False(table.ContainsKey("z"));

            table.Remove("h");


            Assert.True(table.IsEmpty());
            Assert.False(table.ContainsKey("h"));
            Assert.False(table.ContainsKey("z"));

            var keys4 = table.Keys();
            var values4 = table.Values();

            Assert.Empty(keys4);
            Assert.Empty(values4);

            Assert.Throws<ArgumentException>(() => table.Remove("z"));

        }

        // Get enumerator

        [Fact]
        void Test_get_enumerator()
        {
            string[] sortedKeys = { "b", "c", "a"};
            MyHashTableSeperateChaining<string, string> table = new MyHashTableSeperateChaining<string, string>();

            table.Insert("a", "1");
            table.Insert("b", "2");
            table.Insert("c", "3");

            List<string> keys = new List<string>();

            foreach (var k in table)
            {
                keys.Add(k);
                int index = Array.FindIndex(sortedKeys, s => k == s);
                sortedKeys[index] = null;
            }

            foreach (var s in sortedKeys)
                Assert.Null(s);

            Assert.Equal(3, keys.Count);


            table.Clear();
            keys.Clear();

            table.Insert("h", "1");
            table.Insert("km", "2");
            table.Insert("zy", "3");

            sortedKeys = new string[] { "h", "km", "zy"};

            foreach (var k in table)
            {
                keys.Add(k);
                int index = Array.FindIndex(sortedKeys, s => k == s);
                sortedKeys[index] = null;
            }

            foreach (var s in sortedKeys)
                Assert.Null(s);

            Assert.Equal(3, keys.Count);
        }

        [Fact]
        void Test_using_enumerator()
        {
            MyHashTableSeperateChaining<string, string> table = new MyHashTableSeperateChaining<string, string>();

            table.Insert("a", "1");
            table.Insert("b", "2");
            table.Insert("c", "3");

            using (var e = table.GetEnumerator())
                Assert.NotNull(e);
        }
    }
}
