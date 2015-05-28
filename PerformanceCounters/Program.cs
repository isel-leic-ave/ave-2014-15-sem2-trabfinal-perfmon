using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Threading;

namespace PerformanceCounters
{
    class Program
    {
        static void CreateCounter() 
        {
            CounterCreationDataCollection data = new CounterCreationDataCollection();
            CounterCreationData samplesCounter = new CounterCreationData();
            samplesCounter.CounterType = PerformanceCounterType.NumberOfItems32;
            samplesCounter.CounterName = "SampleCounter";
            data.Add(samplesCounter);

            PerformanceCounterCategory.Create(
                "SampleCategory",
                "Simple performance counter in C#",
                PerformanceCounterCategoryType.SingleInstance,
                data);
        }

        static void Main(string[] args)
        {
            if (!PerformanceCounterCategory.Exists("SampleCategory") )
            {
                CreateCounter();
            }
            PerformanceCounter counter = new PerformanceCounter(
                "SampleCategory", 
                "SampleCounter", 
                false);
            for (int i = 0; i < 360*4; ++i)
            {
                double sinValue = Math.Sin(i*Math.PI/180) * 100;
                counter.RawValue = (long)(sinValue < 0 ? -sinValue : sinValue);
                Thread.Sleep(200);
            }
        }
    }
}
