using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Checkpoint[] checkpointArray;

    private int currentCheckpoint;

    public void CheckpointReach(Checkpoint checkpoint)
    {
        if (checkpoint.checkpointNumber > currentCheckpoint)
            currentCheckpoint = checkpoint.checkpointNumber;
    }

    private void Update()
    {
        if (playerTransform.position.y <= checkpointArray[currentCheckpoint].resetHeight)
            playerTransform.position = checkpointArray[currentCheckpoint].resetPositionTo;
    }
}
