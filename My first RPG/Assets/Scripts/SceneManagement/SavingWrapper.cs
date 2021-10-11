using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        //const string defaultSafeFile = "save";
        const string defaultSafeFile = null;

        [SerializeField] float fadeInTime = 0.2f;

        IEnumerator Start()
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();
           yield return GetComponent<SavingSystem>().LoadLastScene(defaultSafeFile);
            yield return fader.FadeIn(fadeInTime);
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
        }

        public void Save()
        {
            if(defaultSafeFile != null)
            {
                GetComponent<SavingSystem>().Save(defaultSafeFile);
            }        
        }

        public void Load()
        {
            if (defaultSafeFile != null)
            {
                GetComponent<SavingSystem>().Load(defaultSafeFile);
            }             
        }
    }
}

