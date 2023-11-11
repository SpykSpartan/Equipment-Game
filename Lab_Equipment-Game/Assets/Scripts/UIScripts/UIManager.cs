using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject playerReference;
    [SerializeField] GameObject cameraReference;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject optionsMenu;

    private float storedRotateSpeed;

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

    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
