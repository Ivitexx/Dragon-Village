using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public static EnemyHealthManager instance;

    public int maxHealth = 1;
    public int currentHealth;
    public bool haveDroop = false;
    public GameObject droop = null;

    private EnemyController enemyController;
    private CapsuleCollider capsuleCollider;
    private void Awake()
    {
        instance = this;
        enemyController = GetComponent<EnemyController>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        currentHealth = maxHealth;
    }

    void Start()
    {
        //currentHealth = maxHealth;
    }

    public void TakeDamage()
    {
        currentHealth--;
        if(currentHealth <= 0)
        {
            StartCoroutine(EnemyDead());
        }
    }

    public IEnumerator EnemyDead()
    {
        enemyController.enabled = false;
        capsuleCollider.enabled = false;
        GetComponent<EnemyController>().animator.SetTrigger("Dead");
        if(haveDroop)
        Instantiate(droop, transform.position, transform.rotation);
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
