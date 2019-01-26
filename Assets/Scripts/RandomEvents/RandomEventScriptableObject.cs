using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polyjam2019
{
    [CreateAssetMenu(fileName = "RandomEvent", menuName = "Random Events/Random Event")]
    public class RandomEventScriptableObject : ScriptableObject
    {
        [SerializeField]
        private string plotDescription;

        [SerializeField]
        private List<RandomEventChoiceScriptableObject> choices;

        public string PlotDescription { get { return plotDescription; } }
    }
}