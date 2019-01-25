using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polyjam2019
{
    public class TurnManagerScript : MonoBehaviour
    {
        private TimeManagerScript timeManager;

        public void EndTurn()
        {
            if(timeManager != null)
            {
                timeManager.FireOnDayPassed();
            }
        }

        private void Awake()
        {
            // set timeManager with object from singleton
        }
    }
}
