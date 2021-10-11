using RPG.Attributes;
using UnityEngine;

namespace RPG.Combat
{
    //if you place a CombatTarget Component it will automatically place a Health component
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour
    {

    }
}