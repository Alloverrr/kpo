using System;

namespace кр
{
    public abstract class Plane : IComparable<Plane>
    {
        private string planeType;
        private int fuelConsumption;

        public string PlaneType
        {
            get { return planeType; }
            protected set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Тип самолета не может быть пустым.");
                planeType = value;
            }
        }

        public int FuelConsumption
        {
            get { return fuelConsumption; }
            protected set
            {
                if (value <= 0)
                    throw new ArgumentException("Расход топлива должен быть больше 0.");
                fuelConsumption = value;
            }
        }

        protected Plane(string planeType, int fuelConsumption)
        {
            PlaneType = planeType;
            FuelConsumption = fuelConsumption;
        }

        public int CompareTo(Plane? other)
        {
            if (other == null) return 1;
            return FuelConsumption.CompareTo(other.FuelConsumption);
        }

        public override string ToString()
        {
            return $"Тип: {PlaneType}, расход топлива: {FuelConsumption} л/ч";
        }
    }
}