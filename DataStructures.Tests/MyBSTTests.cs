using DataStructures.Main;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace DataStructures.Tests
{
    public class MyBSTTests
    {
        public MyBST<int> BST { get; set; }

        public int[] RandArr { get; set; }

        public MyBSTTests()
        {
            BST = new MyBST<int>();
            RandArr = GenerateRandArr();

        }

        // Constructor

        [Fact]
        public void Test_constructor()
        {
            Assert.Equal(0, BST.Size);
            Assert.Null(BST.Root);
        }

        // Add

        [Fact]
        public void Test_add()
        {
            foreach (var a in RandArr) Assert.True(BST.Add(a));

            Assert.True(BST.IsVariant());

            Assert.False(BST.IsEmpty());
            Assert.NotNull(BST.Root);
            Assert.Equal(RandArr.Length, BST.Size);

            foreach (var a in RandArr) Assert.True(BST.Contains(a));
        }


        [Fact]
        public void Test_add_null()
        {
            var bst = new MyBST<string>();
            Assert.Throws<ArgumentNullException>(() => bst.Add(null));
        }


        [Fact]
        public void Test_add_existed_element()
        {
            var bst = new MyBST<char>();
            bst.Add('a');
            bst.Add('z');
            bst.Add('b');

            Assert.False(bst.Add('a'));
            Assert.True(bst.Add('A'));

        }
        // Remove
        [Fact]
        public void Test_remove()
        {
            foreach (var a in RandArr) BST.Add(a);

            foreach (var a in RandArr) Assert.True(BST.Remove(a));

            Assert.True(BST.IsEmpty());
            Assert.Null(BST.Root);

        }

        [Fact]
        public void Test_remove_from_empty()
        {
            Assert.Throws<Exception>(() => BST.Remove(50));

        }

        [Fact]
        public void Test_remove_not_existed()
        {
            BST.Add(1000);
            BST.Add(200);
            BST.Add(10);

            Assert.False(BST.Remove(50));
        }

        [Fact]
        public void Test_remove_null()
        {
            var bst = new MyBST<string>();
            bst.Add("abc");
            bst.Add("cd");
            bst.Add("qw");

            Assert.Throws<ArgumentNullException>(() => bst.Remove(null));
        }

        // Exhaustively

        [Fact]
        public void Test_exhaustively()
        {
            foreach (var a in RandArr) Assert.True(BST.Add(a));

            Assert.True(BST.IsVariant());
            Assert.False(BST.IsEmpty());
            Assert.NotNull(BST.Root);
            Assert.Equal(RandArr.Length, BST.Size);

            foreach (var a in RandArr) Assert.True(BST.Contains(a));

            foreach (var a in RandArr) Assert.True(BST.Remove(a));

            Assert.True(BST.IsVariant());
            Assert.True(BST.IsEmpty());
            Assert.Null(BST.Root);


            for (int i = RandArr.Length-1;i >= 0 ;i--) Assert.True(BST.Add(RandArr[i]));

            Assert.True(BST.IsVariant());
            Assert.False(BST.IsEmpty());
            Assert.NotNull(BST.Root);
            Assert.Equal(RandArr.Length, BST.Size);


            foreach (var a in RandArr) Assert.True(BST.Contains(a));

            foreach (var a in RandArr) Assert.True(BST.Remove(a));

            Assert.True(BST.IsVariant());
            Assert.True(BST.IsEmpty());
            Assert.Null(BST.Root);


        }

        // Contains

        [Fact]
        public void Test_contains()
        {
            var bst = new MyBST<string>();
            bst.Add("abc");
            bst.Add("cd");
            bst.Add("qw");

            Assert.True(bst.Contains("cd"));
            Assert.False(bst.Contains("c"));
        }

        // Height

        [Fact]

        public void Test_height()
        {
            Assert.Equal(0, BST.GetHeight());

            for (int i = 0; i < 10; i++) BST.Add(i);
            Assert.Equal(10, BST.GetHeight());

            for (int i = 0; i < 10; i++) BST.Remove(i);

            BST.Add(10);
            Assert.Equal(1, BST.GetHeight());

            BST.Add(5);
            Assert.Equal(2, BST.GetHeight());

            BST.Add(100);
            Assert.Equal(2, BST.GetHeight());

            BST.Add(500);
            BST.Add(1);
            Assert.Equal(3, BST.GetHeight());

        }

        // InOrderTraverse

        [Fact]
        public void Test_in_order_traverse()
        {
            for (int i = RandArr.Length-1; i >= 0; i--) 
                BST.Add(RandArr[i]);

            int j = 0;
            foreach (var e in BST)
            {
                Assert.Equal(RandArr[j], e);
                j++;
            }

            int? last = null;
            foreach (var e in BST)
            {
                if (last != null)
                    Assert.True(e > last);

                last = e;
            }

        }

        // Private
        private int[] GenerateRandArr()
        {
            var rand = new Random();
            int randLength = rand.Next(1, 1000);
            int[] resultArr = new int[randLength];

            for (int i = 0; i < randLength; i++) resultArr[i] = i;

            return resultArr;
        }
    }
}
