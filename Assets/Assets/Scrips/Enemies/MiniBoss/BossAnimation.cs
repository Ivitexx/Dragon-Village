using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    public void MinibossOutAttacking()
    {
        MiniBossController.instance.attacking = false;

    }

    public void MinibossInAttacking()
    {
        MiniBossController.instance.attacking = true;
        
        /*
        if (MiniBossController.instance.animator.GetBool("InAttacking") == false)
        {
            MiniBossController.instance.animator.SetBool("InAttacking", true);
        }
        else
        {
            MiniBossController.instance.animator.SetBool("InAttacking", false);
        }
        */
    }
}
