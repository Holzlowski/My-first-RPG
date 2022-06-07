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
        
        Transform target;
        float timeSinceLastAttack = 0;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;

            if (!GetIsInrange())
            {
                GetComponent<MoverIC>().MoveTo(target.position);
            }
            else
            {
                GetComponent<MoverIC>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if(timeSinceLastAttack > timeBetweenAttacks)
            {
                // This will trigger the hit event
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;
            }         
        }

        private bool GetIsInrange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTargetIC combatTarget)
        {
            GetComponent<ActionSchedulerIC>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }

        // Animation Event
        void Hit()
        {
            HealthIC healthComponent = target.GetComponent<HealthIC>();
            healthComponent.TakeDamage(weaponDamage);
        }
    }
}