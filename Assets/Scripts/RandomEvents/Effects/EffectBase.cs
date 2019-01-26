using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polyjam2019
{
    public abstract class EffectBase : ScriptableObject
    {
        public abstract void ExecuteEffect(GameObject gameObject);
    }
}