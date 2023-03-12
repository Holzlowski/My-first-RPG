using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using IC.SceneManagement;

namespace IC.Scenemanagement
{
    public class SavingWrapperIC : MonoBehaviour
    {
        const string defaultSaveFile = "save";
        [SerializeField] float fadeTimeIn = 0.2f;

        IEnumerator Start()
        {
            FaderIC fader = FindObjectOfType<FaderIC>();
            fader.FadeOutImmediate();
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
            yield return fader.FadeIn(fadeTimeIn);
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.L))
            {
                Load();
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                Save();
            }
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }
    }
}
