using System;
using System.Collections.Generic;

namespace кр
{
    internal class Program
    {
        private const string PassType1 = "Air 67";
        private const int PassFuel1 = 9449;
        private const int PassSeats1 = 189;

        private const string PassType2 = "Dinastya";
        private const int PassFuel2 = 3333;
        private const int PassSeats2 = 248;

        private const string CargoType1 = "Aviaseils";
        private const int CargoFuel1 = 5552;
        private const double CargoWeight1 = 144;

        private const string CargoType2 = "Gruz 200";
        private const int CargoFuel2 = 6767;
        private const double CargoWeight2 = 150;

        private const int MinFuel = 5000;
        private const int MaxFuel = 10000;
    private static void Main()
        {
            Airline airline = new Airline();

            airline.Add(new PPlane(PassType1, PassFuel1, PassSeats1));
            airline.Add(new PPlane(PassType2, PassFuel2, PassSeats2));
            airline.Add(new CPlane(CargoType1, CargoFuel1, CargoWeight1));
            airline.Add(new CPlane(CargoType2, CargoFuel2, CargoWeight2));

            Console.WriteLine("Все самолеты:");
            Console.WriteLine(airline);
            Console.WriteLine();

            Console.WriteLine("Общее число пассажиров:" + airline.Passengers);
            Console.WriteLine();

            airline.SortByFuel();
            Console.WriteLine("Список после сортировки:");
            Console.WriteLine(airline);
            Console.WriteLine();

            List<Plane> found = airline.FindByFuelRange(MinFuel, MaxFuel);
            Console.WriteLine($"Самолеты с расходом топлива от {MinFuel} до {MaxFuel}");
            foreach (Plane plane in found)
            {
                Console.WriteLine(plane);
            }
        }
    }
}
