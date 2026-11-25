using System;

namespace кр
{
    public class CPlane : Plane
    {
        private double carryingCapacity;

        public double CarryingCapacity
        {
            get { return carryingCapacity; }
            private set
            {
                if (value <= 0)
                    throw new ArgumentException("Грузоподъемность должна быть больше 0.");
                carryingCapacity = value;
            }
        }

        public CPlane(string planeType, int fuelConsumption, double carryingCapacity)
            : base(planeType, fuelConsumption)
        {
            CarryingCapacity = carryingCapacity;
        }

        public override string ToString()
        {
            return base.ToString() + $", грузоподъемность: {CarryingCapacity} т";
        }
    }
}