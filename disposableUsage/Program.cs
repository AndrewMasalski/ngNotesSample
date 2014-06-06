using System;

namespace disposableUsage
{
    class Program
    {
        static void Main(string[] args)
        {
            var holder = new StateHolder();
            // this code
            using (holder.PushDown())
            {
                // ...
                // here can be a lot of code, that supposed to be running in pushed down state
                // ...
                Console.WriteLine(holder.IsPushedDown);
            }
            Console.WriteLine(holder.IsPushedDown);
            // --------------------------------------------------------------------------------------
            // is same as
            holder.IsPushedDownManual = true;
            // ...
            // here can be a lot of code, that supposed to be running in pushed down state
            // ...
            Console.WriteLine(holder.IsPushedDown);
            holder.IsPushedDownManual = false;
            Console.WriteLine(holder.IsPushedDown);
        }

        public class StateHolder
        {
            private DisposableSwitchHelper<bool> _pushDown;

            public bool IsPushedDown
            {
                get { return _pushDown.Flag; }
            }

            public bool IsPushedDownManual { get; set; }

            public IDisposable PushDown()
            {
                _pushDown = new DisposableSwitchHelper<bool>(true, false);
                return _pushDown;
            }
        }

        public class DisposableSwitchHelper<T> : IDisposable
        {
            private readonly T _state2;

            public DisposableSwitchHelper(T state1, T state2)
            {
                _state2 = state2;
                Flag = state1;
            }

            public T Flag { get; private set; }

            public void Dispose()
            {
                Flag = _state2;
            }
        }
    }
}
