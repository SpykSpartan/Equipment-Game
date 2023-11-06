using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Transforms for camera
    public Transform target;
    public Transform pivot;

    // Offeset from player
    public Vector3 offset;
    public bool useOffsetValues;

    // Speed of the mouse that rotates the player and camera
    public float rotateSpeed;

    // Restrictions on Camera
    public float maxVeiwAngle;
    public float minVeiwAngle;

    // Invert Up/down
    public bool invertY;

    void Start()
    {
        // checks for if an offset is manually put in
        if(!useOffsetValues)
        {
            offset = target.position - transform.position;
        }
        
        // finds and moves the pivot to the player
        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;

        // locks the mouse (hiding it)
        Cursor.lockState = CursorLockMode.Locked;    
        
    }

    void LateUpdate()
    {
        // get the x position if the mouse & rotate the target
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.Rotate(0, horizontal, 0);

        // get the y position if the mouse & rotate the pivot
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;

        if(invertY)
        {
            pivot.Rotate(vertical, 0, 0);
        }
        else
        {
            pivot.Rotate(-vertical, 0, 0);
        }

        // Limit the up/down camera rotation
        if(pivot.rotation.eulerAngles.x > maxVeiwAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxVeiwAngle, 0, 0);
        }

        if(pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minVeiwAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minVeiwAngle, 0, 0);
        }

        // Move the camera based on the rotation on the target & the original offset
        float nextYAngle = target.eulerAngles.y;
        float nextXAngle = pivot.eulerAngles.x;

        Quaternion rotation = Quaternion.Euler(nextXAngle, nextYAngle, 0);
        transform.position = target.position - (rotation * offset);

        //transform.position = target.position - offset;

        if(transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y - 0.5f, transform.position.z);
        }

        transform.LookAt(target);
    }
}
