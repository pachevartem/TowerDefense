using UnityEngine;

namespace CyberCountry
{
    [RequireComponent(typeof(BoxCollider))]
    public class Castle : MonoBehaviour, IReloaded
    {
        private int _health = 100;

        public int Health
        {
            get => _health;
            private set
            {
                if (value < 0)
                {
                    _health = 0;
                }

                if (value > 100)
                {
                    _health = 100;
                }
            }
        }

        void Heal()
        {
            Health += 30;
        }

        void Damage(int value)
        {
            Health -= value;
        }

        public void ReloadGame()
        {
            // Health = 100;
        }
    }
}