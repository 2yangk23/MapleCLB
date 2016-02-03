using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MapleCLB.Tools {
    public class MultiKeyDictionary<TK, TL, TV> {
        internal readonly Dictionary<TK, TV> BaseDictionary = new Dictionary<TK, TV>();
        internal readonly Dictionary<TL, TK> SubDictionary = new Dictionary<TL, TK>();
        internal readonly Dictionary<TK, TL> PrimaryToSubkeyMapping = new Dictionary<TK, TL>();

        ReaderWriterLockSlim ReaderWriterLock = new ReaderWriterLockSlim();

        public TV this[TL subKey] {
            get {
                TV item;
                if (TryGetValue(subKey, out item)) {
                    return item;
                }

                throw new KeyNotFoundException("sub key not found: " + subKey.ToString());
            }
        }

        public TV this[TK primaryKey] {
            get {
                TV item;
                if (TryGetValue(primaryKey, out item)) {
                    return item;
                }

                throw new KeyNotFoundException("primary key not found: " + primaryKey.ToString());
            }
        }

        public void Associate(TL subKey, TK primaryKey) {
            ReaderWriterLock.EnterUpgradeableReadLock();

            try {
                if (!BaseDictionary.ContainsKey(primaryKey))
                    throw new KeyNotFoundException(string.Format("The base dictionary does not contain the key '{0}'", primaryKey));

                if (PrimaryToSubkeyMapping.ContainsKey(primaryKey)) // Remove the old mapping first
                {
                    ReaderWriterLock.EnterWriteLock();

                    try {
                        if (SubDictionary.ContainsKey(PrimaryToSubkeyMapping[primaryKey])) {
                            SubDictionary.Remove(PrimaryToSubkeyMapping[primaryKey]);
                        }

                        PrimaryToSubkeyMapping.Remove(primaryKey);
                    } finally {
                        ReaderWriterLock.ExitWriteLock();
                    }
                }

                SubDictionary[subKey] = primaryKey;
                PrimaryToSubkeyMapping[primaryKey] = subKey;
            } finally {
                ReaderWriterLock.ExitUpgradeableReadLock();
            }
        }

        public bool TryGetValue(TL subKey, out TV val) {
            val = default(TV);

            TK primaryKey;

            ReaderWriterLock.EnterReadLock();

            try {
                if (SubDictionary.TryGetValue(subKey, out primaryKey)) {
                    return BaseDictionary.TryGetValue(primaryKey, out val);
                }
            } finally {
                ReaderWriterLock.ExitReadLock();
            }

            return false;
        }

        public bool TryGetValue(TK primaryKey, out TV val) {
            ReaderWriterLock.EnterReadLock();

            try {
                return BaseDictionary.TryGetValue(primaryKey, out val);
            } finally {
                ReaderWriterLock.ExitReadLock();
            }
        }

        public bool ContainsKey(TL subKey) {
            TV val;

            return TryGetValue(subKey, out val);
        }

        public bool ContainsKey(TK primaryKey) {
            TV val;

            return TryGetValue(primaryKey, out val);
        }

        public void Remove(TK primaryKey) {
            ReaderWriterLock.EnterWriteLock();

            try {
                if (PrimaryToSubkeyMapping.ContainsKey(primaryKey)) {
                    if (SubDictionary.ContainsKey(PrimaryToSubkeyMapping[primaryKey])) {
                        SubDictionary.Remove(PrimaryToSubkeyMapping[primaryKey]);
                    }

                    PrimaryToSubkeyMapping.Remove(primaryKey);
                }

                BaseDictionary.Remove(primaryKey);
            } finally {
                ReaderWriterLock.ExitWriteLock();
            }
        }

        public void Remove(TL subKey) {
            ReaderWriterLock.EnterWriteLock();

            try {
                BaseDictionary.Remove(SubDictionary[subKey]);

                PrimaryToSubkeyMapping.Remove(SubDictionary[subKey]);

                SubDictionary.Remove(subKey);
            } finally {
                ReaderWriterLock.ExitWriteLock();
            }
        }

        public void Add(TK primaryKey, TV val) {
            ReaderWriterLock.EnterWriteLock();

            try {
                BaseDictionary.Add(primaryKey, val);
            } finally {
                ReaderWriterLock.ExitWriteLock();
            }
        }

        public void Add(TK primaryKey, TL subKey, TV val) {
            Add(primaryKey, val);

            Associate(subKey, primaryKey);
        }

        public TV[] CloneValues() {
            ReaderWriterLock.EnterReadLock();

            try {
                TV[] values = new TV[BaseDictionary.Values.Count];

                BaseDictionary.Values.CopyTo(values, 0);

                return values;
            } finally {
                ReaderWriterLock.ExitReadLock();
            }
        }

        public List<TV> Values {
            get {
                ReaderWriterLock.EnterReadLock();

                try {
                    return BaseDictionary.Values.ToList();
                } finally {
                    ReaderWriterLock.ExitReadLock();
                }
            }
        }

        public TK[] ClonePrimaryKeys() {
            ReaderWriterLock.EnterReadLock();

            try {
                TK[] values = new TK[BaseDictionary.Keys.Count];

                BaseDictionary.Keys.CopyTo(values, 0);

                return values;
            } finally {
                ReaderWriterLock.ExitReadLock();
            }
        }

        public TL[] CloneSubKeys() {
            ReaderWriterLock.EnterReadLock();

            try {
                TL[] values = new TL[SubDictionary.Keys.Count];

                SubDictionary.Keys.CopyTo(values, 0);

                return values;
            } finally {
                ReaderWriterLock.ExitReadLock();
            }
        }

        public void Clear() {
            ReaderWriterLock.EnterWriteLock();

            try {
                BaseDictionary.Clear();

                SubDictionary.Clear();

                PrimaryToSubkeyMapping.Clear();
            } finally {
                ReaderWriterLock.ExitWriteLock();
            }
        }

        public int Count {
            get {
                ReaderWriterLock.EnterReadLock();

                try {
                    return BaseDictionary.Count;
                } finally {
                    ReaderWriterLock.ExitReadLock();
                }
            }
        }

        public IEnumerator<KeyValuePair<TK, TV>> GetEnumerator() {
            ReaderWriterLock.EnterReadLock();

            try {
                return BaseDictionary.GetEnumerator();
            } finally {
                ReaderWriterLock.ExitReadLock();
            }
        }
    }
}
