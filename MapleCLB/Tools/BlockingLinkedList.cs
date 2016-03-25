using System.Collections.Generic;
using System.Threading;

namespace MapleCLB.Tools {
    public class BlockingLinkedList<T> {
        private readonly LinkedList<T> list = new LinkedList<T>();
        private readonly AutoResetEvent waiter = new AutoResetEvent(false);
        private readonly object accessLock = new object();

        public T GetFirst() {
            waiter.WaitOne();

            lock (accessLock) {
                var result = list.First.Value;
                list.RemoveFirst();
                if (list.Count > 0) {
                    waiter.Set();
                }
                return result;
            }
        }

        public void AddFirst(T item) {
            lock (accessLock) {
                list.AddFirst(item);
            }
            waiter.Set();
        }

        public void AddLast(T item) {
            lock (accessLock) {
                list.AddLast(item);
            }
            waiter.Set();
        }
    }
}
