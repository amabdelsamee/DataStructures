using Microsoft.Win32.SafeHandles;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace DataStructures.Main
{
    public class MyHashTableSeperateChaining<K, V>
    {
        

        private const int DEFAULT_CAPACITY = 10;
        private const double DEFAULT_LOAD_FACTOR = 0.75;
        public int Capacity { get; private set; }
        public int Size { get; private set; }
        public double MaxLoadFactor { get; private set; }
        public int Threshold { get; private set; }
        private List<Entry<K,V>>[] _table;

        public MyHashTableSeperateChaining() : this(DEFAULT_CAPACITY, DEFAULT_LOAD_FACTOR)
        { }
        public MyHashTableSeperateChaining(int capacity) : this(capacity, DEFAULT_LOAD_FACTOR)
        { }
        public MyHashTableSeperateChaining(int capacity, double maxLoadFactor)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException("Capacity");

            if (maxLoadFactor <= 0)
                throw new ArgumentOutOfRangeException("Max Load Factor");

            Capacity = capacity;
            MaxLoadFactor = maxLoadFactor;
            Threshold = Convert.ToInt32(Capacity * MaxLoadFactor);
            _table = new List<Entry<K,V>>[Capacity];
        }

        public int NormalizeIndex(int hashKey) => (hashKey & 0x7FFFFFFF) % Capacity;
        public bool IsEmpty() => Size == 0;

        public void Clear()
        {
            Array.Fill(_table, null);
            Size = 0;
        }

        public bool ContainsKey(K key)
        {
            if (key == null)
                throw new ArgumentNullException();

            int bucketIndex = NormalizeIndex(key.GetHashCode());
            return BucketSeekEntry(bucketIndex, key) != null;
        }

        public bool Insert(K key, V val)
        {
            if (key == null)
                throw new ArgumentNullException();

            var entry = new Entry<K, V>(key, val);
            int bucketIndex = NormalizeIndex(key.GetHashCode());
            return BucketInsertEntry(bucketIndex, entry);
        }

        private bool BucketInsertEntry(int bucketIndex, Entry<K,V> entry)
        {
            var bucket = _table[bucketIndex];

            if (bucket == null)
                _table[bucketIndex] = bucket = new List<Entry<K, V>>();

            var existingEntry = BucketSeekEntry(bucketIndex, entry.Key);

            if (existingEntry == null)
            {
                bucket.Add(entry);

                if (Size++ > Threshold)
                    ResizeTable();

                return true;
            }

            existingEntry.Value = entry.Value;
            return false;
        }

        private void ResizeTable()
        {
            Capacity *= 2;
            Threshold = Convert.ToInt32(MaxLoadFactor * Capacity);

            var newTable = new List<Entry<K, V>>[Capacity];
            Array.Copy(_table, newTable, _table.Length);
            var oldTable = _table;
            _table = newTable;
            Array.Clear(oldTable, 0, oldTable.Length);
            oldTable = null;
        }

        private Entry<K, V> BucketSeekEntry(int bucketIndex, K key)
        {
            if (key == null) return null;
            var bucket = _table[bucketIndex];
            if (bucket != null)
            {
                foreach (var e in bucket)
                {
                    if (e.Key.Equals(key))
                        return e;
                }
            }

            return null;
        }

        public V GetValue(K key)
        {
            if (key == null)
                throw new ArgumentNullException();

            int bucketIndex = NormalizeIndex(key.GetHashCode());
            var entry = BucketSeekEntry(bucketIndex, key);

            if (entry != null)
                return entry.Value;

            throw new KeyNotFoundException();
        }

        public V Remove(K key)
        {
            if (key == null)
                throw new ArgumentNullException();

            if (IsEmpty())
                throw new ArgumentException("The table is already empty");

            int bucketIndex = NormalizeIndex(key.GetHashCode());
            return BucketRemoveEntry(bucketIndex, key);

        }

        private V BucketRemoveEntry(int bucketIndex, K key)
        {
            var entry = BucketSeekEntry(bucketIndex,key);

            if (entry == null)
                throw new KeyNotFoundException();

            _table[bucketIndex].Remove(entry);
            Size--;
            return entry.Value;
        }

        public List<K> Keys()
        {
            List<K> keys = new List<K>();
            foreach (var b in _table)
            {
                if (b != null)
                    b.ForEach(e => keys.Add(e.Key));
            }

            return keys;
        }

        public List<V> Values()
        {
            List<V> values = new List<V>();
            foreach (var b in _table)
            {
                if (b != null)
                    b.ForEach(e => values.Add(e.Value));
            }

            return values;
        }

        public MyHashTableSeperateChainingEnumerator<K,V> GetEnumerator()
        {
            return new MyHashTableSeperateChainingEnumerator<K, V>(_table);
        }
    }

    public class Entry<K, V>
    {
        public int Hash;
        public K Key { get; set; }
        public V Value { get; set; }

        public Entry(K key, V val)
        {
            Key = key;
            Value = val;
            Hash = Key.GetHashCode();
        }

        public bool Equals(Entry<K, V> entry)
        {
            return Hash.Equals(entry.Hash);
        }

    }
    public class MyHashTableSeperateChainingEnumerator<K,V> : IEnumerator<K>
    {
        private List<Entry<K, V>>[] _table;
        public K Current => _table[_bucketIndex][_index].Key;
        object IEnumerator.Current => Current;

        private int _bucketIndex;
        private int _index;

        private readonly IntPtr _managedResources;
        private readonly SafeHandle _unmanagedResources;

        public MyHashTableSeperateChainingEnumerator(List<Entry<K, V>>[] table)
        {
            _table = table;
            _bucketIndex = -1;
            _index = 0;

            _unmanagedResources = new SafeFileHandle(new IntPtr(), true);
            _managedResources = Marshal.AllocHGlobal(sizeof(int));
        }

        public bool MoveNext()
        {
            if (_bucketIndex == -1)
            {
                do
                {
                    _bucketIndex++;

                } while (_bucketIndex < _table.Length && _table[_bucketIndex] == null);

                if (_bucketIndex < _table.Length)
                    return true;

            }
            else
            {
                if (++_index >= _table[_bucketIndex].Count)
                {
                    do
                    {
                        _bucketIndex++;

                    } while (_bucketIndex < _table.Length && _table[_bucketIndex] == null);

                    if (_bucketIndex < _table.Length)
                    {
                        _index = 0;
                        return true;
                    }
                }
                else
                    return true;
            }

            Reset();
            return false;
        }

        public void Reset()
        {
            _bucketIndex = -1;
            _index = 0;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool isDisposing)
        {
            ReleaseUnmanagedResources(_unmanagedResources);
            if (isDisposing) ReleaseManagedResources(_managedResources);
        }
        private void ReleaseUnmanagedResources(SafeHandle safeHandle)
        {
            if (safeHandle != null) safeHandle.Dispose();
        }
        private void ReleaseManagedResources(IntPtr intPtr)
        {
            Marshal.FreeHGlobal(intPtr);
        }
    }
}
