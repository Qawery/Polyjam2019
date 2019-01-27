using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Polyjam2019
{
    public class HealthOrbUIController : MonoBehaviour
    {
        [SerializeField]
        private Slider healthOrbSlider;

        [SerializeField]
        private HealthComponent healthComponent;

        private void Awake()
        {
            healthComponent.OnValueChanged += OnHealthChanged;
            healthOrbSlider.value = 1;
        }

        private void OnHealthChanged(int dummy)
        {
            healthOrbSlider.value = healthComponent.HealthLeftNormalized;
        }

    }
}