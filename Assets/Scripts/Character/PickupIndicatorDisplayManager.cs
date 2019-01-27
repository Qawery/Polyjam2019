using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polyjam2019
{
    public class PickupIndicatorDisplayManager : MonoBehaviour
    {
        private CharacterEquipment equipment;

        [SerializeField]
        private Transform pickupIndicatorTransform;

        [SerializeField]
        private Transform loadingIndicatorTransform;

        [SerializeField]
        private Transform loadingIndicatorMaskTransform;

        private Vector3 initMaskScale = new Vector3(3, 6, 1);
        private Vector3 zeroMaskScale = new Vector3(3, 0, 1);

        private void Awake()
        {
            equipment = GetComponent<CharacterEquipment>();
        }

        private void Update()
        {
            if(equipment.ItemAvailableForPickup && equipment.CurrentlyAvailableItem != null)
            {
                if(equipment.CurrentlyAvailableItem.PickingUpProgressNormalized < 1)
                {
                    if (pickupIndicatorTransform.gameObject.activeSelf)
                    {
                        pickupIndicatorTransform.gameObject.SetActive(false);
                    }

                    if(!loadingIndicatorTransform.gameObject.activeSelf)
                    {
                        loadingIndicatorTransform.gameObject.SetActive(true);
                    }

                    loadingIndicatorMaskTransform.localScale =
                        Vector3.Lerp(initMaskScale, zeroMaskScale,  equipment.CurrentlyAvailableItem.PickingUpProgressNormalized);
                }
                else
                {
                    pickupIndicatorTransform.gameObject.SetActive(true);
                    loadingIndicatorTransform.gameObject.SetActive(false);
                }

            }
            else
            {
                pickupIndicatorTransform.gameObject.SetActive(false);
                loadingIndicatorTransform.gameObject.SetActive(false);
                loadingIndicatorMaskTransform.localScale = initMaskScale;
            }
        }
    }
}