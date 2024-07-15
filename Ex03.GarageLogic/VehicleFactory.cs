using System;
using Ex03.GarageLogic.Enums;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;

namespace Ex03.GarageLogic
{
    public static class VehicleFactory
    {
        public static List<Vehicle> m_VehicelList = new List<Vehicle> { new Car(), new Motorcycle(), new Truck() };
        public static Vehicle CreateVehicle(eVehicleTypes i_VehicleType)
        {
            switch (i_VehicleType)
            {
                case eVehicleTypes.Car:
                    return new Car();
                case eVehicleTypes.Motorcycle:
                    return new Motorcycle();
                case eVehicleTypes.Truck:
                    return new Truck();
                default:
                    throw new ArgumentException("Invalid vehicle type");
            }
        }
    }
}
