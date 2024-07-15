using System.Collections.Generic;
using Ex03.GarageLogic.Enums;
using System.Text;

namespace Ex03.GarageLogic
{
    internal class Motorcycle : Vehicle
    {
        private eLicenseType m_LicenseType;
        private int m_EngineCapacity;
        public Motorcycle() { }
        public Motorcycle(string i_Model, string i_LicenseNumber, List<Wheels> i_Wheels, eLicenseType i_LicenseType, int i_EngineCapacity)
            : base(i_Model, i_LicenseNumber, i_Wheels)
        {
            this.m_LicenseType = i_LicenseType;
            this.m_EngineCapacity = i_EngineCapacity;
        }

        public eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set { m_LicenseType = value; }
        }

        public int EngineCapacity
        {
            get { return m_EngineCapacity; }
            set { m_EngineCapacity = value; }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("### Motorcycle Info ###");
            sb.AppendLine($" Model: {base.Model}");
            sb.AppendLine($" Licenses: {base.LicenseNumber}");
            sb.AppendLine($" License Type: {this.LicenseType}");
            sb.AppendLine($" Engine Volume: {this.m_EngineCapacity}");
            sb.AppendLine($" {base.VehicleEnergySource} ");
            sb.AppendLine($" {base.Wheels[0]} ");
            return sb.ToString();
        }
    }
}
