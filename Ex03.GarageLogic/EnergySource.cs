using System;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    public abstract class EnergySource
    {
        protected float m_CurrentEnergyCapacity;
        protected int m_EnergyLeftInPercentage;
        protected float m_MaxEnergyCapacity;

        public EnergySource(float i_CurrentEnergyCapacity, float i_MaxEnergyCapacity)
        {
            this.m_CurrentEnergyCapacity = i_CurrentEnergyCapacity;
            this.m_MaxEnergyCapacity = i_MaxEnergyCapacity;
            this.m_EnergyLeftInPercentage = (int)Math.Round((i_CurrentEnergyCapacity / i_MaxEnergyCapacity) * 100f);
        }

        protected float CurrentEnergyCapacity
        {
            get { return m_CurrentEnergyCapacity; }
            set
            {
                m_CurrentEnergyCapacity = value;
                m_EnergyLeftInPercentage = (int)Math.Round((m_CurrentEnergyCapacity / m_MaxEnergyCapacity) * 100f);
            }
        }
        protected float MaxEnergyCapacity
        {
            get { return m_MaxEnergyCapacity; }
        }
        protected int EnergyLeftInPercentage
        {
            get { return m_EnergyLeftInPercentage; }
            set { m_EnergyLeftInPercentage = value; }
        }
        public abstract void AddEnergy(float i_AddEnergy, eFuelTypes? i_FuelType);
    }
}
