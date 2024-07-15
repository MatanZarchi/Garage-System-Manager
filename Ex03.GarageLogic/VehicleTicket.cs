using System.Text;
using Ex03.GarageLogic.Enums;

namespace Ex03.GarageLogic
{
    public class VehicleTicket
    {
        private Vehicle m_Vehicle;
        private string m_OwnerName;
        private string m_OwnerPhone;
        private eVehicleStates m_VehicleState;
        public VehicleTicket(string i_OwnerName, string i_OwnerPhone, Vehicle i_VehicleObject)
        {
            this.m_OwnerName = i_OwnerName;
            this.m_OwnerPhone = i_OwnerPhone;
            this.m_Vehicle = i_VehicleObject;
        }
        public string OwnerName
        {
            get { return m_OwnerName; }
            set { m_OwnerName = value; }
        }
        public string OwnerPhone
        {
            get { return m_OwnerPhone; }
            set { m_OwnerPhone = value; }
        }
        public eVehicleStates VehicleState
        {
            get { return m_VehicleState; }
            set { m_VehicleState = value; }
        }
        public Vehicle Vehicle
        {
            get { return m_Vehicle; }
            set { m_Vehicle = value; }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("***** Vehicle Ticket *****");
            sb.AppendLine("--- Customer Details ---");
            sb.AppendLine($" Customer Name: {m_OwnerName}");
            sb.AppendLine($" Phone Number: {m_OwnerPhone}");
            sb.AppendLine($" Vehicle Status: {m_VehicleState}");
            sb.AppendLine($" {m_Vehicle}");
            return sb.ToString();
        }
    }
}
