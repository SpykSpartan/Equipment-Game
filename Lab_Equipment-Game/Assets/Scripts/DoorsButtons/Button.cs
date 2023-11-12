using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool isActivated = false;
    private Door parentDoor;

    private void Start()
    {
        parentDoor = transform.GetComponentInParent<Door>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && !isActivated)
        {
            isActivated = true;
            parentDoor.TryOpen();
        }
    }
}
