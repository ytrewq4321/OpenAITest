using UnityEngine;
using UnityEngine.AI;

public class Fox : MonoBehaviour
{
    [SerializeField] private GameObject chatCanvas;
    [SerializeField] public Transform centerPoint;
    [SerializeField] public Transform camera;

    public StateMachine SM;
    public UserInteractionState UserInteraction;
    public RunState Run;
    public bool IsInteractWithUser { get; private set; }

    private Animator animator;
    private NavMeshAgent agent;
    private float range = 20f;


    private void Start()
    {
        SM = new StateMachine();
        UserInteraction = new UserInteractionState(this, SM);
        Run = new RunState(this, SM);

        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        SM.Initialize(Run);

        ChatUI.ChatClosed.AddListener(() => IsInteractWithUser = false);
        ChatUI.ChatOpened.AddListener(() => IsInteractWithUser = true);
    }

    private void Update()
    {
        SM.CurrentState.LogicUpdate();
        Debug.Log(SM.CurrentState);
    }


    public void SetAnimationBool(string param, bool value)
    {
        animator.SetBool(param, value);
    }

    public void SetDestination()
    {
        if(agent.remainingDistance<=agent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(centerPoint.position, range, out point))
            {
                agent.SetDestination(point);
            }
        }
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    public void StopMove()
    {
        agent.isStopped = true;
    }

    public void ResumeMove()
    {
        agent.isStopped = false;
    }

    public void LookAtCamera()
    {
        transform.LookAt(camera.transform);
    }

    private void OnMouseDown()
    {
        chatCanvas.SetActive(true);
        ChatUI.ChatOpened.Invoke();
    }
}
