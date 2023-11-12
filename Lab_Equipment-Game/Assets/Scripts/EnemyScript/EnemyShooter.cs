using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShooter : EnemyDefault
{
    //Modified behavior to add shooting.
    [SerializeField]
    private GameObject barrel;//used for rotating.
    public GameObject bulletRef; //im mostly riffing off player shots, but its a seperate prefab.
    [SerializeField]
    private GameObject bulletSpawnRef;//spawn location of bullet.
    [SerializeField]
    private GameObject laserSight; //im trying something new for the enemies; a laser sight showing where they are aiming.

    [SerializeField]
    private float bulletChargeMax; //how long for the bullet to fire
    private float currentCharge;

    private new void Awake()
    {
        base.Awake(); //all the classics.
        currentCharge = bulletChargeMax; //set charge to max charge
        laserSight.GetComponent<EnemyLaserScript>().maxChargeTimer = bulletChargeMax;
        laserSight.SetActive(false);
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
        if (!seeTarget && angerLingerTimer > 0)
        {
            angerLingerTimer -= Time.deltaTime;
            laserSight.SetActive(false); //quick fix, i know its ugly...
        }

        //First, check if engaging has changed
        if (ShouldEngage())
        {
            if (engaging == false)
                alertAnim.SetTrigger("Alerted");
                engaging = true;
        }
        else
        {
            if (engaging == true)
            {
                alertAnim.SetTrigger("Confused");
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
                Debug.Log("Test Message- Enemy Reached patrol point");
                confusedTimer = 1; //adds a short pause
                Debug.Log("Test Message- Enemy is Confused");
                patrolNext += 1;
                if (patrolNext > (patrol.Count - 1)) //if you ahve completed the patrol list, flip back to 0
                    patrolNext = 0;


            }
        }

    }
    



    
    protected new void ApproachPoint(Transform player) 
    {
        base.ApproachPoint(player); //normal orbitting motion mechanic.
        //check if enemy is close enough to consider shooting
        if (Vector3.Distance(player.position, transform.position) - personalSpaceRadius < 0.1) 
        {
            currentCharge -= Time.deltaTime; //run down timer
            if (currentCharge < bulletChargeMax - 0.5)
            {
                laserSight.SetActive(true); 
                laserSight.GetComponent<EnemyLaserScript>().target = this.target;
                laserSight.GetComponent<EnemyLaserScript>().chargeTimer = currentCharge;
            }
            else
                laserSight.SetActive(false);
            if (currentCharge <= 0.0f)
            {
                Fire();
                currentCharge = bulletChargeMax;
            }
        }
        else
        {
            if (currentCharge < bulletChargeMax)
            {
                currentCharge += Time.deltaTime / 2;
            }
        }
    }

    protected void Fire()
    {
        //Instantiate(bulletRef, bulletSpawnRef.transform.position, bulletSpawnRef.transform.rotation);
    }
    


}
