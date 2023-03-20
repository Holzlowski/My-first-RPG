using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using IC.SceneManagement;
using RPG.Control;

namespace IC.Scenemanagement
{
    public class PortalIC : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D, E, F
        }

        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 2f;
        [SerializeField] float fadeWaitTime = 0.5f;

       private void OnTriggerEnter(Collider other)
        {
            if(other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if(sceneToLoad < 0)
            {
                Debug.LogError("Scene to load not set");
                yield break;
            }

            DontDestroyOnLoad(gameObject);

            FaderIC fader = FindObjectOfType<FaderIC>();

            yield return fader.FadeOut(fadeOutTime);

            SavingWrapperIC wrapper = FindObjectOfType<SavingWrapperIC>();
            wrapper.Save();

            yield return SceneManager.LoadSceneAsync(sceneToLoad);

            wrapper.Load();

            PortalIC otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);

            wrapper.Save();

            yield return new WaitForSeconds(fadeWaitTime);
            yield return fader.FadeIn(fadeInTime);

            Destroy(gameObject);
        }

        private void UpdatePlayer(PortalIC otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            //weil Spieler sonst irgendwo hinteleportiert wird, wegen dem NavMesh
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
            player.transform.position = otherPortal.spawnPoint.transform.position;
            player.transform.rotation = otherPortal.spawnPoint.transform.rotation;
        }

        private PortalIC GetOtherPortal()
        {
            foreach(PortalIC portal in FindObjectsOfType<PortalIC>())
            {
                if (portal == this) continue;
                if (portal.destination != destination) continue;
                return portal;
            }
            return null;
        }
    }
}


