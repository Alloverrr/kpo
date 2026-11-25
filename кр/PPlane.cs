using System;

namespace кр
{
    public class PPlane : Plane
    {
        private int passengersCount;

        public int PassengersCount
        {
            get { return passengersCount; }
            private set
            {
                if (value <= 0)
                    throw new ArgumentException("Количество пассажиров должно быть больше 0.");
                passengersCount = value;
            }
        }

        public PPlane(string planeType, int fuelConsumption, int passengersCount)
            : base(planeType, fuelConsumption)
        {
            PassengersCount = passengersCount;
        }

        public override string ToString()
        {
            return base.ToString() + $", пассажиров: {PassengersCount}";
        }
    }
}