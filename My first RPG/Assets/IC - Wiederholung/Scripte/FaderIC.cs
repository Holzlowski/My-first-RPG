using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IC.SceneManagement
{
    public class FaderIC : MonoBehaviour
    {
        CanvasGroup canvasGroup;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public IEnumerator FadeOut(float time)
        {
            while (canvasGroup.alpha < 1) 
            {
                canvasGroup.alpha += Time.deltaTime / time;
                yield return null;  
            }
        }

        public IEnumerator FadeIn(float time)
        {
            while (canvasGroup.alpha > 0) 
            {
                canvasGroup.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }
    }
}
