using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public int selectedCharacter = 0;

    public void ChooseCharacter(int character)
    {
        selectedCharacter = character;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
