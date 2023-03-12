
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IC.Stats
{
    public class BaseStatsIC : MonoBehaviour
    {
        [Range(1,99)]
        [SerializeField] int startingLEvel = 1;
        [SerializeField] CharacterClassIC characterClass;
        [SerializeField] ProgressionIC progression = null;
    }
}
