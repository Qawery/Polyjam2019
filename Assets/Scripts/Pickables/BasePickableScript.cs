using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Polyjam2019.Pickables.BasePickableScriptableObject;

namespace Polyjam2019.Pickables
{
    [RequireComponent(typeof(Collider2D))]
    public class BasePickableScript : MonoBehaviour
    {
        [SerializeField]
        protected BasePickableScriptableObject pickable;

        protected Collider2D triggerer;
        protected PickableData pickableData;

        public float Weight { get { return IsPickableSet? pickable.Weight : 0 ; } }
        public float Size { get { return IsPickableSet? pickable.Size : 0; } }
        public float BasePickupTime { get { return IsPickableSet ? pickable.BasePickupTime : 1f; } }

        private float timeLeft;

        private void Awake()
        {
            if(IsPickableSet)
            {
                pickableData = pickable.CreatePickableData();
            }
        }

        public void StartPickingUp(CharacterEquipment equipment)
        {
            if(equipment.CanFitTheItem(pickableData))
            {
                timeLeft = BasePickupTime;
                StartCoroutine(PickupCoroutine());
            }
        }

        protected void OnPickupCanceled()
        {
            StopAllCoroutines();
        }

        protected IEnumerator PickupCoroutine()
        {
            //yield return new WaitForSeconds(BasePickupTime);

            while(timeLeft > 0)
            {
                Debug.Log(timeLeft);
                timeLeft -= Time.deltaTime;
                yield return null;
            }

            OnPickupEnded();
        }

        protected void OnPickupEnded()
        {
            //triggerer dodaje przedmiot do ekwipunku
            CharacterEquipment equipment = triggerer.GetComponent<CharacterEquipment>();

            if (equipment != null)
            {
                equipment.TryInsertItem(pickableData);

                Destroy(this.gameObject);
                equipment.ItemAvailableForPickup = false;
            }
        }

        protected void OnEntry()
        {
            if(triggerer != null)
            {
                CharacterEquipment equipment = triggerer.GetComponent<CharacterEquipment>();
                equipment.ItemAvailableForPickup = true;
                equipment.CurrentlyAvailableItem = this;
            }
        }

        protected virtual void OnExit()
        {
            OnPickupCanceled();

            if (triggerer != null)
            {
                CharacterEquipment equipment = triggerer.GetComponent<CharacterEquipment>();
                equipment.ItemAvailableForPickup = false;
                equipment.CurrentlyAvailableItem = null;
            }
        }

        protected bool IsPickableSet
        {
            get { return pickable != null; }
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            triggerer = collider;

            OnEntry();
        }

        private void OnTriggerExit2D(Collider2D collider)
        {
            triggerer = null;

            OnExit();
        }
    }
}