using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polyjam2019
{
    [CreateAssetMenu(fileName = "Event Choice", menuName = "Random Events/Event Choice")]
    public class RandomEventChoiceScriptableObject : ScriptableObject
    {
        [SerializeField]
        private string choiceDisplayString;

        [SerializeField]
        private ChoiceResult[] choiceResults;

        private int[] weightedCategories;
        private int weightSum;

        public string ChoiceDisplayString { get { return choiceDisplayString; } }

        public ChoiceResult ChooseResult()
        {
            ChoiceResult result = null;

            if(WeightedCategoriesNotPrepared)
            {
                PrepareWeightedCategories();
            }

            int choice = UnityEngine.Random.Range(0, weightSum);

            for(int i = 0; i < weightedCategories.Length; i++)
            {
                if(choice < weightedCategories.Length)
                {
                    result = choiceResults[i];
                    break;
                }
            }

            return result;
        }

        private void PrepareWeightedCategories()
        {
            weightSum = 0;
            weightedCategories = new int[choiceResults.Length];

            for(int i = 0; i < choiceResults.Length; i++)
            {
                weightSum += (choiceResults[i].Weight > 0 ? choiceResults[i].Weight : 0);
                weightedCategories[i] = weightSum;
            }
        }

        private bool WeightedCategoriesNotPrepared
        {
            get { return weightedCategories == null; }
        }
    }

    [Serializable]
    public class ChoiceResult
    {
        [SerializeField]
        private int weight;

        public int Weight { get { return weight; } }
    }
}
