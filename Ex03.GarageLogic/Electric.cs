using System;
using System.Text;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    class Electric : EnergySource
    {
        public Electric(float i_CurrentEnergyCapacity, float i_MaxEnergyCapacity)
            : base(i_CurrentEnergyCapacity, i_MaxEnergyCapacity)
        {
            if (i_CurrentEnergyCapacity > i_MaxEnergyCapacity)
            {
                throw new Exception($"The current energy cannot be greater than the maximum ({i_MaxEnergyCapacity}) capacity");
            }
        }
        public override void AddEnergy(float i_AddEnergy, eFuelTypes? i_FuelType)
        {
            if (i_AddEnergy + base.CurrentEnergyCapacity > base.MaxEnergyCapacity)
            {
                throw new Exception($"Your battery capacity is:{base.MaxEnergyCapacity}. You can not add more then that!");
            }
            base.CurrentEnergyCapacity += i_AddEnergy;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("::: Electric Battery Info :::");
            sb.AppendLine($" Electric Capacity: {base.CurrentEnergyCapacity}");
            sb.AppendLine($" Remaining Battery Power: {base.EnergyLeftInPercentage}%");
            sb.AppendLine($" Max Capacity: {base.MaxEnergyCapacity}");
            return sb.ToString();
        }
    }
}
