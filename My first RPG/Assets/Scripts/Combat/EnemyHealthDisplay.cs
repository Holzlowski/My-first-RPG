using RPG.Attributes;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter fighter;

        private void Awake()
        {
            fighter = GameObject.FindGameObjectWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {
            if(fighter.GetTarget() == null) 
            {
                GetComponent<Text>().text = "N/A";
                return;
            }
            else
            {
                Health enemyHealth = fighter.GetTarget();
                GetComponent<Text>().text = String.Format("{0:0}%", enemyHealth.GetPercentage());
            }
        }    
    }
}


