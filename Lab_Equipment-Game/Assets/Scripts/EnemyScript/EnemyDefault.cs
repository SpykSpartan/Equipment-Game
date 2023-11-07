using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDefault : MonoBehaviour
{
    //Default enemy behavior. This does NOT attack. Attacking enemies will inherit.

    public Transform target; //assigned when it sees you, not on awake. This is NOT used for patrols.

    //Its a bit ugly, but this is how im doing all the distance logic.
    public SphereCollider visionCollider;
    public SphereCollider hurtCollider;

    //List of locations for the enemy to patrol between when not engaging.
    public List<Vector3> patrol = new List<Vector3>();
    public int patrolNext; //the next patrol target. Equivilent to the next index in the list.
    public bool patrolOverride = false; //if true, it will wander instead of patrolling.
    public float confusedTimer = 0.0f;

    //Logic vars, if the enemy can see you.
    public float visionRadius = 10; //defines vision (how far it checks for player)
    public bool seeTarget; //if you are currently within it's vision range.
    public float maxAngerLinger = 2.0f;
    public float angerLingerTimer = 0.0f;

    //movement vars.
    public float speed;

    
    private void Awake()
    {
        
        visionCollider = GetComponents<SphereCollider>()[0];
        hurtCollider = GetComponents<SphereCollider>()[1];
        Debug.Log(visionCollider.radius);
        Debug.Log(hurtCollider.radius);

        //Applies collider radii to the colliders.
        visionCollider.radius = visionRadius;


        //Some patrol behavior so you dont need to change scripts.
        if (patrol.Count == 0)
        {
            patrol.Add(transform.position); //add a patrol location at it's starting location. Usefull when disengaging.
        }
    }

    private void Update()
    {
        //ConfusedTimer should be done first i guess.
        if (confusedTimer > 0)
            confusedTimer -= Time.deltaTime;

        //This way, you can implement stunning by adding to confusedTimer :)
        if (confusedTimer > 0)
        {
            return;
        }

        //if player not in vision range, start decreasing anger timer.
        if (!seeTarget && angerLingerTimer > 0) angerLingerTimer -= Time.deltaTime; 
        
        
        //First, check if enemy wants to approach player
        if (ShouldEngage())
        //Yes, enemy wants to engage player
        {
            ApproachPoint(target.position); //move towards player

        }
        //No, enemy does not have interest in the player right now
        else if (!patrolOverride) //If patrol override is not enabled
        { 
            ApproachPoint(patrol[patrolNext]);
            if ((transform.position - patrol[patrolNext]).magnitude < 0.1) //if enemy is (approximatly) on top of the patrol point
            {
                Debug.Log("Test Message- Enemy Reached patrol point");
                confusedTimer = 1; //adds a short pause
                Debug.Log("Test Message- Enemy is Confused");
                patrolNext += 1;
                if (patrolNext > (patrol.Count - 1)) //if you ahve completed the patrol list, flip back to 0
                    patrolNext = 0;


            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("TestMessage- Player Detected by Enemy");
            target = other.transform; //Target is assigned here.
            seeTarget = true; //Confirm that player is in range
            angerLingerTimer = maxAngerLinger; //maxes out linger timer.
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("TestMessage- Player Exited Enemy Detection");
            seeTarget = false;
            angerLingerTimer = maxAngerLinger; //redundant, but maxes out linger timer.
            //Target is *not* unassigned here.
        }
    }

    protected virtual void ApproachPoint(Vector3 goal) //i really gotta stop naming variables whatever i feel like...
    {
        transform.position = Vector3.Lerp(transform.position, goal, Time.deltaTime * speed);
    }

    private bool ShouldEngage() //bool return on wether enemy should be engaging the player. Yes, i compressed it all cuz its funny.
    {
        return target != null && (seeTarget || (!seeTarget && angerLingerTimer > 0.0f));
    }


}
