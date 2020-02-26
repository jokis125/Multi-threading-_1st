//Author Jokubas Kondrackas
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ConsoleApp1
{
    public class ParkingLot
    {
        private Dictionary<int, string> ParkingSpots = new Dictionary<int, string>();
        private int currentId = 0;
        private int lotSize;
        public int CarCount;

        public ParkingLot(int size)
        {
            lotSize = size;
            for (var i = 0; i < size; i++)
            {
                ParkingSpots.Add(i, "");
            }
        }

        private void AssignParkingSpace(string carNo, bool safe)
        {
            if (safe)
            {
                CarCount++;
                lock (ParkingSpots)
                {
                    if (lotSize >= currentId + 1)
                        ParkingSpots[currentId++] = carNo;
                }
            }
            else
            {
                CarCount++;
                if (lotSize >= currentId + 1)
                    ParkingSpots[currentId++] = carNo;
            }
        }
        
        public void AddRandomCar()
        {
            //Thread.Sleep((lotSize/(CarCount+1))/100);
            AssignParkingSpace($"{Program.RandomString(3)}{Program._random.Next(100, 999)}", false);
        }
        
        public void AddRandomCarSafe()
        {
            //Thread.Sleep((lotSize/(CarCount+1))/100);
            AssignParkingSpace($"{Program.RandomString(3)}{Program._random.Next(100, 999)}", true);
        }

        public void CheckForRaceCondition()
        {
            var temp = "Race condition happened because: ";
            if (CarCount == lotSize && ParkingSpots.Count == lotSize)
                temp = "Race condition didn't happen because: ";
            Console.WriteLine(temp + $"CarCount = {CarCount}; DictSize = {ParkingSpots.Count}; LotSize = {lotSize}");
        }

        public void FillParkingSpots(bool safe)
        {
            
            if (safe)
            {
                while (true)
                {
                    lock (ParkingSpots)
                    {
                        if (CarCount < lotSize)
                        {
                            ParkingSpots[currentId++] = $"{Program.RandomString(3)}{Program._random.Next(100, 999)}";
                            CarCount++;
                        }
                        else
                        {
                            break;
                        }
                        
                    }
                }
                return;
            }
            while (true)
            {
                
                if (CarCount < lotSize)
                {
                    ParkingSpots[currentId++] = $"{Program.RandomString(3)}{Program._random.Next(100, 999)}";
                    CarCount++;
                }
                else
                {
                    break;
                }
            }
        }

        public void FreeParkingSpace(string carNo)
        {
            CarCount--;
            for (var i = 0; i < ParkingSpots.Count; i++)
            {
                if (carNo == ParkingSpots[i])
                    ParkingSpots[i] = "";
            }
        }

        public void RemoveCarById(int id)
        {
            CarCount--;
            ParkingSpots[id] = "";
        }

        public void RemoveLastCar()
        {
            CarCount--;
            ParkingSpots[--currentId] = "";
        }

        public void FillParkingSpace()
        {
            for (var i = 0; i < ParkingSpots.Count; i++)
            {
                CarCount++;
                ParkingSpots[i] = $"{Program.RandomString(3)}{Program._random.Next(100, 999)}";
                currentId++;
            }
        }

        public void PrintParkingSpace()
        {
            foreach (var keyValuePair in ParkingSpots.ToList())
            {
                Console.WriteLine(keyValuePair);
            }
        }
    }
}