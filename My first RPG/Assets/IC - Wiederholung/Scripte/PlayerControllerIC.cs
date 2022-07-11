using System;
using IC.Combat;
using IC.Core;
using IC.Movement;
using UnityEngine;

namespace IC.Control
{
    public class PlayerControllerIC : MonoBehaviour
    {
        HealthIC health;

        void Start()
        {
            health = GetComponent<HealthIC>();
        }

        private void Update()
        {
           if(health.IsDead()) return;

           if(InteractWithCombat()) return;
           if(InteractWithMovement()) return;
           print("Nothing to do.");
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach(RaycastHit hit in hits)
            {
                CombatTargetIC target = hit.transform.GetComponent<CombatTargetIC>();

                if (target == null) continue; 

                if(!GetComponent<FighterIC>().CanAttack(target.gameObject)) continue;

                if(Input.GetMouseButton(0))
                {
                    GetComponent<FighterIC>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<MoverIC>().StartMoveAction(hit.point, 1f);
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}