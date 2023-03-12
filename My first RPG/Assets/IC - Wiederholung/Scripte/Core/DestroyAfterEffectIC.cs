using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IC.Core
{
    public class DestroyAfterEffectIC : MonoBehaviour
    {

        // Update is called once per frame
        void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}
