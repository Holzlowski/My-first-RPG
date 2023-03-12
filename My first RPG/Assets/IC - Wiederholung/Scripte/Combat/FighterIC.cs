using UnityEngine;
using IC.Movement;
using IC.Core;
using System;
using RPG.Saving;

namespace IC.Combat
{
    public class FighterIC : MonoBehaviour, ICAction, ISaveable
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] WeaponIC defaultWeapon = null;
        [SerializeField] string defaultWeaponName = "Unarmed";

        HealthIC target;
        float timeSinceLastAttack = Mathf.Infinity;
        WeaponIC currentWeapon = null;

        private void Start()
        {
            if (currentWeapon == null)
            {
                EquipWeapon(defaultWeapon);
            }
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (target.IsDead()) return;

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
        
        public void EquipWeapon(WeaponIC weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if (timeSinceLastAttack > timeBetweenAttacks)
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
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetRange();
        }

        // Animation Event
        void Hit()
        {
            if (target == null) return;

            if(currentWeapon.HasProjectole())
            {
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
            }
            else
            {
                target.TakeDamage(currentWeapon.GetDamage());
            }
        }

        void Shoot()
        {
            Hit();
        }

        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            WeaponIC weapon = Resources.Load<WeaponIC>(weaponName);
            EquipWeapon(weapon);
        }

    }
}