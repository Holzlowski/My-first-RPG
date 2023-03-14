using RPG.Saving;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSafeFile = "save";
        //const string defaultSafeFile = null;

        [SerializeField] float fadeInTime = 0.2f;

        Fader fader;

        private void Start()
        {
            fader = FindObjectOfType<Fader>(); // Link fader to the Fader script.
            StartCoroutine(LoadLastScene());
        }

        IEnumerator LoadLastScene()
        {
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSafeFile); // Get what the last loaded scene was.
            fader.FadeOutImmediate(); // Perform an immediate fade out.
            yield return fader.FadeIn(fadeInTime); // Fade back in.
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
            if (Input.GetKeyDown(KeyCode.Delete))
            {
                Delete();
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

        public void Delete()
        {
            GetComponent<SavingSystem>().Delete(defaultSafeFile);
        }
    }
}

