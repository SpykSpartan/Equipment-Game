using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Vector3 resetPositionTo; //Position player gets sent to if they die
    public int checkpointNumber;
    public float resetHeight; //When the player reaches this height, the player's position is reset to resetPositionTo
}
