using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(AgentLinkMover))]
public class KidMovementScript : MonoBehaviour
{
    [SerializeField] float initSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] private KidThrowerSO sO;
    [SerializeField] private Transform snowSpawn;
    //[SerializeField] private Transform snowTarget;
    

    ObjectPoolerScript poolerScript;

    [HideInInspector] public KidSpawnerScript spawner;


    public static event Action<int> TargetNotFound = delegate { };
    private float targetFoundTracker = 0;
    private float targetNotFoundTime = 2f; 
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
    private const string throwTag = "throw";
    private const string throwSpeedTag = "throwSpeed";

    private bool kidActive = true;
    private bool hasReachedTarget = false;
    bool hasReachedDestination = false;

    public Vector3 finalTarget;
    public void SetFinalTarget(Vector3 finalTarget)
    {
        this.finalTarget = finalTarget;
    }

    public void OnEnterDanger()
    {
        
            
            Agent.enabled = false;
            Animator.SetFloat(throwSpeedTag, sO.SpeedMultiplier);
            Animator.SetTrigger(throwTag);
    }


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
        poolerScript = ObjectPoolerScript.Instance;
        StartCoroutine(FollowTarget());
        Agent.speed = initSpeed;
        speedIncrement = (maxSpeed - initSpeed) / 3;

        Debug.Log(Agent.velocity.magnitude);
        Animator.SetFloat("runSpeed", Agent.velocity.magnitude / Agent.speed);
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
            Animator.SetFloat("runSpeed", Agent.velocity.magnitude / Agent.speed);
        }
    }

    void CreateTheBallAndThrow()
    {
        GameObject projectile = poolerScript.SpawnFromPool(OPTag.ENEMYBULLET, snowSpawn.position, Quaternion.identity);
        projectile.GetComponent<Rigidbody>().isKinematic = false;
        Vector3 randomInaccuracy = new(UnityEngine.Random.Range(-sO.MaxInaccuracy, sO.MaxInaccuracy), UnityEngine.Random.Range(-sO.MaxInaccuracy, sO.MaxInaccuracy), 0);
        //target.y += 1.5f;
        Vector3 throwDirection = finalTarget - (snowSpawn.position + randomInaccuracy);
        
        projectile.GetComponent<Rigidbody>().AddForce(sO.Force * throwDirection.normalized, ForceMode.Impulse);

    }

    private void HandleLinkEnd()
    {
        Animator.SetTrigger(landed);
    }

    private void Update()
    {
        if (!Agent.enabled) return;
        Animator.SetBool(isRunning, Agent.velocity.magnitude > 0.01f);

        if (Agent.velocity.magnitude < 0.01f)
        {
            targetFoundTracker += Time.deltaTime;
        } else
        {
            targetFoundTracker = 0;
        }
        if (targetFoundTracker >= targetNotFoundTime)
        {
            TargetNotFound?.Invoke(GetID());
            Debug.Log("target not found");

        }
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
