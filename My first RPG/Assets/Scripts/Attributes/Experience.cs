using System;
using System.Collections;
using RPG.Saving;
using UnityEngine;

namespace RPG.Attributes
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0;

        //public delegate void ExperienceGainedDelegate();
        public Action onExperiencedGained;

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
            onExperiencedGained();
        }

        internal float GetPoints()
        {
            return experiencePoints;
        }

        public object CaptureState()
        {
            return experiencePoints;
        }

        public void RestoreState(object state)
        {
            experiencePoints = (float)state;
        }

    }
}
