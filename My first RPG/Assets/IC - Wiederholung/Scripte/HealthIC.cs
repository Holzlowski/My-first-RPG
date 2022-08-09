using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace IC.Core
{
    public class HealthIC : MonoBehaviour, ISaveable
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

        public object CaptureState()
        {
            return healtPoints;
        }

        public void RestoreState(object state)
        {
            healtPoints = (float)state;

            if(healtPoints == 0)
            {
                Die();
            }
        }
    }
}

