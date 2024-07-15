using System;

namespace Ex03.GarageLogic
{
    public class ValueOutOfRangeException : Exception
    {
        private float m_MinValue;
        private float m_MaxValue;

        public ValueOutOfRangeException(Exception i_InnerException, float i_MaxValue, float i_MinValue) :
            base(string.Format("It's invalid... You must choose values between {0} - {1}!", i_MinValue, i_MaxValue), i_InnerException)
        {
            m_MinValue = i_MinValue;
            m_MaxValue = i_MaxValue;          
        }
    }
}