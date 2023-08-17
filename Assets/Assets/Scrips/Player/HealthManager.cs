using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;

    public int currentHealth;
    public int maxHealth;

    public float invecibleLength = 0.5f;
    public float invecibleCounter;

    public Material hitMaterial;
    public Material baseMaterial;

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
        if(invecibleCounter > 0)
        {
            invecibleCounter -= Time.deltaTime;

            for(int i = 0; i < PlayerController.instance.PlayerPieces.Length; i++)
            {
                if(Mathf.Floor(invecibleCounter * 7f) % 2 == 0)
                {
                    PlayerController.instance.PlayerPieces[i].GetComponent<Renderer>().material = baseMaterial;
                }
                else
                {
                    PlayerController.instance.PlayerPieces[i].GetComponent<Renderer>().material = hitMaterial;
                }

                if (invecibleCounter <= 0 )
                {
                    PlayerController.instance.PlayerPieces[i].GetComponent<Renderer>().material = baseMaterial;
                }
            }
        }
    }

    public void Hurt()
    {
        if(PlayerController.instance.isDefend)
        {
            PlayerController.instance.animator.SetTrigger("getHit");
            AudioManager.instance.PlaySFX(AudioManager.instance.getHitShield);
        }
        else if(invecibleCounter <= 0)
        {
            PlayerController.instance.animator.SetTrigger("getHit");
            AudioManager.instance.PlaySFX(AudioManager.instance.getHit);

            CinemachineShake.instance.ShakeCamera();
            currentHealth--;

            if (currentHealth <= 0)
            {
                GameManager.instance.Respawn();
                PlayerController.instance.isAttacking = false;
                PlayerController.instance.HurtBox.SetActive(false);
            }
            else
            {
                PlayerController.instance.Knockback();
                invecibleCounter = invecibleLength;
            }
        }

        UpdateUI();
    }


    public void ResetHealth()
    {
        currentHealth = maxHealth;

        UpdateUI();
    }


    public void AddHealt(int amountToHeal)
    {
        currentHealth += amountToHeal;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        UpdateUI();
    }

    public void UpdateUI()
    {
        switch(currentHealth)
        {
            case 3:
                UIManager.instance.Hearts[2].SetActive(true);
                UIManager.instance.Hearts[1].SetActive(true);
                break;
            case 2:
                UIManager.instance.Hearts[2].SetActive(false);
                UIManager.instance.Hearts[1].SetActive(true);
                break;
            case 1:
                UIManager.instance.Hearts[1].SetActive(false);
                break;
        }
    }
}
