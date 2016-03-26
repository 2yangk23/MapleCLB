using System.Threading;

namespace MapleCLB.Tools {
    public class Blocking<T> {
        private readonly AutoResetEvent waiter;
        private T value;

        public Blocking() {
            waiter = new AutoResetEvent(false);
        }

        public Blocking(T value) {
            this.value = value;
            waiter = new AutoResetEvent(true);
        }

        public T Get() {
            waiter.WaitOne();
            return value;
        }

        public void Set(T value) {
            this.value = value;
            waiter.Set();
        }
    }
}
