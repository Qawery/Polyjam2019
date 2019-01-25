using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polyjam2019
{
    public class TimeManagerScript
    {
        public delegate void DayPassed();

        private event DayPassed OnDayPassed;

        private int day;

        public void Instantiate()
        {
            day = 0;
            RegisterToOnDayPassed(IncrementDayNumber);
        }

        public void RegisterToOnDayPassed(DayPassed listener)
        {
            OnDayPassed += listener;
        }

        public void DeregisterFromOnDayPassed(DayPassed listener)
        {
            OnDayPassed -= listener;
        }        

        public void FireOnDayPassed()
        {
            if(OnDayPassed != null)
            {
                OnDayPassed();
            }
        }

        public int Day { get { return day; } }

        private void IncrementDayNumber()
        {
            day++;
        }
    }
}
