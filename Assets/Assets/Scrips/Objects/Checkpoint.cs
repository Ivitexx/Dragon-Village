using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public GameObject CheckpointON;
    public GameObject CheckpointOFF;

    public GameObject Base;

    void Start()
    {
        CheckpointOFF.SetActive(true);
        CheckpointON.SetActive(false);
        Base.SetActive(false);
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && CheckpointON.activeInHierarchy == false)
        {
            AudioManager.instance.PlaySFX(AudioManager.instance.checkPoint);

            GameManager.instance.SetSpawnPoint(transform.position);

            Checkpoint[] allCheckpoint = FindObjectsOfType<Checkpoint>();

            foreach(Checkpoint checkpoint in allCheckpoint)
            {
                checkpoint.CheckpointOFF.SetActive(true);
                checkpoint.CheckpointON.SetActive(false);
            }

            HealthManager.instance.ResetHealth();

            CheckpointOFF.SetActive(false);
            CheckpointON.SetActive(true);
        }
    }
}
