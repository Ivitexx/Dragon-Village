using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleDoor : MonoBehaviour
{
    public GameObject Boss;
    public Transform PointSpawn;
    public float time;
    public bool appear = false;
    public GameObject respawnPlayerInBoss;
    private void Update()
    {
        if(appear)
        {
            time += Time.deltaTime;
            if (time > 3)
            {
                Instantiate(Boss, PointSpawn.position, Quaternion.identity);
                appear = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.SetSpawnPoint(respawnPlayerInBoss.transform.position);
            this.gameObject.GetComponent<Animator>().SetBool("Active", true);
            AudioManager.instance.music[0].Stop();
            AudioManager.instance.music[1].Play();
            appear = true;
        }
    }
}
