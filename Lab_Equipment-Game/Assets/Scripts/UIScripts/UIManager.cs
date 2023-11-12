using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject playerReference;
    [SerializeField] GameObject cameraReference;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] TextMeshProUGUI timerDisplay;
    [SerializeField] TextMeshProUGUI checkpointDisplay;
    [SerializeField] Image tutorialDisplay;

    private float storedRotateSpeed;
    private float time;

    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("manager null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        time += Time.deltaTime;
        timerDisplay.text = "Time:  " + ((int)time / 60) + ":" + ((int)time % 60).ToString("D2");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);

        storedRotateSpeed = cameraReference.GetComponent<CameraController>().rotateSpeed;
        cameraReference.GetComponent<CameraController>().rotateSpeed = 0;
        Cursor.lockState = CursorLockMode.None;

        playerReference.GetComponent<PlayerController>().isPaused = true;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);

        cameraReference.GetComponent<CameraController>().rotateSpeed = storedRotateSpeed;
        Cursor.lockState = CursorLockMode.Locked;

        playerReference.GetComponent<PlayerController>().isPaused = false;
    }

    public void UpdateCheckpointDisplay(int newCheckpoint)
    {
        checkpointDisplay.text = "Checkpoint " + newCheckpoint + "/4";
    }

    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
