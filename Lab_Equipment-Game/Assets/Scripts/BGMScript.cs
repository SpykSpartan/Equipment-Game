using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMScript : MonoBehaviour
{
    public AudioClip defaultAudio;

    public AudioClip intenseAudio;

    public bool audioIsIntense;
    public float angryEnemies = 0;


    public void Awake()
    {
        GetComponent<AudioSource>().clip = defaultAudio;
        GetComponent<AudioSource>().Play();
    }
    public void AdjustAudio(int change)
    {
        angryEnemies += change;
        if (angryEnemies < 0)
        {
            angryEnemies = 0;
        }
        if (angryEnemies > 0)
        {
            if (!audioIsIntense)
            {
                GetComponent<AudioSource>().clip = intenseAudio;
                GetComponent<AudioSource>().Play();
                audioIsIntense = true;
            }
        }
        
        if (angryEnemies == 0)
        {
            if (audioIsIntense)
            {
                GetComponent<AudioSource>().clip = defaultAudio;
                GetComponent<AudioSource>().Play();
                audioIsIntense = false;
            }
        }
    }
}
