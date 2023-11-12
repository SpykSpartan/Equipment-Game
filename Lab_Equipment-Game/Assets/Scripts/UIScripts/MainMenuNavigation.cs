using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuNavigation : MonoBehaviour
{
    [SerializeField] GameObject startButtons;
    [SerializeField] GameObject characterSelect;

    public void StartCharacterSelect()
    {
        startButtons.SetActive(false);
        characterSelect.SetActive(true);
    }
}
