using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;

namespace SimpleNumbersThreads
{
    internal class Program
    {
        private static readonly ConcurrentQueue<int> SimpleNumbers = new ConcurrentQueue<int>();


        public static void Main(string[] args)
        {
            var timer = new Stopwatch();
            var t1 = new Thread(FindNextSimple) {Priority = ThreadPriority.Normal};
            SimpleNumbers.Enqueue(1);
            SimpleNumbers.Enqueue(2);
            t1.Start();
            timer.Start();
            while (t1.IsAlive || SimpleNumbers.Count > 0)
                if (SimpleNumbers.TryDequeue(out var result))
                    Console.WriteLine(result);
            Console.WriteLine(timer.Elapsed.TotalMilliseconds);
        }

        private static void FindNextSimple()
        {
            var i = 3;
            while (i < 10000)
            {
                if (IsSimple(i))
                    SimpleNumbers.Enqueue(i);
                i += 2;
            }
        }

        private static bool IsSimple(int n)
        {
            for (var i = 2; i < n / 2; i++)
                if (n % i == 0)
                    return false;

            return true;
        }
    }
}