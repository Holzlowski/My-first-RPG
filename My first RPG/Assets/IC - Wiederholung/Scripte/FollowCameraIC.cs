using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IC.Core
{
    public class FollowCameraIC : MonoBehaviour
    {
        [SerializeField] Transform target;


        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = target.position;
        }
    }
}
