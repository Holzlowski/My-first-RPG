using RPG.Core;
using RPG.Movement;
using System.Collections;
using UnityEngine;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 10f;

        float timeSinceLastAttack = Mathf.Infinity;

        Mover mover;
        Health target;

        private void Start()
        {
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;

            if (target.IsDead()) return;

            if (!GetIsInRange())
            {
                mover.MoveTo(target.transform.position);
            }
            else
            {
                mover.Cancel();
                AttackBehavior();
            }
        }

        private void AttackBehavior()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                // this will trigger the Hit() event
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            this.GetComponent<Animator>().ResetTrigger("stopAttack");
            this.GetComponent<Animator>().SetTrigger("attack");
        }

        //Animation Event
        void Hit()
        {
            if(target == null) { return; }
            target.TakeDamage(weaponDamage);
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null) { return false; }
            Health targetToTest = combatTarget.GetComponent<Health>();
            return targetToTest != null && !targetToTest.IsDead();
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StoppAttack();
            target = null;
        }

        private void StoppAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("stopAttack");
        }
    }
}