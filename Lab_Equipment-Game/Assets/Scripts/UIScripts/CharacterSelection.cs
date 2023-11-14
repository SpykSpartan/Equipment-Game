using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public int selectedCharacter = 0;
    [SerializeField] private AnimatorOverrideController[] overrideControllers;
    [SerializeField] private AnimatorScipt overrider;

    public void Set(int value)
    {
        overrider.SetAnimations(overrideControllers[value]);
    }

    public void ChooseCharacter(int character)
    {
        selectedCharacter = character;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
