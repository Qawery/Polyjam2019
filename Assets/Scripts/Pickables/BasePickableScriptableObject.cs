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
		
		[SerializeField][Range(1, 5)]
		protected int amount = 1;

		[SerializeField]
        protected Resource resource;

        public float Weight { get { return weight; } }
        public float Size { get { return size; } }
        public float BasePickupTime { get { return basePickupTime; } }
		public int Amount { get { return amount; } }
        public Resource Resource { get { return resource; } }

        [System.Serializable]
        public class PickableData
        {            
            private static int PickableDataMaxID = 0;            

            internal PickableData(Resource resource, float weight, float size, int amount)
            {
                Weight = weight;
                Size = size;
                Resource = resource;
				Amount = amount;
                ItemID = ++PickableDataMaxID;
            }

            public int ItemID { get; private set; }
            public float Weight { get; private set; }
            public float Size { get; private set; }
			public int Amount { get; private set; }
            public Resource Resource { get; private set; }
        }

        public PickableData CreatePickableData()
        {
            return new PickableData(Resource, Weight, Size, Amount);
        }
    }
}