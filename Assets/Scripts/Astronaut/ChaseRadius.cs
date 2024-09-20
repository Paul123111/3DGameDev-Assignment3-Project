using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseRadius : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Astronaut")) {
            PatrolState patrolState = other.GetComponent<StateManager>().getCurrentState() as PatrolState;

            if (patrolState!=null) {
                patrolState.SetAstronautAlerted(true);
            }
        }
    }
}
