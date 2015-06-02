using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading;

namespace PerformanceCounters
{
    class Program
    {
        static readonly String CATEGORY = "AVEMath";
        static readonly String COUNTER = "Sin";

        static void CreateCounter() 
        {
            CounterCreationDataCollection data = new CounterCreationDataCollection();
            CounterCreationData samplesCounter = new CounterCreationData();
            samplesCounter.CounterType = PerformanceCounterType.NumberOfItems32;
            samplesCounter.CounterName = COUNTER;
            data.Add(samplesCounter);

            PerformanceCounterCategory.Create(
                CATEGORY,
                "A fictitious performance counter in C#",
                PerformanceCounterCategoryType.MultiInstance,
                data);
        }

        static void Main(string[] args)
        {
            //PerformanceCounterCategory.Delete(CATEGORY);
            if (!PerformanceCounterCategory.Exists(CATEGORY) )
            {
                CreateCounter();
            }
            PerformanceCounter counter = new PerformanceCounter(
                CATEGORY, 
                COUNTER, 
                AppDomain.CurrentDomain.FriendlyName,
                false);
            for (int i = 0; ; ++i)
            {
                counter.RawValue = (long) (Math.Sin(i*Math.PI/180) * 50 + 50);
                Console.WriteLine("Counter value = {0}", counter.RawValue);
                Thread.Sleep(200);
            }
        }
    }
}
