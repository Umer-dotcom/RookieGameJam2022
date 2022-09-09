using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(AgentLinkMover))]
public class KidMovementScript : MonoBehaviour
{
    [SerializeField] float initSpeed;
    [SerializeField] float maxSpeed;

    private float speedIncrement;
    public Vector3 target;

    private Animator Animator;
    public float UpdateRate = 0.1f;
    private NavMeshAgent Agent;
    private AgentLinkMover LinkMover;

    private KidInfantryScript kid;

    private const string isRunning = "isRunning";
    private const string jump = "jump";
    private const string landed = "landed";

    private bool kidActive = true;
    private bool hasReachedTarget = false;
    private bool hasReachedDestination = false;

    

    int wayPointIndex = 0;

    public int GetID()
    {
        return kid.GetKidInfantryID();
    }

    private void Awake()
    {
        kid = GetComponent<KidInfantryScript>();
        Animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        LinkMover = GetComponent<AgentLinkMover>();

        LinkMover.OnLinkEnd += HandleLinkEnd;
        LinkMover.OnLinkStart += HandleLinkStart;
    }

    private void Start()
    {
        StartCoroutine(FollowTarget());
        Agent.speed = initSpeed;
        speedIncrement = (maxSpeed - initSpeed) / 3;
    }

    private void HandleLinkStart()
    {
        Animator.SetTrigger(jump);
    }
    public void GetAggressive()
    {
        if (Agent.speed < maxSpeed)
        {
            Agent.speed += speedIncrement;
        }
    }

    private void HandleLinkEnd()
    {
        Animator.SetTrigger(landed);
    }

    private void Update()
    {
        Animator.SetBool(isRunning, Agent.velocity.magnitude > 0.01f);
    }

    private IEnumerator FollowTarget()
    {
        WaitForSeconds Wait = new WaitForSeconds(UpdateRate);

        while (Agent.isActiveAndEnabled && kidActive)
        {
            Agent.SetDestination(target - (target - transform.position).normalized * 0.5f);
            yield return Wait;
        }
    }
    public void KidDied()
    {
        Animator.enabled = false;
        Agent.enabled = false;
        LinkMover.enabled = false;
        kidActive = false;

    }

    public void SetTarget(Vector3 targetPosition)
    {
        target = targetPosition;

        hasReachedTarget = false;
    }
    public void ReachedTarget(int ID)
    {

        if (kid.GetKidInfantryID() == ID && !hasReachedTarget)
        {
            hasReachedTarget = true;
            wayPointIndex++;
            if (wayPointIndex >= 5) hasReachedDestination = true;
        }
    }
    public bool IsTargetReached()
    {
        return hasReachedTarget;
    }

    public int WPIndex()
    {
        return wayPointIndex;
    }

}
