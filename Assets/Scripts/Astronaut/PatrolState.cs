using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolState : State {

    [SerializeField] ChaseState chaseState;
    [SerializeField] Animator anim;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] StateManager stateManager;
    [SerializeField] GameObject exclamationPoint;
    [SerializeField] GameObject chaseRadius;

    [SerializeField] AudioSource sightedAudio;

    bool canSeePlayer = false;
    bool astronautAlerted = false;

    [SerializeField] Transform[] goals;

    public override Transform[] getGoals() {
        return goals;
    }

    public override void InitialSwitch() {
        anim.SetBool("isChasing", false);
        agent.speed = 50;
    }

    public override State RunCurrentState() {
        if (canSeePlayer || astronautAlerted || stateManager.getHealth() < stateManager.getMaxHealth()) {
            //Debug.Log("State Changed");
            if (!sightedAudio.isPlaying && !astronautAlerted) {
                sightedAudio.Play();
            }
            exclamationPoint.SetActive(true);
            chaseRadius.SetActive(true);
            return chaseState;
        }
        //Debug.Log("Ran State");
        return this;
    }

    public bool GetCanSeePlayer() {
        return canSeePlayer;
    }

    public void SetCanSeePlayer(bool canSeePlayer) {
        //Debug.Log("set player");
        this.canSeePlayer = canSeePlayer;
    }

    public void SetAstronautAlerted(bool astronautAlerted) {
        //Debug.Log("set player");
        this.astronautAlerted = astronautAlerted;
    }
}
