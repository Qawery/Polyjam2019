using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Polyjam2019.Pickables;
using static Polyjam2019.Pickables.BasePickableScriptableObject;

namespace Polyjam2019
{
    public class CharacterEquipment : MonoBehaviour
    {
        private Dictionary<int, PickableData> itemsInEquipment;

        private float totalWeight;
        private float totalSize;

        [SerializeField]
        private float maxWeight;

        [SerializeField]
        private float maxSize;

        [SerializeField]
        private bool limitWeight;

        [SerializeField]
        private bool limitSize;        

        private void Awake()
        {
            itemsInEquipment = new Dictionary<int, PickableData>();
            totalSize = 0;
            totalWeight = 0;
        }

        private void Update()
        {
            
        }

        public void PickItem()
        {
            if(CurrentlyAvailableItem != null)
            {
                CurrentlyAvailableItem.StartPickingUp(this);
            }
        }

        public bool CanFitTheItem(PickableData itemData)
        {
            return
                (!limitSize || totalSize + itemData.Size <= maxSize) &&
                (!limitWeight || totalWeight + itemData.Weight <= maxWeight);
        }

        public bool TryInsertItem(PickableData itemData)
        {
            bool result = !itemsInEquipment.ContainsKey(itemData.ItemID) && CanFitTheItem(itemData);

            if(result)
            {
                itemsInEquipment.Add(itemData.ItemID, itemData);
                totalSize += itemData.Size;
                totalWeight += itemData.Weight;
            }

            return result;
        }

        public PickableData RemoveItemFromInventory(int itemID)
        {
            bool contains = itemsInEquipment.ContainsKey(itemID);
            PickableData result = contains ? itemsInEquipment[itemID] : null;

            if(contains)
            {
                itemsInEquipment.Remove(itemID);
                totalSize -= result.Size;
                totalWeight -= result.Weight;
            }

            return result;
        }

        public IEnumerable<PickableData> GetItemsIterator()
        {
            if(itemsInEquipment != null)
            {
                foreach(PickableData data in itemsInEquipment.Values)
                {
                    yield return data;
                }
            }
        }

        [HideInInspector]
        private bool itemAvailableForPickup = false;

        public bool ItemAvailableForPickup
        {
            get
            {
                return itemAvailableForPickup;
            }
            set
            {
                itemAvailableForPickup = value;
                Debug.Log("Item available: " + itemAvailableForPickup);
            }
        }

        [HideInInspector]
        public BasePickableScript CurrentlyAvailableItem = null;
    }
}