using System.Collections.Generic;
using Ex03.GarageLogic.Enums;
using System;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private readonly Dictionary<string, VehicleTicket> m_VehicelsTicket;
        public Garage()
        {
            m_VehicelsTicket = new Dictionary<string, VehicleTicket>();
        }

        public void InsertNewVehicle(VehicleBuilder i_VehicleBuilder)
        {
            try
            {
                Vehicle vehicleBuilder = VehicleFactory.CreateVehicle(i_VehicleBuilder.m_VehicleType);
                vehicleBuilder.LicenseNumber = i_VehicleBuilder.m_LicenseNumber;
                vehicleBuilder.Model = i_VehicleBuilder.m_ModelName;

                vehicleBuilder.VehicleEnergySource = SetVehicleEnergySource(i_VehicleBuilder.m_VehicleType, i_VehicleBuilder.m_EnergySource, i_VehicleBuilder.m_CurrentVehicleEnergy);
                vehicleBuilder.Wheels = SetVehicleWheels(i_VehicleBuilder.m_VehicleType, i_VehicleBuilder.m_CurrentTirePressure, i_VehicleBuilder.m_WheelManufacturer);

                switch (i_VehicleBuilder.m_VehicleType)
                {
                    case eVehicleTypes.Car:
                        Car car = (Car)vehicleBuilder;
                        car.Color = i_VehicleBuilder.m_Color;
                        car.Doors = i_VehicleBuilder.m_Doors;
                        vehicleBuilder = car;
                        break;
                    case eVehicleTypes.Motorcycle:
                        Motorcycle motorcycle = (Motorcycle)vehicleBuilder;
                        motorcycle.LicenseType = i_VehicleBuilder.m_LicenseType;
                        vehicleBuilder = motorcycle;
                        break;
                    case eVehicleTypes.Truck:
                        Truck truck = (Truck)vehicleBuilder;
                        truck.IsTransportChemicals = i_VehicleBuilder.m_IsTransportChemicals;
                        truck.CargoValume = i_VehicleBuilder.m_CargoVolume;
                        vehicleBuilder = truck;
                        break;
                }

                VehicleTicket vehicleTicket = new VehicleTicket(i_VehicleBuilder.m_VehicleOwnerName, i_VehicleBuilder.m_VehicleOwnerPhone, vehicleBuilder);
                m_VehicelsTicket[vehicleTicket.Vehicle.LicenseNumber] = vehicleTicket;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public List<string> VehicleFilterByState(eVehicleStates? i_VehicleState = null)
        {
            List<string> licenseNumberList = new List<string>();
            foreach (var vehicleTicket in m_VehicelsTicket)
            {
                if (i_VehicleState == 0)
                {
                    licenseNumberList.Add(vehicleTicket.Value.Vehicle.LicenseNumber);
                }
                else if (vehicleTicket.Value.VehicleState == i_VehicleState && i_VehicleState != null)
                {
                    licenseNumberList.Add(vehicleTicket.Value.Vehicle.LicenseNumber);
                }
            }
            return licenseNumberList;
        }
        public void ChangeVehicleState(string i_VehicleLicense, eVehicleStates i_NewState)
        {
            if (m_VehicelsTicket.ContainsKey(i_VehicleLicense))
            {
                m_VehicelsTicket[i_VehicleLicense].VehicleState = i_NewState;
            }
            else
            {
                throw new Exception($"Vehicle with license number '{i_VehicleLicense}' was not found");
            }
        }
        public void InflatingToMax(string i_VehicleLicense)
        {
            if (m_VehicelsTicket.ContainsKey(i_VehicleLicense))
            {
                List<Wheels> wheels = m_VehicelsTicket[i_VehicleLicense].Vehicle.Wheels;
                foreach (var wheel in wheels)
                {
                    wheel.Inflating();
                }
            }
            else
            {
                throw new Exception($"Vehicle with license number '{i_VehicleLicense}' was not found");
            }
        }
        public bool RefulingCarGas(string i_VehicleLicense, eFuelTypes i_FuelType, float i_FuelAmount)
        {
            bool isRefulingGasSucceed = false;
            if (m_VehicelsTicket.ContainsKey(i_VehicleLicense))
            {
                EnergySource energySource = m_VehicelsTicket[i_VehicleLicense].Vehicle.VehicleEnergySource;
                try
                {
                    energySource.AddEnergy(i_FuelAmount, i_FuelType);
                    isRefulingGasSucceed = true;
                }
                catch (Exception exception)
                {
                    throw new ArgumentException($"{exception.Message}");
                }
            }
            return isRefulingGasSucceed;
        }
        public void ChargeBattery(string i_VehicleLicense, float i_TimeCharging)
        {
            if (m_VehicelsTicket.ContainsKey(i_VehicleLicense))
            {
                EnergySource energySource = m_VehicelsTicket[i_VehicleLicense].Vehicle.VehicleEnergySource;
                if (energySource is Electric)
                {
                    energySource.AddEnergy(i_TimeCharging, null);
                }
                else
                {
                    throw new ArgumentException("Wrong energy type!");
                }
            }
        }
        public string DisplayVehicleInfo(string i_VehicleLicense)
        {
            string vehicleDetails;
            if (m_VehicelsTicket.ContainsKey(i_VehicleLicense))
            {
                vehicleDetails = m_VehicelsTicket[i_VehicleLicense].ToString();
            }
            else
            {
                throw new Exception($"Vehicle with license number '{i_VehicleLicense}' was not found");
            }
            return vehicleDetails;
        }
        public bool IsVehicleExistsInGarage(string i_LicenseNumber)
        {
            bool isVehicleExists = false;
            if (m_VehicelsTicket.ContainsKey(i_LicenseNumber))
            {
                isVehicleExists = true;
            }
            return isVehicleExists;
        }
        public EnergySource SetVehicleEnergySource(eVehicleTypes i_VehicleType, eEnergySource i_EnergySource, float i_CurrentVehicleEnergy)
        {
            EnergySource energySource = null;
            switch (i_EnergySource)
            {
                case eEnergySource.Gas:
                    if (i_VehicleType == eVehicleTypes.Car)
                    {
                        energySource = new Fuel(eFuelTypes.Octan95, i_CurrentVehicleEnergy, 58);
                    }
                    else if (i_VehicleType == eVehicleTypes.Motorcycle)
                    {
                        energySource = new Fuel(eFuelTypes.Octan98, i_CurrentVehicleEnergy, 5.8f);
                    }
                    else
                    {
                        energySource = new Fuel(eFuelTypes.Soler, i_CurrentVehicleEnergy, 110);
                    }
                    break;
                case eEnergySource.Electric:
                    if (i_VehicleType == eVehicleTypes.Car)
                    {
                        energySource = new Electric(i_CurrentVehicleEnergy, 4.8f);
                    }
                    else if (i_VehicleType == eVehicleTypes.Motorcycle)
                    {
                        energySource = new Electric(i_CurrentVehicleEnergy, 2.8f);
                    }
                    break;
            }
            return energySource;
        }
        public List<Wheels> SetVehicleWheels(eVehicleTypes i_VehicleType, float i_TirePressure, string i_Manufacture)
        {
            List<Wheels> wheels = new List<Wheels>();
            switch (i_VehicleType)
            {
                case eVehicleTypes.Car:
                    for (int i = 0; i < 5; i++)
                    {
                        wheels.Add(new CarWheels(i_TirePressure, i_Manufacture));
                    }
                    break;
                case eVehicleTypes.Truck:
                    for (int i = 0; i < 5; i++)
                    {
                        wheels.Add(new TruckWheels(i_TirePressure, i_Manufacture));
                    }
                    break;
                case eVehicleTypes.Motorcycle:
                    for (int i = 0; i < 2; i++)
                    {
                        wheels.Add(new MotorcycleWheels(i_TirePressure, i_Manufacture));
                    }
                    break;
            }
            return wheels;
        }
    }
}