using UnityEngine;

namespace IC.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "IC/Progression", order = 0)]
    public class ProgressionIC : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClassIC[] characterClass = null;

        [System.Serializable]
        class ProgressionCharacterClassIC
        {
            [SerializeField] CharacterClassIC characterClass;
            [SerializeField] float[] health;
        }
    }
}
