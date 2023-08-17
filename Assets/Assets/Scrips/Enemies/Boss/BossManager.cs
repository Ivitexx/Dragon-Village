using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public static BossManager instance;

    [Header("PARAMETERS------------")]

    public float time = 0;

    public Animator animator;
    public GameObject WeaknessBox;
    public GameObject[] BossPieces;

    public float invecibleLength = 0.5f;
    public float invecibleCounter;

    public Material hitMaterial;
    public Material baseMaterial;

    [Header("WAVES------------")]
    public GameObject wave1;
    public GameObject wave2;
    public GameObject wave3;

    public int currentWave;

    public List<GameObject> enemisWave1 = new List<GameObject>();
    public List<GameObject> enemisWave2 = new List<GameObject>();
    public List<GameObject> enemisWave3 = new List<GameObject>();

    [Space]

    [Header("METEORITE------------")]
    public GameObject meteoriteZone;

    [Space]

    [Header("FLARE------------")]
    public bool activateFlare;
    public GameObject flareObject;
    public GameObject flare;
    public GameObject flareDamageZone;
    public int FlareSound;

    private void Awake()
    {
        instance = this;
    }

    public enum BossState
    {
        Start,
        Wave,
        Meteorite,
        Flare,
        Weak,
        Finish
    };

    [Header("STATE------------")]
    public BossState currentState;
    void Start()
    {
        wave1.SetActive(false);
        wave2.SetActive(false);
        wave3.SetActive(false);

        flare.SetActive(false);
        flareDamageZone.SetActive(false);
        currentWave = 1;
    }

    void Update()
    {
        time += Time.deltaTime;

        switch (currentState)
        {

            case BossState.Start:

                if (time < 0.5)
                {
                    animator.SetTrigger("Appear");
                    animator.SetTrigger("ThrowFlame");
                }

                if (time > 7f)
                {
                    animator.SetTrigger("ThrowFlame");
                }

                if (time > 8f)
                {
                    currentState = BossState.Wave;
                    time = 0f;
                }

                break;

            case BossState.Wave:

                if (currentWave == 1)
                {
                    wave1.SetActive(true);

                    if (enemisWave1.Count == 0 || time > 60)
                    {
                        time = 0f;

                        currentState = BossState.Meteorite;
                        animator.SetTrigger("ThrowFlame");
                    }
                }

                if (currentWave == 2)
                {
                    wave2.SetActive(true);

                    if (enemisWave2.Count == 0 || time > 60)
                    {
                        time = 0f;

                        currentState = BossState.Meteorite;
                        animator.SetTrigger("ThrowFlame");
                    }
                }

                if (currentWave >= 3)
                {
                    wave3.SetActive(true);

                    if (enemisWave3.Count == 0 || time > 60)
                    {
                        time = 0f;

                        currentState = BossState.Meteorite;
                        animator.SetTrigger("ThrowFlame");
                    }
                }
                break;

            case BossState.Meteorite:

                meteoriteZone.SetActive(true);

                meteoriteArea.instance.SpawnMeteorite();

                if (time > 30)
                {
                    currentState = BossState.Flare;
                    meteoriteZone.SetActive(false);

                    time = 0f;
                }

                if (!gameObject.activeInHierarchy)
                {
                    currentState = BossState.Start;
                }
                break;

            case BossState.Flare:

                flareObject.SetActive(true);

                if (time > 8 && time < 8.1)
                {
                    animator.SetTrigger("ThrowFlame");

                }

                if (time > 15)
                {
                    flareDamageZone.SetActive(true);

                }

                if (time > 20)
                {
                    flare.SetActive(true);

                }

                if (time > 27)
                {
                    flare.SetActive(false);
                    flareDamageZone.SetActive(false);
                }

                if (time > 28)
                {
                    WeaknessBox.SetActive(true);
                    currentState = BossState.Weak;
                    time = 0f;
                }

                if (!gameObject.activeInHierarchy)
                {
                    currentState = BossState.Start;
                }
                break;

            case BossState.Weak:
                animator.SetBool("ToWeakned", true);

                if (time > 15 || !WeaknessBox.activeSelf)
                {
                    animator.SetBool("ToWeakned", false);
                    WeaknessBox.SetActive(false);
                    DamageBoss();
                }


                if (time > 20)
                {
                    if (BossHealth.instance.Death == true)
                    {
                        currentState = BossState.Finish;
                        time = 0f;
                    }
                    else
                    {
                        WeaknessBox.SetActive(true);
                        animator.SetTrigger("ThrowFlame");
                    }
                }

                if (time > 24)
                {
                    currentWave++;
                    currentState = BossState.Wave;
                    time = 0f;
                }
                break;

            case BossState.Finish:

                if(time > 5)
                {
                    GameManager.instance.FinishTheGame();
                }
                break;
        }

        if(HealthManager.instance.currentHealth == 0f)
        {
            time = 0f;

            if(currentWave == 1)
            {
                currentState = BossState.Start;
                wave1.SetActive(false);
            }

            if (currentWave == 2)
            {
                currentState = BossState.Wave;
                wave2.SetActive(false);
            }

            if (currentWave == 3)
            {
                currentState = BossState.Wave;
                wave3.SetActive(false);
            }
        }
    }

    public void DamageBoss()
    {
        if (invecibleCounter > 0)
        {
            invecibleCounter -= Time.deltaTime;

            for (int i = 0; i < BossPieces.Length; i++)
            {
                if (Mathf.Floor(invecibleCounter * 5f) % 2 == 0)
                {
                    BossPieces[i].GetComponent<Renderer>().material = baseMaterial;
                }
                else
                {
                    BossPieces[i].GetComponent<Renderer>().material = hitMaterial;
                }

                if (invecibleCounter <= 0)
                {
                    BossPieces[i].GetComponent<Renderer>().material = baseMaterial;
                }
            }
        }
    }
}
