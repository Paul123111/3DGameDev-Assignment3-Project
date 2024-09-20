using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State {

    Transform[] goals = new Transform[1];
    [SerializeField] Animator anim;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] PatrolState patrolState;
    [SerializeField] AttackState attackState;

    void Awake() {
        goals[0] = GameObject.FindGameObjectWithTag("PlayerAim").transform;
    }

    public override Transform[] getGoals() {
        return goals;
    }

    public override void InitialSwitch() {
        anim.SetBool("isChasing", true);
        agent.speed = 50;
        agent.angularSpeed = 1000;
        agent.acceleration = 40;
    }
    public override State RunCurrentState() {
        if (patrolState.GetCanSeePlayer()) {
            return attackState;
        }
        return this;
    }
}
