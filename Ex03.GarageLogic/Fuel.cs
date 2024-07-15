using System;
using System.Text;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    class Fuel : EnergySource
    {
        private eFuelTypes m_FuelType;

        public Fuel(eFuelTypes i_FuelType, float i_CurrentEnergyCapacity, float i_MaxEnergyCapacity)
            : base(i_CurrentEnergyCapacity, i_MaxEnergyCapacity)
        {
            this.m_FuelType = i_FuelType;
            if (i_CurrentEnergyCapacity > i_MaxEnergyCapacity)
            {
                throw new Exception($"The current amount of fuel can not be greater than the maximum ({i_MaxEnergyCapacity}) capacity");
            }
        }
        public eFuelTypes FuelType
        {
            get { return m_FuelType; }
            set { m_FuelType = value; }
        }

        public override void AddEnergy(float i_AddEnergy, eFuelTypes? i_FuelType)
        {
            if (this.m_FuelType != i_FuelType)
            {
                throw new Exception($"Incorrect fuel! This vehicle fuel type is {this.FuelType}");
            }
            if (i_AddEnergy + base.CurrentEnergyCapacity > base.MaxEnergyCapacity)
            {
                throw new Exception($"Your tank capacity is:{base.MaxEnergyCapacity}. You can not add more then that!");
            }
            base.CurrentEnergyCapacity += i_AddEnergy;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("::: Gas Fuel Info :::");
            sb.AppendLine($" Type: {FuelType}");
            sb.AppendLine($" Remaining Fuel In Tank: {base.EnergyLeftInPercentage}%");
            sb.AppendLine($" Gas Capacity: {base.CurrentEnergyCapacity}");
            sb.AppendLine($" Max Capacity: {base.MaxEnergyCapacity}");
            return sb.ToString();
        }
    }
}