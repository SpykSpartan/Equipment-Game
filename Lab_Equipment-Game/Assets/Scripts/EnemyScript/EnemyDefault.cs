using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDefault : MonoBehaviour
{
    //Default enemy behavior. This does NOT attack. Attacking enemies will inherit.

    public Transform target; //assigned when it sees you, not on awake. This is NOT used for patrols.

    public Rigidbody rb;
    //Its a bit ugly, but this is how im doing all the distance logic.
    public SphereCollider visionCollider;
    public SphereCollider spaceCollider;
    public SphereCollider hurtCollider;
    public GameObject rotateRef;

    //List of locations for the enemy to patrol between when not engaging.
    public List<Vector3> patrol = new List<Vector3>();
    public int patrolNext; //the next patrol target. Equivilent to the next index in the list.
    public bool patrolOverride = false; //if true, it will wander instead of patrolling.
    public float confusedTimer = 0.0f;

    //Logic vars, if the enemy can see you.
    public float visionRadius = 6; //defines vision (how far it checks for player) collider value
    public float personalSpaceRadius = 3; //how close the enemy wants to be. not a collider value.
    public bool seeTarget; //if you are currently within it's vision range.
    public bool engaging = false;
    public float maxAngerLinger = 2.0f;
    public float angerLingerTimer = 0.0f;
    public Animator alertAnim;

    public int health;
    public GameObject deathPrefab;
    //movement vars.
    public float speed;

    
    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        visionCollider = GetComponents<SphereCollider>()[0];
        hurtCollider = GetComponents<SphereCollider>()[1];

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


        //First, check if engaging has changed
        if (ShouldEngage())
        {
            if (engaging == false)
                //engagestart animation
                engaging = true;
        }
        else
        {
            if (engaging == true)
            {
                //disengaged animation
                engaging = false;
            }

        }


        
        if (engaging)
        //Yes, enemy wants to engage player
        {
            ApproachPoint(target); //move towards player

        }
        //No, enemy does not have interest in the player right now
        else if (!patrolOverride) //If patrol override is not enabled
        { 
            ApproachPoint(patrol[patrolNext]);
            if ((transform.position - patrol[patrolNext]).magnitude < 0.1) //if enemy is (approximatly) on top of the patrol point
            {
                
                confusedTimer = 1; //adds a short pause
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
            confusedTimer = 0.0f; //override confusion
            target = other.transform; //Target is assigned here.
            seeTarget = true; //Confirm that player is in range
            angerLingerTimer = maxAngerLinger; //maxes out linger timer.
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            seeTarget = false;
            angerLingerTimer = maxAngerLinger; //redundant, but maxes out linger timer.
            //Target is *not* unassigned here.
        }
    }



    protected virtual void ApproachPoint(Vector3 goal) //i really gotta stop naming variables whatever i feel like...
    {
        transform.position = (Vector3.Lerp(transform.position, goal, Time.deltaTime * speed));
        rotateRef.transform.LookAt(goal);
    }

    protected virtual void ApproachPoint(Transform player) //overload functionality for player, if needed.
    {
        Vector3 playerOffset = new Vector3(player.position.x, player.position.y + 2, player.position.z);
        Vector3 storedgoal;
        rotateRef.transform.LookAt(player, Vector3.up);

        if (Vector3.Distance(player.position, transform.position) - personalSpaceRadius < 0.1 && Vector3.Distance(player.position, transform.position) - personalSpaceRadius > -0.1)
        {
            return;
        }
        
            
        if (Vector3.Distance(player.position, transform.position) > personalSpaceRadius)
        {
            storedgoal = (Vector3.Lerp(transform.position, playerOffset, Time.deltaTime * speed));
        }
        else
        {
            storedgoal = Vector3.MoveTowards(transform.position,( player.position -transform.position).normalized, Time.deltaTime * speed * 10);
        }
        
        if (storedgoal.y < playerOffset.y)
        {
            storedgoal.y = playerOffset.y;
        }
        
        transform.position = storedgoal;
    }

    protected bool ShouldEngage() //bool return on wether enemy should be engaging the player. Yes, i compressed it all cuz its funny.
    {
        return target != null && (seeTarget || (!seeTarget && angerLingerTimer > 0.0f));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "PlayerBullet")
        {
            health--;
            if (health <= 0)
            {
                Instantiate(deathPrefab, transform.position, transform.rotation);
                Destroy(this.gameObject);
            }
        }
    }
}
