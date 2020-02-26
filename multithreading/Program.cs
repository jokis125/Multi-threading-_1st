/*Author Jokubas Kondrackas
Norint kad butu race condition reikia carpark.FillParkingSpots(bool safe) iskviesti su argumentu false
Kvieciant su true race condition neivyksta, nes apsaugota kritine sritis
 */
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
            
            List<Thread> threadList = new List<Thread>();
            for (var i = 0; i < 16; i++)
            {
                threadList.Add(new Thread(()=> carpark.FillParkingSpots(false)));
            }

            foreach (var thread in threadList)
            {
                thread.Start();
            }

            foreach (var thread in threadList)
            {
                thread.Join();
            }
            //carpark.PrintParkingSpace();
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