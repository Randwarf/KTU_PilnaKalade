using UnityEngine;

namespace Assets.Scripts.GameCore.Players
{
    public class Player : MonoBehaviour
    {
        public int Health;

        public int Mana;

        public int Defense;

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
