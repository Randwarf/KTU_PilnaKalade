using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GameCore.Players
{
    public class Player : MonoBehaviour
    {
        public int Health;

        public int Mana;

        public int Defense;

        private List<int> _appliedPoisons;

        private void Start()
        {
            _appliedPoisons = new List<int>();
        }

        public void AddPoison(int poisonDamagePerTurn)
        {
            _appliedPoisons.Add(poisonDamagePerTurn);
        }

        public int ApplyPoisonDamage()
        {
            int totalDamage = 0;

            foreach(var poisonDamage in _appliedPoisons)
            {
                totalDamage += poisonDamage;
                takeDamage(poisonDamage);
            }

            return totalDamage;
        }

        public void ClearPoison()
        {
            _appliedPoisons = new List<int>();
        }

        public void takeDamage(int damage)
        {
            Defense -= damage;
            if(Defense < 0)
            {
                Health += Defense;
                Defense = 0;
            }
        }
    }
}
