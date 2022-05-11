using System;
using IC.Combat;
using IC.Movement;
using UnityEngine;

namespace IC.Control
{
    public class PlayerControllerIC : MonoBehaviour
    {

        private void Update()
        {
           InteractWithCombat();
           InteractWithMovement();
        }

        private void InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach(RaycastHit hit in hits)
            {
                CombatTargetIC target = hit.transform.GetComponent<CombatTargetIC>();
                if(target == null) continue;

                if(Input.GetMouseButtonDown(0))
                {
                    GetComponent<FighterIC>().Attack(target);
                }
            }
        }

        private void InteractWithMovement()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit)
            {
                GetComponent<MoverIC>().MoveTo(hit.point);
            }
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}