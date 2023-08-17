using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && UIManager.instance.key.activeInHierarchy == true)
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("Active");
            UIManager.instance.key.SetActive(false);
        }
    }
}
