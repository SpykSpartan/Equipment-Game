using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserScript : MonoBehaviour
{
    public Transform origin;
        public Transform target;

    LineRenderer laserLine;

    public float maxChargeTimer; //assigned on awake by EnemyShooter...
    public float chargeTimer;

    private float storeWidth;
    public Material color1; //default
    public Material color2; //right before shooting.

    private void Awake()
    {
        laserLine = GetComponent<LineRenderer>();

        laserLine.SetPosition(0, origin.position);
        if (target != null)
        laserLine.SetPosition(1, target.position);
    }

    private void Update()
    {
        if (target != null)
        {
            laserLine.SetPosition(0, origin.position);
            laserLine.SetPosition(1, target.position);
            LineUpdate();
        }

    }

    private void LineUpdate()
    {
        laserLine.startWidth = 0.05f;
        laserLine.endWidth = 0.05f;
        if (chargeTimer < 0.3f)
        {
            laserLine.material = color2;
        }
        else
            laserLine.material = color1;
        if (chargeTimer < 0.2f)
        {
            laserLine.startWidth = 0.3f;
            laserLine.endWidth = 0.3f;
        }
    }

    
}
