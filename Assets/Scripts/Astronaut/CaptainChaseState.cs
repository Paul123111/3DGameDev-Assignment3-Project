using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CaptainChaseState : State {

    Transform[] goals = new Transform[1];
    [SerializeField] Animator anim;
    [SerializeField] NavMeshAgent agent;
    //[SerializeField] PatrolState patrolState;
    //[SerializeField] AttackState attackState;

    [SerializeField] State[] states;
    float time = 0;
    [SerializeField] float cooldown;

    void Awake() {
        goals[0] = GameObject.FindGameObjectWithTag("PlayerAim").transform;
        anim.SetBool("isCaptain", true);
    }

    public override Transform[] getGoals() {
        return goals;
    }

    public override void InitialSwitch() {
        anim.SetBool("isChasing", true);
        agent.speed = 70;
        agent.angularSpeed = 1000;
        agent.acceleration = 40;
    }
    public override State RunCurrentState() {
        time += Time.deltaTime;
        //Debug.Log(time);
        if (time >= cooldown) {
            time = 0;
            return states[Random.Range(0, states.Length)];
        }
        return this;
    }
}
