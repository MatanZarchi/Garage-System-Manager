using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    class Truck : Vehicle
    {
        private bool m_IsTransportChemicals;
        private float m_CargoValume;
        public Truck() { }
        public Truck(string i_Model, string i_LicenseNumber, List<Wheels> i_Wheels,
            bool i_IsTransportChemicals, float i_CargoValume) : base(i_Model, i_LicenseNumber, i_Wheels)
        {
            this.m_IsTransportChemicals = i_IsTransportChemicals;
            this.m_CargoValume = i_CargoValume;
        }

        public bool IsTransportChemicals
        {
            get { return m_IsTransportChemicals; }
            set { m_IsTransportChemicals = value; }
        }
        public float CargoValume
        {
            get { return m_CargoValume; }
            set { m_CargoValume = value; }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("### Truck Info ###");
            sb.AppendLine($" Model: {base.Model}");
            sb.AppendLine($" Licenses: {base.LicenseNumber}");
            sb.AppendLine($" Has Chemicals: {this.m_IsTransportChemicals}");
            sb.AppendLine($" Cargo Valume: {this.m_CargoValume}");
            //sb.AppendLine($" Energy Capacity: {base.Energy}");
            sb.AppendLine($" {base.VehicleEnergySource}");
            sb.AppendLine($" {base.Wheels[0]} ");
            return sb.ToString();
        }
    }
}
