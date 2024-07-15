using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    abstract public class Vehicle
    {
        private string m_Model;
        private List<Wheels> m_Wheels;
        private string m_LicenseNumber;
        private EnergySource m_VehicleEnergySource;
        public Vehicle() { }
        public Vehicle(string i_Model, string i_LicenseNumber, List<Wheels> i_Wheels)
        {
            this.m_Model = i_Model;
            this.m_Wheels = i_Wheels;
            this.m_LicenseNumber = i_LicenseNumber;
        }
        public string Model
        {
            get { return m_Model; }
            set { m_Model = value; }
        }
        public string LicenseNumber
        {
            get { return m_LicenseNumber; }
            set { m_LicenseNumber = value; }
        }
        public EnergySource VehicleEnergySource
        {
            get { return m_VehicleEnergySource; }
            set { m_VehicleEnergySource = value; }
        }
        public List<Wheels> Wheels
        {
            get { return m_Wheels; }
            set { m_Wheels = value; }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"### {this.m_LicenseNumber} Ticket Info ### ");
            sb.AppendLine($" Model: {this.m_Model} ");
            sb.AppendLine($" {this.VehicleEnergySource} ");
            sb.AppendLine($" {this.Wheels[0]} ");
            return sb.ToString();
        }
    }
}
