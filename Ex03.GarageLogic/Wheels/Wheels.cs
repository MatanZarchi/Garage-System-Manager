using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Wheels
    {
        protected float m_CurrentTirePressure;
        protected string m_Manufacture;

        public Wheels() { }
        public Wheels(float i_CurrentTirePressure, string i_Manufacture)
        {
            this.m_CurrentTirePressure = i_CurrentTirePressure;
            this.m_Manufacture = i_Manufacture;
        }
        protected string Manufacture
        {
            get { return m_Manufacture; }
            set { m_Manufacture = value; }
        }
        protected float CurrentTirePressure
        {
            get { return m_CurrentTirePressure; }
            set { m_CurrentTirePressure = value; }
        }
        public abstract void Inflating();
    }
}
