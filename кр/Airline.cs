using System;
using System.Collections.Generic;
using кр;

namespace кр
{
    public class Airline
    {
        private List<Plane> planes;

        public Airline()
        {
            planes = new List<Plane>();
        }

        public void Add(Plane plane)
        {
            if (plane == null)
                throw new ArgumentNullException(nameof(plane));
            planes.Add(plane);
        }

        public int Passengers
        {
            get
            {
                int sum = 0;
                foreach (Plane plane in planes)
                {
                    if (plane is PPlane pPlane)
                    {
                        sum += pPlane.PassengersCount;
                    }
                }
                return sum;
            }
        }

        public void SortByFuel()
        {
            planes.Sort();
        }

        public List<Plane> FindByFuelRange(int a, int b)
        {
            if (a > b)
                throw new ArgumentException("Нижняя граница диапазона не должна быть больше верхней.");

            List<Plane> result = new List<Plane>();
            foreach (Plane plane in planes)
            {
                if (plane.FuelConsumption >= a && plane.FuelConsumption <= b)
                {
                    result.Add(plane);
                }
            }
            return result;
        }
        public override string ToString()
        {
            if (planes.Count == 0)
                return "Список самолетов пуст.";

            string info = "";
            foreach (Plane plane in planes)
            {
                info += plane.ToString() + Environment.NewLine;
            }
            return info.TrimEnd();
        }
    }
}