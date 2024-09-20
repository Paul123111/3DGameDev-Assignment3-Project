using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State {

    [SerializeField] Transform[] goals;
    [SerializeField] GameObject ragdoll;
    [SerializeField] GameObject astronaut;
    [SerializeField] bool isCaptain;

    public override Transform[] getGoals() {
        return goals;
    }

    public override void InitialSwitch() {
        if (isCaptain) {
            GameSession gameSession = FindFirstObjectByType<GameSession>();
            gameSession.NextLevelWait();
        }
        Instantiate(ragdoll, transform.position, transform.rotation);
        Destroy(astronaut);
    }

    public override State RunCurrentState() {
        return this;
    }
}
