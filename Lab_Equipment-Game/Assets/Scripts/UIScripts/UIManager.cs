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
    [SerializeField] GameObject winScreen;
    [SerializeField] TextMeshProUGUI finalTimerText;
    [SerializeField] GameObject lossScreen;

    [SerializeField] Transform healthBar;
    [SerializeField] TextMeshProUGUI timerDisplay;
    [SerializeField] TextMeshProUGUI checkpointDisplay;

    [SerializeField] List<GameObject> tutorialPrompts;

    private float storedRotateSpeed;
    private float time;
    private int currentPrompt;

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

    private void Start()
    {
        TutorialPrompt(tutorialPrompts[0]);
        currentPrompt = 0;
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

        if (newCheckpoint == 1)
        {
            TutorialPrompt(tutorialPrompts[1]);
            currentPrompt = 1;
        }
        if (newCheckpoint == 3)
        {
            TutorialPrompt(tutorialPrompts[2]);
            currentPrompt = 2;
        }
    }

    public void LoadScene(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    private void TutorialPrompt(GameObject prompt)
    {
        prompt.SetActive(true);
        StartCoroutine("Fade");
    }

    private IEnumerator Fade()
    {
        yield return new WaitForSeconds(4f);
        
        Color imageColor = tutorialPrompts[currentPrompt].GetComponent<Image>().color;
        Color textColor = tutorialPrompts[currentPrompt].GetComponentInChildren<TextMeshProUGUI>().color;

        for (float alpha = 0.47f; alpha >= 0; alpha -= 0.1f)
        {
            imageColor.a = alpha;
            tutorialPrompts[currentPrompt].GetComponent<Image>().color = imageColor;

            textColor.a = alpha;
            tutorialPrompts[currentPrompt].GetComponentInChildren<TextMeshProUGUI>().color = textColor;

            yield return new WaitForSeconds(0.1f);
        }

        tutorialPrompts[currentPrompt].SetActive(false);
    }

    public void TakeDamage()
    {
        if (healthBar.childCount != 1)
        {
            Destroy(healthBar.GetChild(healthBar.childCount - 1).gameObject);
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Time.timeScale = 0;
        lossScreen.SetActive(true);

        storedRotateSpeed = cameraReference.GetComponent<CameraController>().rotateSpeed;
        cameraReference.GetComponent<CameraController>().rotateSpeed = 0;
        Cursor.lockState = CursorLockMode.None;

        playerReference.GetComponent<PlayerController>().isPaused = true;
    }

    public void GameWon()
    {
        Time.timeScale = 0;
        winScreen.SetActive(true);
        finalTimerText.text = "TIME:  " + ((int)time / 60) + ":" + ((int)time % 60).ToString("D2");

        storedRotateSpeed = cameraReference.GetComponent<CameraController>().rotateSpeed;
        cameraReference.GetComponent<CameraController>().rotateSpeed = 0;
        Cursor.lockState = CursorLockMode.None;

        playerReference.GetComponent<PlayerController>().isPaused = true;
    }
}
