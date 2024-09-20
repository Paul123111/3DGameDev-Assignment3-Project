using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    [SerializeField] PatrolState patrolState;

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            patrolState.SetCanSeePlayer(true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            patrolState.SetCanSeePlayer(false);
        }
    }
}
