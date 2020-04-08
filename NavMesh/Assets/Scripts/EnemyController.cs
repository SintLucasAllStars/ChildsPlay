using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour {
    public enum State {
        Idle,
        Wander,
        Look,
        Follow
    }

    private float stateTimer;
    private State currentState;

    private NavMeshAgent agent;
    private Transform target;

    void Start() {
        currentState = State.Idle;
        agent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<PlayerController>().transform;
    }

    void Update() {
        State thisState = currentState;
        switch (currentState) {
            case State.Idle:
                if (stateTimer > .5f)
                    if (SeeTarget(.3f))
                        agent.destination = target.position;
                    else if (Random.value > .5f)
                        currentState = State.Wander;
                    else stateTimer = 0;
                break;
            case State.Wander:
                if (stateTimer > .2f)
                    if (SeeTarget(.5f))
                        currentState = State.Follow;
                else if (!agent.pathPending)
                    if (agent.remainingDistance <= agent.stoppingDistance)
                        if (Random.value > .5f)
                            if (Random.value > .5f)
                                currentState = State.Look;
                            else currentState = State.Idle;
                        else agent.destination = transform.position + Vector3.forward * Random.Range(-10f, 10f) + Vector3.right * Random.Range(-10f, 10f);
                break;
            case State.Look:
                transform.eulerAngles = transform.eulerAngles + Vector3.up * Time.deltaTime * 90;
                if (SeeTarget(.9f))
                    currentState = State.Follow;
                else if (stateTimer > .2f)
                    if (Random.value > .4f)
                        currentState = State.Wander;
                    else stateTimer = 0;
                break;
            case State.Follow:
                if (Vector3.Dot(target.position - transform.position, target.position - transform.position) < 2)
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                if (stateTimer > .5f) {
                    if (SeeTarget(.2f))
                        agent.destination = target.position;
                    else if (!agent.pathPending)
                        if (agent.remainingDistance <= agent.stoppingDistance)
                            currentState = State.Wander;
                    stateTimer = 0;
                }
                break;
        }
        stateTimer += Time.deltaTime;
        if (thisState != currentState) stateTimer = 0;
    }

    bool SeeTarget(float fov) {
        return Vector3.Dot(transform.forward, (target.position - transform.position).normalized) > fov &&
               Physics.Raycast(transform.position, target.position - transform.position, out RaycastHit hit) &&
               hit.transform == target;
    }
}