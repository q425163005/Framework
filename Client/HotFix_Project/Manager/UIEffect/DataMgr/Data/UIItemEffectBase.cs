using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace HotFix_Project.UIEffect
{
    public abstract class UIItemEffectBase
    {
        public GameObject target;
        protected UIItemEffectEnum EffectType;

        public abstract void TriggerEffect();
        public abstract void ResetEffect();
    }
}
