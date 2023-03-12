using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace IC.Cinematics
{
    public class CinematicTriggerIC : MonoBehaviour
    {
        private bool alreadyTriggered = false;

        private void OnTriggerEnter(Collider other)
        {
            if(!alreadyTriggered && other.gameObject.tag == "Player")
            {
                alreadyTriggered = true;
                GetComponent<PlayableDirector>().Play();
            }
        }
    }
}

