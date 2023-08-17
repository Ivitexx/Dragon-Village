using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationObjects : MonoBehaviour
{
    public static AnimationObjects instance;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            this.gameObject.GetComponent<Animator>().SetBool("Active", true);
        }
    }
}
