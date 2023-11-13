using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] GameObject activatedButton;

    private Vector3 storedPosition;
    private Vector3 storedRotation;
    private Vector3 storedScale;
    
    public bool isActivated = false;
    private Door parentDoor;

    private void Start()
    {
        parentDoor = transform.GetComponentInParent<Door>();

        storedPosition = transform.position;
        storedRotation = transform.eulerAngles;
        storedScale = transform.lossyScale;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && !isActivated)
        {
            isActivated = true;
            transform.SetParent(null, true);

            parentDoor.TryOpen();

            activatedButton.SetActive(true);

            activatedButton.transform.position = storedPosition;
            activatedButton.transform.eulerAngles = storedRotation;
            activatedButton.transform.localScale = storedScale;

            gameObject.SetActive(false);
        }
    }
}
