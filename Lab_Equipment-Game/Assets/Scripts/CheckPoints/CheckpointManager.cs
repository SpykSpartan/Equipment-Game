using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Checkpoint[] checkpointArray;

    private int currentCheckpoint;

    private void Start()
    {
        currentCheckpoint = 0;
    }

    public void CheckpointReach(Checkpoint checkpoint) //This function is never called. Unsure of how to manage collisions with PlayerController
    {
        if (checkpoint.checkpointNumber > currentCheckpoint)
            currentCheckpoint = checkpoint.checkpointNumber;
    }

    private void Update()
    {
        if (playerTransform.position.y <= checkpointArray[currentCheckpoint].resetHeight)
            playerTransform.position = checkpointArray[currentCheckpoint].resetPositionTo; //SHOULD reset player position to the vector "resetPositionTo", currently does not
    }
}
