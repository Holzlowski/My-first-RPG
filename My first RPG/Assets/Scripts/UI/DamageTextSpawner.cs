using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI.DamageText
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] DamageText damageTextPrefab = null;
        //[SerializeField] Transform parent;


        public void Spawn(float damageAmount)
        {  
           DamageText instance = Instantiate<DamageText>(damageTextPrefab, gameObject.transform);
           instance.SetValue(damageAmount);
        }
    }

}