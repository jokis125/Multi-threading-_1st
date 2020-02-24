using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        public static Random _random = new Random();
        static void Main(string[] args)
        {
            var size = 10000;
            var carpark = new ParkingLot(size);
            
            //carpark.FillParkingSpace();
            
            //for(var i = 0; i < size/2; i++)
                //carpark.RemoveCarById(_random.Next(0, size/2));

                /*for (var i = 0; i < size; i++)
                {
                    threadList.Add(new Thread(carpark.AddRandomCarSafe));
                }*/
                
            List<Thread> threadList = new List<Thread>();
            for (var i = 0; i < 16; i++)
            {
                threadList.Add(new Thread(()=> carpark.FillParkingSpots(true)));
            }

            foreach (var thread in threadList)
            {
                thread.Start();
                //thread.Join();
            }

            foreach (var thread in threadList)
            {
                thread.Join();
            }
            carpark.PrintParkingSpace();
            carpark.CheckForRaceCondition();

        }

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_random.Next(s.Length)]).ToArray());
        }
    }
}