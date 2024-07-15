using System;
using System.Text;

namespace Ex03.GarageLogic
{
    class MotorcycleWheels : Wheels
    {
        public const float v_MaxTirePressure = 29;
        public MotorcycleWheels(float i_CurrentTirePressure, string i_Manufacture) : base(i_CurrentTirePressure, i_Manufacture)
        {
            if (i_CurrentTirePressure > v_MaxTirePressure)
            {
                throw new Exception("Current tire pressure is heigher than the maximum tire pressure");
            }
        }
        public override void Inflating()
        {
            base.CurrentTirePressure = v_MaxTirePressure;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("### Tires Info ###");
            sb.AppendLine($" Manufacture: {base.m_Manufacture}");
            sb.AppendLine($" Tire Pressure: {base.m_CurrentTirePressure}");
            sb.AppendLine($" Max Tire Pressure: {v_MaxTirePressure}");
            return sb.ToString();
        }
    }
}
