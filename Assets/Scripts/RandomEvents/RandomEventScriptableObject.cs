using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polyjam2019
{
    public class RandomEventScriptableObject : ScriptableObject
    {
        [SerializeField]
        private string plotDescription;

        public string PlotDescription { get { return plotDescription; } }
    }
}