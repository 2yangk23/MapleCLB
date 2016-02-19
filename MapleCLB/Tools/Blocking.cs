using System.Threading;

namespace MapleCLB.Tools {
    public class Blocking<T> {
        private readonly AutoResetEvent Waiter;
        private T Value;

        public Blocking() {
            Waiter = new AutoResetEvent(false);
        }

        public Blocking(T value) {
            Value = value;
            Waiter = new AutoResetEvent(true);
        } 

        public T Get() {
            Waiter.WaitOne();
            return Value;
        }

        public void Set(T value) {
            Value = value;
            Waiter.Set();
        }
    }
}
