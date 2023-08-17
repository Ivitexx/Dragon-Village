using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossHealthManager : MonoBehaviour
{
    public static MiniBossHealthManager instance;

    public int maxHealth = 1;
    public int currentHealth;

    private MiniBossController miniBossController;
    private CapsuleCollider capsuleCollider;
    private void Awake()
    {
        instance = this;
        miniBossController = GetComponent<MiniBossController>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            StartCoroutine(MiniBossDead());
        }
    }

    public IEnumerator MiniBossDead()
    {
        miniBossController.enabled = false;
        capsuleCollider.enabled = false;
        GetComponent<MiniBossController>().animator.SetTrigger("Dead");
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}
