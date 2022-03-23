using UnityEngine;

namespace Assets.Scripts.UI.Stats
{
    public class StatusEffect : MonoBehaviour
    {
        public StatusEffectType Type;
        public float LifeTime;

        void Start()
        {
            Destroy(gameObject, LifeTime);
        }
    }
}
