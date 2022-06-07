using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IC.Core
{
    public class ActionSchedulerIC : MonoBehaviour
    {
        ICAction currentAction;
        public void StartAction(ICAction action)
        {
            if (currentAction == action) return;
            if(currentAction != null)
            {
                currentAction.Cancel();
            }
;            currentAction = action;
        }
    }
}


