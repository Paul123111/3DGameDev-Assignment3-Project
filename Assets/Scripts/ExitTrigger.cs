using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    AlienController alienController;

    private void Awake() {
        alienController = GameObject.FindGameObjectWithTag("Player").GetComponent<AlienController>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && alienController.getAlive()) {
            StartCoroutine(Exit());
        }
    }

    IEnumerator Exit() {
        GameSession gameSession = FindFirstObjectByType<GameSession>();
        yield return new WaitForSeconds(1f);
        gameSession.NextLevel();
    }
}
