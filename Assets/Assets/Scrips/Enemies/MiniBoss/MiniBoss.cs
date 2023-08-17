using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBoss : MonoBehaviour
{
    public GameObject keyDroop;
    public Animator Gate;

    public GameObject doorClose;

    public void DeadBoss()
    {
        Instantiate(keyDroop, new Vector3(transform.position.x, transform.position.y + 0.8f, transform.position.z), Quaternion.identity);
        Gate.SetBool("Active", true);
        doorClose.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            this.gameObject.GetComponent<Animator>().SetBool("Active", true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        this.gameObject.GetComponent<Animator>().SetBool("Active", false);
    }
}
