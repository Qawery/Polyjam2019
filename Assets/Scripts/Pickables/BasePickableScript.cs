using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polyjam2019.Pickables
{
    [RequireComponent(typeof(Collider2D))]
    public class BasePickableScript : MonoBehaviour
    {
        [SerializeField]
        protected BasePickableScriptableObject pickable;

        protected Collider2D triggerer;

        public float Weight { get { return IsPickableSet? pickable.Weight : 0 ; } }
        public float Size { get { return IsPickableSet? pickable.Size : 0; } }
        public float BasePickupTime { get { return IsPickableSet ? pickable.BasePickupTime : 1f; } }

        protected void OnPickupCanceled()
        {
            StopAllCoroutines();
        }

        protected IEnumerator PickupCoroutine()
        {
            yield return new WaitForSeconds(BasePickupTime);

            OnPickupEnded();
        }

        protected void OnPickupEnded()
        {
            //triggerer dodaje przedmiot do ekwipunku
            CharacterEquipment equipment = triggerer.GetComponent<CharacterEquipment>();

            if (equipment != null && equipment.TryInsertItem(pickable.CreatePickableData()))
            {                
                Destroy(this.gameObject);
                equipment.ItemAvailableForPickup = false;
            }
        }

        protected void OnEntry()
        {
            if(triggerer != null)
            {
                triggerer.GetComponent<CharacterEquipment>().ItemAvailableForPickup = true;
            }
        }

        protected virtual void OnExit()
        {
            OnPickupCanceled();

            if (triggerer != null)
            {
                triggerer.GetComponent<CharacterEquipment>().ItemAvailableForPickup = true;
            }
        }

        protected bool IsPickableSet
        {
            get { return pickable != null; }
        }

        protected void OnTriggerEnter(Collider2D collider)
        {
            triggerer = collider;

            OnEntry();
        }

        protected void OnTriggerExit(Collider2D collider)
        {
            triggerer = null;

            OnExit();
        }
    }
}