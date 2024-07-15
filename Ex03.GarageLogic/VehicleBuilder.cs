using System.Collections.Generic;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    public struct VehicleBuilder
    {
        //Customer
        public eVehicleTypes m_VehicleType;
        public string m_VehicleOwnerPhone;
        public string m_VehicleOwnerName;
         
        public string m_LicenseNumber;
        public string m_ModelName;
        public float m_CurrentVehicleEnergy;
        public eEnergySource m_EnergySource;

        //Car
        public eDoors m_Doors;
        public eColors m_Color;

        //Motorcycle
        public eLicenseType m_LicenseType;
        public int m_EngineVolume;

        //Truck
        public bool m_IsTransportChemicals;
        public float m_CargoVolume;

        //Wheel
        public string m_WheelManufacturer;
        public float m_CurrentTirePressure;
    }
}
