using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Polyjam2019.Pickables.BasePickableScriptableObject;

namespace Polyjam2019
{
    [RequireComponent(typeof(Collider2D))]
    public class EndMissionScript : MonoBehaviour
    {
        private void FinishMission()
        {
            CharacterEquipment equipment = FindObjectOfType<CharacterEquipment>();
            Dictionary<Resource, int> resourcesGathered = new Dictionary<Resource, int>();

            foreach(PickableData data in equipment.GetItemsIterator())
            {
                if(!resourcesGathered.ContainsKey(data.Resource))
                {
                    resourcesGathered.Add(data.Resource, 0);
                }

                resourcesGathered[data.Resource]++;
            }

            if (BaseState.Instance != null)
            {
                BaseState.Instance.ChangeValuesOfResources(resourcesGathered);
				Application.LoadLevel("Base");
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name.Equals("PlayerCharacter"))
            {
                FinishMission();
            }
        }
	}
}