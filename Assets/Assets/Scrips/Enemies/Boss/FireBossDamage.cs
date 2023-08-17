using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBossDamage : MonoBehaviour
{

    public float time;

    void Update()
    {
        time += Time.deltaTime;
    }
    private void OnTriggerStay(Collider other)
    {
        if (time > 0.1)
        {
            if (other.name == "Player")
            {
                PlayerController.instance.isDefend = false;
                HealthManager.instance.Hurt();

                time = 0f;
            }
        }
    }
}
