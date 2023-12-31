using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Button[] buttonArray;
    private int activatedButtonsCount;

    public AudioSource GateSFX;

    private void Start()
    {
        buttonArray = new Button[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            buttonArray[i] = transform.GetChild(i).GetComponent<Button>();
    }

    public void TryOpen() //Called each time a child button is activated
    {
        activatedButtonsCount = 0;
        for (int i = 0; i < buttonArray.Length; i++)
        {
            if (buttonArray[i].isActivated)
                activatedButtonsCount++;
        }

        if (activatedButtonsCount == buttonArray.Length)
            OpenDoor();
            GateSFX.Play();
    }

    private void OpenDoor()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y - 15, transform.position.z); //TEMPORARY - update when animation ready
        StartCoroutine(OpenAnimation());
    }

    private IEnumerator OpenAnimation()
    {
        float height = transform.position.y;
        
        for (float targetHeight = transform.position.y; targetHeight <= (transform.position.y + 15); targetHeight += 0.1f)
        {
            height = targetHeight;
            transform.position = new Vector3(transform.position.x, height, transform.position.z);
            yield return new WaitForSeconds(0.01f);
        }
    }
}
