using DataStructures.Main;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DataStructures.Tests
{
    public class MyUnionFindTests
    {
        public MyUnionFind Uf { get; set; }
        private const int SIZE = 5;

        public MyUnionFindTests()
        {
            Uf = new MyUnionFind(SIZE);
        }

        // Constructor

        [Fact]

        public void Test_constructor1 ()
        {
            Assert.Equal(SIZE, Uf.ComponentsNo);
        }

        [Fact]
        public void Test_constructor2()
        {
            Assert.Throws<ArgumentException>(() => new MyUnionFind(0));
            Assert.Throws<ArgumentException>(() => new MyUnionFind(-1));
            Assert.Throws<ArgumentException>(() => new MyUnionFind(-100));
        }

        // IsConnected
        [Fact]
        public void Test_is_connected()
        {
            for (int i = 0; i < SIZE; i++) Assert.True(Uf.IsConnected(i, i));

            Uf.Unify(0, 2);

            Assert.True(Uf.IsConnected(0, 2));
            Assert.True(Uf.IsConnected(2, 0));

            Assert.False(Uf.IsConnected(0, 1));
            Assert.False(Uf.IsConnected(3, 1));
            Assert.False(Uf.IsConnected(0, 4));
            Assert.False(Uf.IsConnected(4, 3));


            Uf.Unify(3, 1);

            Assert.True(Uf.IsConnected(0, 2));
            Assert.True(Uf.IsConnected(2, 0));
            Assert.True(Uf.IsConnected(1, 3));
            Assert.True(Uf.IsConnected(3, 1));

            Assert.False(Uf.IsConnected(0, 1));
            Assert.False(Uf.IsConnected(1, 2));
            Assert.False(Uf.IsConnected(2, 3));
            Assert.False(Uf.IsConnected(1, 0));
            Assert.False(Uf.IsConnected(2, 1));
            Assert.False(Uf.IsConnected(3, 2));
            Assert.False(Uf.IsConnected(0, 4));
            Assert.False(Uf.IsConnected(4, 3));




            Uf.Unify(2, 4);
            Assert.True(Uf.IsConnected(0, 2));
            Assert.True(Uf.IsConnected(2, 0));
            Assert.True(Uf.IsConnected(1, 3));
            Assert.True(Uf.IsConnected(3, 1));
            Assert.True(Uf.IsConnected(0, 4));
            Assert.True(Uf.IsConnected(4, 0));
            Assert.True(Uf.IsConnected(4, 2));
            Assert.True(Uf.IsConnected(2, 4));

            Assert.False(Uf.IsConnected(0, 1));
            Assert.False(Uf.IsConnected(1, 2));
            Assert.False(Uf.IsConnected(2, 3));
            Assert.False(Uf.IsConnected(1, 0));
            Assert.False(Uf.IsConnected(2, 1));
            Assert.False(Uf.IsConnected(3, 2));
            Assert.False(Uf.IsConnected(4, 3));

            // Connect everything
            Uf.Unify(1, 2);

            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    Assert.True(Uf.IsConnected(i, j));
                }
            }
        }

        // ComponentsNo

        [Fact]
        public void Test_components_no()
        {

            Assert.Equal(5, Uf.ComponentsNo);

            Uf.Unify(0, 1);
            Assert.Equal(4, Uf.ComponentsNo);

            Uf.Unify(1, 0);
            Assert.Equal(4, Uf.ComponentsNo);

            Uf.Unify(1, 2);
            Assert.Equal(3, Uf.ComponentsNo);

            Uf.Unify(0, 2);
            Assert.Equal(3, Uf.ComponentsNo);

            Uf.Unify(2, 1);
            Assert.Equal(3, Uf.ComponentsNo);

            Uf.Unify(3, 4);
            Assert.Equal(2, Uf.ComponentsNo);

            Uf.Unify(4, 3);
            Assert.Equal(2, Uf.ComponentsNo);

            Uf.Unify(1, 3);
            Assert.Equal(1, Uf.ComponentsNo);

            Uf.Unify(4, 0);
            Assert.Equal(1, Uf.ComponentsNo);
        }
        // GetSize
        [Fact]
        public void Test_get_size_at()
        {
            Assert.Equal(1, Uf.GetSizeAt(0));
            Assert.Equal(1, Uf.GetSizeAt(1));
            Assert.Equal(1, Uf.GetSizeAt(2));
            Assert.Equal(1, Uf.GetSizeAt(3));
            Assert.Equal(1, Uf.GetSizeAt(4));


            Uf.Unify(0, 1);
            Assert.Equal(2, Uf.GetSizeAt(0));
            Assert.Equal(0, Uf.GetSizeAt(1));
            Assert.Equal(1, Uf.GetSizeAt(2));
            Assert.Equal(1, Uf.GetSizeAt(3));
            Assert.Equal(1, Uf.GetSizeAt(4));

            Uf.Unify(1, 0);
            Assert.Equal(2, Uf.GetSizeAt(0));
            Assert.Equal(0, Uf.GetSizeAt(1));
            Assert.Equal(1, Uf.GetSizeAt(2));
            Assert.Equal(1, Uf.GetSizeAt(3));
            Assert.Equal(1, Uf.GetSizeAt(4));

            Uf.Unify(1, 2);
            Assert.Equal(3, Uf.GetSizeAt(0));
            Assert.Equal(0, Uf.GetSizeAt(1));
            Assert.Equal(0, Uf.GetSizeAt(2));
            Assert.Equal(1, Uf.GetSizeAt(3));
            Assert.Equal(1, Uf.GetSizeAt(4));

            Uf.Unify(0, 2);
            Assert.Equal(3, Uf.GetSizeAt(0));
            Assert.Equal(0, Uf.GetSizeAt(1));
            Assert.Equal(0, Uf.GetSizeAt(2));
            Assert.Equal(1, Uf.GetSizeAt(3));
            Assert.Equal(1, Uf.GetSizeAt(4));

            Uf.Unify(2, 1);
            Assert.Equal(3, Uf.GetSizeAt(0));
            Assert.Equal(0, Uf.GetSizeAt(1));
            Assert.Equal(0, Uf.GetSizeAt(2));
            Assert.Equal(1, Uf.GetSizeAt(3));
            Assert.Equal(1, Uf.GetSizeAt(4));

            Uf.Unify(3, 4);
            Assert.Equal(3, Uf.GetSizeAt(0));
            Assert.Equal(0, Uf.GetSizeAt(1));
            Assert.Equal(0, Uf.GetSizeAt(2));
            Assert.Equal(2, Uf.GetSizeAt(3));
            Assert.Equal(0, Uf.GetSizeAt(4));

            Uf.Unify(4, 3);
            Assert.Equal(3, Uf.GetSizeAt(0));
            Assert.Equal(0, Uf.GetSizeAt(1));
            Assert.Equal(0, Uf.GetSizeAt(2));
            Assert.Equal(2, Uf.GetSizeAt(3));
            Assert.Equal(0, Uf.GetSizeAt(4));

            Uf.Unify(1, 3);
            Assert.Equal(5, Uf.GetSizeAt(0));
            Assert.Equal(0, Uf.GetSizeAt(1));
            Assert.Equal(0, Uf.GetSizeAt(2));
            Assert.Equal(0, Uf.GetSizeAt(3));
            Assert.Equal(0, Uf.GetSizeAt(4));

            Uf.Unify(4, 0);
            Assert.Equal(5, Uf.GetSizeAt(0));
            Assert.Equal(0, Uf.GetSizeAt(1));
            Assert.Equal(0, Uf.GetSizeAt(2));
            Assert.Equal(0, Uf.GetSizeAt(3));
            Assert.Equal(0, Uf.GetSizeAt(4));
        }
    }
}
