using UnityEngine;
using UnityEngine.AI;

public class EnemyRangeController : MonoBehaviour
{
    public static EnemyRangeController instance;

    public Transform[] patrolPoints;
    public int currentPatrolPoint;

    public NavMeshAgent agent;
    public Animator animator;

    public float waitAtPoint = 2f;
    private float waitCounter;

    public float chaseRange;
    public float attackRange = 1f;

    public float timeBetweenAttacks = 2f;
    public float attackCounter;

    public float speedRotation;


    //public bool isKnocking;
    //public float timeKnocking = 0.1f;
    //public float knockingCounter;

    private void Awake()
    {
        instance = this;
    }

    public enum IAState
    {
        Idle,
        Patrolling,
        Chasing,
        Attacking,
        Knocking
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

                if (waitCounter > 0)
                {
                    waitCounter -= Time.deltaTime;
                }
                else
                {
                    currentState = IAState.Patrolling;
                    agent.SetDestination(patrolPoints[currentPatrolPoint].position);
                }

                if (distanceToPlayer <= chaseRange)
                {
                    currentState = IAState.Chasing;
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


                        attackCounter = timeBetweenAttacks;
                    }
                }
                else
                {
                    currentState = IAState.Idle;
                    waitCounter = waitAtPoint;

                    agent.isStopped = false;
                }

                break;

                //case IAState.Knocking:

                //    knockingCounter -= Time.deltaTime;

                //    if (knockingCounter <= 0)
                //    {
                //        currentState = IAState.Idle;
                //        knockingCounter = timeKnocking;
                //    }

                //    break;
        }

    }
}
