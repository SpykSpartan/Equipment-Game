using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] GameObject activatedButton;
    [SerializeField] Renderer lantern;

    [SerializeField] Material metalMaterial;
    [SerializeField] Material unlitMaterial;

    private Material[] materialArray;

    private Vector3 storedPosition;
    private Vector3 storedRotation;
    private Vector3 storedScale;
    
    public bool isActivated = false;
    private Door parentDoor;
    
    public AudioSource buttonSFX;

    private void Start()
    {
        parentDoor = transform.GetComponentInParent<Door>();

        storedPosition = transform.position;
        storedRotation = transform.eulerAngles;
        storedScale = transform.lossyScale;

        materialArray = new Material[2];
        materialArray[0] = metalMaterial;
        materialArray[1] = unlitMaterial;
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

            lantern.materials = materialArray;

            gameObject.SetActive(false);
            buttonSFX.Play();
        }
    }
}
