using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class MoveTo : MonoBehaviour {

    [SerializeField] Transform[] goals;
    
    int waypointIndex = 0;
    NavMeshAgent agent;
    NavMeshPath path;

    void Start() {
        agent = GetComponent<NavMeshAgent>();

        path = new NavMeshPath();
        StartCoroutine(updateGoal());
    }

    //void Update() {
        
    //}

    void IterateWaypoint() {
        waypointIndex++;
        if (waypointIndex >= goals.Length) {
            waypointIndex = 0;
        }
    }

    public NavMeshAgent getAgent() {
        return agent;
    }

    public void setGoals(Transform[] goals) {
        waypointIndex = 0;
        this.goals = goals;
    }

    IEnumerator updateGoal() {
        for (;;) {

            NavMesh.SamplePosition(agent.transform.position, out NavMeshHit hitA, 10f, NavMesh.AllAreas);
            bool validPos = NavMesh.SamplePosition(goals[waypointIndex].position, out NavMeshHit hitB, 10f, NavMesh.AllAreas);
            //Debug.Log(waypointIndex);

            if (validPos) {
                NavMesh.CalculatePath(hitA.position, hitB.position, NavMesh.AllAreas, path);
                agent.SetPath(path);

                if (goals.Length > 1 && Vector3.Distance(hitA.position, hitB.position) < 20) {
                    IterateWaypoint();
                }
            }

            yield return new WaitForSeconds(0.3f);
        }
    }
}
