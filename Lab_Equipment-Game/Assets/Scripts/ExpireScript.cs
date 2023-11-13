using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpireScript : MonoBehaviour
{
    public float expireTime;
    public float lifeTime;
    private void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime > expireTime)
            Destroy(this.gameObject);
    }
}
