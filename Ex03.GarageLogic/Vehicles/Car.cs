using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    class Car : Vehicle
    {
        private eColors m_Color;
        private eDoors m_NumOfDoors;
        public Car() { }
        public Car(string i_Model, string i_LicenseNumber, List<Wheels> i_Wheels, eColors i_Colors, eDoors i_NumOfDoors)
            : base(i_Model, i_LicenseNumber, i_Wheels)
        {
            this.m_Color = i_Colors;
            this.m_NumOfDoors = i_NumOfDoors;

        }
        public eColors Color
        {
            get { return m_Color; }
            set { m_Color = value; }
        }
        public eDoors Doors
        {
            get { return m_NumOfDoors; }
            set { m_NumOfDoors = value; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("### Car Info ###");
            sb.AppendLine($" Model: {base.Model}");
            sb.AppendLine($" Licenses: {base.LicenseNumber}");
            sb.AppendLine($" Color: {this.m_Color}");
            sb.AppendLine($" Doors: {this.m_NumOfDoors}");
            sb.AppendLine($" {base.VehicleEnergySource}");
            sb.AppendLine($" {base.Wheels[0]} ");
            return sb.ToString();
        }
    }
}
