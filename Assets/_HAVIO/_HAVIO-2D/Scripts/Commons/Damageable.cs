using System;
using UnityEngine;

namespace HAVIO
{
    public class Damageable : MonoBehaviour
    {
        [SerializeField]
        private float maxHealth = 100f;

        [ReadOnly]
        [SerializeField]
        private float _currentHealth;
        public float CurrentHealth
        {
            get
            {
                return _currentHealth;
            }
            private set
            {
                _currentHealth = value;
            }
        }

        [ReadOnly]
        [SerializeField]
        private bool isDead = false;

        public Action OnAlive;
        public Action<float> OnDamaged;
        public Action OnDead;

        public void Alive()
        {
            if (isDead == true)
            {
                isDead = false;
                CurrentHealth = maxHealth;
            }
        }

        public void TakeDamage(float damage)
        {
            if (isDead == true)
            {
                return;
            }

            CurrentHealth = Mathf.Max(CurrentHealth - damage, 0);
            OnDamaged?.Invoke(damage);

            if (CurrentHealth == 0)
            {
                Dead();
            }
        }

        private void Dead()
        {
            isDead = true;
            OnDead?.Invoke();
        }
    }
}