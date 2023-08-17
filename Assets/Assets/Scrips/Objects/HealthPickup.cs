using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healAmount;
    public bool isFullHeal;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && HealthManager.instance.currentHealth != HealthManager.instance.maxHealth)
        {
            Destroy(gameObject);

            AudioManager.instance.PlaySFX(AudioManager.instance.heart);

            if (isFullHeal)
            {
                HealthManager.instance.ResetHealth();
            }
            else
            {
                HealthManager.instance.AddHealt(healAmount);
            }
        }
    }
}
