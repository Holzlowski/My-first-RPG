using UnityEngine;
using IC.Movement;
using IC.Core;

namespace IC.Combat
{
    public class FighterIC : MonoBehaviour, ICAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] float timeBetweenAttacks = 1f;
        
        HealthIC target;
        float timeSinceLastAttack = Mathf.Infinity;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if(target.IsDead()) return; 

            if (!GetIsInrange())
            {
                GetComponent<MoverIC>().MoveTo(target.transform.position, 1f);
            }
            else
            {
                GetComponent<MoverIC>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if(timeSinceLastAttack > timeBetweenAttacks)
            {
                // This will trigger the hit event
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) return false;
            HealthIC targetToTest = combatTarget.GetComponent<HealthIC>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionSchedulerIC>().StartAction(this);
            target = combatTarget.transform.GetComponent<HealthIC>();
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
            GetComponent<MoverIC>().Cancel();
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }


        private bool GetIsInrange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        // Animation Event
        void Hit()
        {
            if (target == null) return;
            target.TakeDamage(weaponDamage);
        }
    }
}