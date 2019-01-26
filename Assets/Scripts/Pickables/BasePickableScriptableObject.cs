using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polyjam2019.Pickables
{    
    [CreateAssetMenu(fileName = "Pickable Item", menuName ="Base Pickable Object")]
    public class BasePickableScriptableObject : ScriptableObject
    {
        [SerializeField]
        protected float weight;

        [SerializeField]
        protected float size;

        [SerializeField]
        protected float basePickupTime;

        public float Weight { get { return weight; } }
        public float Size { get { return size; } }
        public float BasePickupTime { get { return basePickupTime; } }

        [System.Serializable]
        public class PickableData
        {
            private static int PickableDataMaxID = 0;

            private float weight;
            private float size;

            private int itemID;

            internal PickableData(float weight, float size)
            {
                this.weight = weight;
                this.size = size;
                
                itemID = ++PickableDataMaxID;
            }

            public int ItemID { get { return itemID; } }

            public float Weight { get { return weight; } }
            public float Size { get { return size; } }
        }

        public PickableData CreatePickableData()
        {
            return new PickableData(Weight, Size);
        }
    }
}