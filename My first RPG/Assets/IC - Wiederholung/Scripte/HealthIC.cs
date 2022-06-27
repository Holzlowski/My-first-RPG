using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IC.Core
{
    public class HealthIC : MonoBehaviour
    {
        [SerializeField] float healtPoints = 100f;

        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }
        
        public void TakeDamage(float damage)
        {
            healtPoints = Mathf.Max(healtPoints - damage, 0);
            if(healtPoints == 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;

            isDead = true;
            {
                GetComponent<Animator>().SetTrigger("die");
                GetComponent<ActionSchedulerIC>().CancelCurrentAction();
            }
            
        }
    }
}

