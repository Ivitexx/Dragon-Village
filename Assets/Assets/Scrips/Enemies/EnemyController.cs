using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;

    public Transform[] patrolPoints;
    public int currentPatrolPoint;

    public NavMeshAgent agent;
    public Animator animator;
    private EnemyHealthManager enemyHealthManager;

    public float waitAtPoint = 2f;
    private float waitCounter;

    public float chaseRange;
    public float attackRange = 1f;
    public bool attacking;

    public float timeBetweenAttacks = 2f;
    public float attackCounter;

    public float speedRotation;

    private Vector3 respawnPosition;

    private void Awake()
    {
        instance = this;

        enemyHealthManager = GetComponent<EnemyHealthManager>();

        respawnPosition = this.transform.position;
    }

    public enum IAState
    {
        Idle,
        Patrolling,
        Chasing,
        Attacking,
        Respawning
    };
    public IAState currentState;

    void Start()
    {
        waitCounter = waitAtPoint;
        attackCounter = timeBetweenAttacks;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        switch (currentState)
        {
            case IAState.Idle:

                animator.SetBool("IsMoving", false);

                agent.isStopped = true;

                if (waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;
                }
                else
                {
                    agent.isStopped = false;

                    currentState = IAState.Patrolling;
                    agent.SetDestination(patrolPoints[currentPatrolPoint].position);
                }

                if (distanceToPlayer <= chaseRange)
                {
                    agent.isStopped = false;

                    currentState = IAState.Chasing;
                }

                if(HealthManager.instance.currentHealth == 0f)
                {
                    currentState = IAState.Respawning;
                }

                break;

            case IAState.Patrolling:

                animator.SetBool("IsMoving", true);

                if (agent.remainingDistance <= .2f)
                {
                    currentPatrolPoint++;
                    if (currentPatrolPoint >= patrolPoints.Length)
                    {
                        currentPatrolPoint = 0;
                    }

                    currentState = IAState.Idle;
                    waitCounter = waitAtPoint;
                }

                if (distanceToPlayer <= chaseRange)
                {
                    currentState = IAState.Chasing;
                }

                if (HealthManager.instance.currentHealth == 0f)
                {
                    currentState = IAState.Respawning;
                }
                break;

            case IAState.Chasing:

                animator.SetBool("IsMoving", true);

                agent.SetDestination(PlayerController.instance.transform.position);

                if (attackCounter != timeBetweenAttacks)
                    attackCounter -= Time.deltaTime;

                if (distanceToPlayer <= attackRange)
                {
                    agent.isStopped = true;
                    animator.SetBool("IsMoving", false);
                    agent.velocity = Vector3.zero;

                    //attackCounter = 0f;
                    currentState = IAState.Attacking;
                }

                if (distanceToPlayer > chaseRange)
                {
                    currentState = IAState.Idle;
                    waitCounter = waitAtPoint;
                    attackCounter = timeBetweenAttacks;

                    agent.velocity = Vector3.zero;
                    agent.SetDestination(transform.position);
                }

                if (HealthManager.instance.currentHealth == 0f)
                {
                    currentState = IAState.Respawning;
                }
                break;

            case IAState.Attacking:

                transform.LookAt(PlayerController.instance.transform, Vector3.up);
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0f);

                attackCounter -= Time.deltaTime;

                if (distanceToPlayer < attackRange)
                {
                    if (attackCounter <= 0)
                    {
                        animator.SetTrigger("Attack");

                        if(gameObject.name == "Enemy")
                        {
                            AudioManager.instance.PlaySFX(AudioManager.instance.enemySword);
                        }

                        attackCounter = timeBetweenAttacks;
                    }
                }
                else if (!attacking && HealthManager.instance.currentHealth != 0f)
                {
                    currentState = IAState.Idle;
                    waitCounter = waitAtPoint;

                    agent.isStopped = false;
                }

                if (HealthManager.instance.currentHealth == 0f)
                {
                    currentState = IAState.Respawning;
                }
                break;

            case IAState.Respawning:

                agent.isStopped = true;

                this.transform.position = respawnPosition;
                enemyHealthManager.currentHealth = enemyHealthManager.maxHealth;
                currentPatrolPoint = 0;
                waitCounter = 0;
                attackCounter = timeBetweenAttacks;

                currentState = IAState.Idle;
                break;
        }

    }
}
