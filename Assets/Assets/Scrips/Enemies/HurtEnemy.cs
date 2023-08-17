using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.hurtEnemy);
            other.GetComponent<EnemyHealthManager>().TakeDamage();
            other.GetComponent<EnemyController>().animator.SetTrigger("GetHit");
        }
        else if (other.tag == "MiniBoss")
        {
            other.GetComponent<MiniBossHealthManager>().TakeDamage();
            other.GetComponent<MiniBossController>().animator.SetTrigger("GetHit");
        }
    }
}
