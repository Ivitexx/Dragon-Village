using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public static BossHealth instance;

    public int maxHealth;
    public int currentHealth;
    public bool Death = false;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        currentHealth--;

        if(currentHealth == 0f)
        {
            BossManager.instance.animator.SetTrigger("Death");
            Death = true;
        }

        BossManager.instance.invecibleCounter = BossManager.instance.invecibleLength;
        gameObject.SetActive(false);
        BossManager.instance.time = 15f;
    }
}
