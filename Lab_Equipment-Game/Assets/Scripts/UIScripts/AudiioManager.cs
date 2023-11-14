using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiioManager : MonoBehaviour
{
    public AudioSource ClickSFX;

    public void PlayClick()
    {
        ClickSFX.Play();
    }
}
