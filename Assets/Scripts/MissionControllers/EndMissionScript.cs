using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Polyjam2019.Pickables.BasePickableScriptableObject;

namespace Polyjam2019
{
    public class EndMissionScript : MonoBehaviour
    {
        public void FinishMission()
        {
            CharacterEquipment equipment = FindObjectOfType<CharacterEquipment>();

            foreach(PickableData data in equipment.GetItemsIterator())
            {

            }
        }
    }
}