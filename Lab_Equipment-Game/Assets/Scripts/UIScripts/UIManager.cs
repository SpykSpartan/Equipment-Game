using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    GameObject playerReference;
    GameObject pauseMenu;
    
    
    public static UIManager instance { get; private set; }

    //Creating UIManager Singleton
    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;

        playerReference = GameObject.Find("Player");
        pauseMenu = GameObject.Find("Pause Menu");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
        playerReference.GetComponent<CharacterController>().enabled = false;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
        playerReference.GetComponent<CharacterController>().enabled = true;
    }
}
