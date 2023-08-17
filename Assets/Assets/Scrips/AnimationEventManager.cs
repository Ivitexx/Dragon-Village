using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventManager : MonoBehaviour
{
    public void PlayerOutAttacking()
    {
        PlayerController.instance.isAttacking = false;
    }

    public void EnemyOutAttacking()
    {
        EnemyController.instance.attacking = false;

    }

    public void EnemyInAttacking()
    {
        EnemyController.instance.attacking = true;

        /*
        if(EnemyController.instance.animator.GetBool("InAttacking") == false)
        {
            EnemyController.instance.animator.SetBool("InAttacking", true);
        }
        else
        {
            EnemyController.instance.animator.SetBool("InAttacking", false);
        }
        */
    }
}
