#region Copyright
/*********************************************************************************************
Namespace        : BenchmarkLINQ
Description      : LINQ vs For-Loop performance estimates
                 : 2 methods to calculate the sum of even integers in array int[]
*********************************************************************************************
Author           : Alexander Bell
Copyright        : 2016 Infosoft International Inc
Date Created     : 10/22/2016
*********************************************************************************************
DISCLAIMER       : This Application is provide on AS IS basis without any warranty
TERMS OF USE     : Please Keep the Copyright notice intact/
*********************************************************************************************/
#endregion
using System;
using System.Diagnostics;
using System.Linq;

namespace BenchmarkLINQ
{
    class Program
    {
        private const Int64 LoopCounter = 1000;
        private static readonly int[] _arrInt = { 111, 112, 22, 38, 116, 133, 232, 236, 364, 427};

        static void Main(string[] args)
        {
            Console.WriteLine("Runtime LINQ. ms: " + Sum_LINQ(_arrInt).ToString());
            Console.WriteLine("Runtime ForLoop. ms: " + Sum_For(_arrInt).ToString());
            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }

        #region LINQ-based conditional sum calculation, Performance benchmark
        /// <summary>
        /// Conditional sum of even integers in int[] using LINQ, performance benchmark
        /// 1. Very slow
        /// 2. Non-portable to core Java (it requires LINQ library)
        /// </summary>
        /// <param name="intArray">int[]</param>
        /// <returns>double</returns>
        private static double Sum_LINQ(int[] intArray)
        {
            long _sum;

            // stopwatch obj (ref: System.Diagnostics)
            Stopwatch _sw = new Stopwatch();
            // start count ticks
            _sw.Start();

            // multiple pass defined by LoopCounter
            for (long j=0; j<LoopCounter; j++)
            {
                _sum = intArray.Where(i => i % 2 == 0).Sum(i => (long)i);
            }
            _sw.Stop();
            // duration in ms, rounded
            return Math.Round(TicksToMillisecond(_sw.ElapsedTicks), 2);
        }
        #endregion

        #region For-Loop conditional sum calculation, Performance benchmark
        /// <summary>
        ///  Conditional sum of even integers in int[] using For-Loop, Performance benchmark
        ///  1. Very fast (more than the order of magnitude faster than LINQ implementation)
        ///  2. Easy portable to core Java
        /// </summary>
        /// <param name="intArray">int[]</param>
        /// <returns>double</returns>
        private static double Sum_For(int[] intArray)
        {
            long _ret;
            // stopwatch obj (ref: System.Diagnostics)
            Stopwatch _sw = new Stopwatch();

            // start count ticks
            _sw.Start();
            for (long j = 0; j < LoopCounter; j++)
            {
                _ret = 0;
                for (int i=0; i< intArray.Length; i++)
                {
                    if(intArray[i] % 2==0) _ret += intArray[i];
                }
            }
            _sw.Stop();
            // duration in ms, rounded
            return Math.Round(TicksToMillisecond(_sw.ElapsedTicks), 2);
        }
        #endregion

        /// <summary>
        /// Auxiliary method to calculate runtime (ms) from Ticks
        /// </summary>
        /// <param name="Ticks">Int64</param>
        /// <returns>double</returns>
        private static double TicksToMillisecond(Int64 Ticks)
        {
            // msec per tick (stopwatch frequency in kHz)
            double _msPerTick = (double)1000 / Stopwatch.Frequency;

            // duration in msec
            return Ticks * _msPerTick;
        }
    }
}
