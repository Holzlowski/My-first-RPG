using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using IC.Core;
using IC.Control;

namespace IC.Cinematics
{
    public class CinematicsControlRemoverIC : MonoBehaviour
    {
        GameObject player;

        void Start()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
            GameObject player = GameObject.FindWithTag("Player");
        }

        void DisableControl(PlayableDirector pd)
        {
           
            player.GetComponent<ActionSchedulerIC>().CancelCurrentAction();
            player.GetComponent<PlayerControllerIC>().enabled = false;
        }

        void EnableControl(PlayableDirector pd)
        {
            player.GetComponent<PlayerControllerIC>().enabled = true;
        }


    }
}
