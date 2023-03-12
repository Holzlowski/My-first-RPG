using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IC.Combat
{
    public class WeaponPickUpIC : MonoBehaviour
    {
        [SerializeField] WeaponIC weapon = null;
        [SerializeField] float respawnTime = 5;

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                other.GetComponent<FighterIC>().EquipWeapon(weapon);
                StartCoroutine(HideForSeconds(respawnTime));
            }
        }

        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickUp(false);
            yield return new WaitForSeconds(seconds);
            ShowPickUp(true);
        }

        private void ShowPickUp(bool shouldShow)
        {
            GetComponent<Collider>().enabled = true;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
        }
    }
}
