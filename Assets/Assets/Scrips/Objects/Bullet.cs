using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static Bullet instance;

    private Vector3 target;
    Vector3 dir;

    public bool guided;
    public bool ThreeShoot;

    public float speed = 5f;
    private float timeToDestroy = 10f;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if(ThreeShoot)
        {
            target = transform.forward;
            dir = target;
        }
        else
        {
            target = PlayerController.instance.transform.position;
            target.y = target.y + 1;
            dir = target - transform.position;
        }   
    }
    void Update()
    {        
        if(guided)
        {
            //guided
            Vector3 target = PlayerController.instance.transform.position;
            target.y = target.y + 1;
            Vector3 dir = target - transform.position;
            float distanceThisFrameGuided = speed * Time.deltaTime;
            transform.Translate(dir.normalized * distanceThisFrameGuided, Space.World);
        }
        else
        {
            if(ThreeShoot)
            {
                float distanceThisFrame = speed * Time.deltaTime;
                transform.Translate(dir.normalized * distanceThisFrame, Space.World);
            }
            else
            {
                float distanceThisFrame = speed * Time.deltaTime;
                transform.Translate(dir.normalized * distanceThisFrame, Space.World);
            }
        }

        timeToDestroy -= Time.deltaTime;
        if(timeToDestroy <= 0 || HealthManager.instance.currentHealth == 0f)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "arma" && HealthManager.instance.invecibleCounter <= 0f)
        {
            Destroy(gameObject);
        }

    }
}
