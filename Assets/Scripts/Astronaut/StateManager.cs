using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{

    [SerializeField] State currentState;
    [SerializeField] int maxHealth;
    [SerializeField] State deathState;

    int health;

    MoveTo moveTo;

    // Start is called before the first frame update
    void Start()
    {
        moveTo = GetComponent<MoveTo>();
        moveTo.setGoals(currentState.getGoals());
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        RunStateMachine();   
    }
    
    private void RunStateMachine() {
        //Debug.Log(currentState);
        if (health <= 0) {
            SwitchToNextState(deathState);
            return;
        }
        State nextState = currentState?.RunCurrentState();

        if (nextState != null && nextState != currentState) {
            SwitchToNextState(nextState);
        }
    }

    private void SwitchToNextState(State nextState) {
        currentState = nextState;
        currentState.InitialSwitch();
        moveTo.setGoals(currentState.getGoals());
    }

    public int getHealth() {
        return health;
    }

    public int getMaxHealth() {
        return maxHealth;
    }

    public void setHealth(int health) {
        this.health = health;
    }

    public State getCurrentState() {
        return currentState;
    }
}
