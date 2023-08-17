using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteoriteArea : MonoBehaviour
{
    public static meteoriteArea instance;

    public float timer = 0;

    public GameObject meteorite;
    public float timeBetweenMeteorite = 1f;

    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        timer += Time.deltaTime;
    }
    public void SpawnMeteorite()
    {
        if (timer > timeBetweenMeteorite)
        {
            Instantiate(meteorite);
            timer = 0;
        }
    }
}
