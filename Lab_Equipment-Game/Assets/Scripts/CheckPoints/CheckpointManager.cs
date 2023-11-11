using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    private Checkpoint[] checkpointArray;

    private int currentCheckpoint;

    private void Start()
    {
        currentCheckpoint = 0;

        checkpointArray = new Checkpoint[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            checkpointArray[i] = transform.GetChild(i).GetComponent<Checkpoint>();
    }

    public void CheckpointReach(Checkpoint checkpoint)
    {
        if (checkpoint.checkpointNumber > currentCheckpoint)
            currentCheckpoint = checkpoint.checkpointNumber;
    }

    private void Update()
    {
        if (playerTransform.position.y <= checkpointArray[currentCheckpoint].resetHeight)
        {
            playerTransform.gameObject.GetComponent<CharacterController>().enabled = false;
            playerTransform.position = checkpointArray[currentCheckpoint].resetPositionTo;
            playerTransform.gameObject.GetComponent<CharacterController>().enabled = true;
        }
    }
}
