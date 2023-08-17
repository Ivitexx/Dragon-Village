using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseGate : MonoBehaviour
{
    public GameObject gate;
    void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            gate.GetComponent<Animator>().SetBool("Active", false);
        }
    }
}
