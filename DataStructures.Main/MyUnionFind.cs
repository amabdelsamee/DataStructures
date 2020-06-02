using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructures.Main
{
    public class MyUnionFind
    {
        private int[] Size { get; set; }
        private int[] Id { get; set; }
        public int ComponentsNo { get; private set; }

        public MyUnionFind(int size)
        {
            if (size <= 0) throw new ArgumentException("Size must be over than zero");

            Size = new int[size];
            Id = new int[size];
            ComponentsNo = size;

            for (int i = 0; i < size ;i++)
            {
                Size[i] = 1;
                Id[i] = i;
            }
        }

        public int Find (int x)
        {
            int root = x;

            while (root != Id[root]) root = Id[root];

            return root;
        }

        // Compress all paths to be child to parent directly
        // Improves time complexity
        private void CompressPath()
        {
            for (int i = 0; i < Id.Length ;i++)
            {
                // Check if the parent is the real parent (no chaining)
                if (Id[Id[i]] != Id[i])
                    Id[i] = Find(i);
            }
        }

        public void Unify(int x, int y)
        {
            int rootX = Find(x);
            int rootY = Find(y);

            if (rootX != rootY)
            {
                if (Size[rootX] < Size[rootY])
                {
                    // x => y

                    Id[rootX] = rootY;
                    Size[rootX] = 0;
                    Size[rootY] += Size[rootX];
                }
                else
                {
                    // y => x
                    Id[rootY] = rootX;
                    Size[rootX] += Size[rootY];
                    Size[rootY] = 0;
                }

                ComponentsNo--;

                // Compress path

                CompressPath();
            }
        }

        public bool IsConnected(int x, int y) => Find(x) == Find(y);
        public int GetSizeAt(int i) => Size[i];
    }
}
