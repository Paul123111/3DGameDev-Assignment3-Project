using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootRadius : MonoBehaviour
{

    AlienController alienController;
    [SerializeField] GameObject parent;

    // Start is called before the first frame update
    void Start()
    {
        alienController = GameObject.FindGameObjectWithTag("Player").GetComponent<AlienController>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            alienController.setCanLoot(true);
            alienController.setNearby(parent);
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.CompareTag("Player")) {
            alienController.setCanLoot(true);
            alienController.setNearby(parent);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            alienController.setCanLoot(false);
            alienController.setNearby(null);
        }
    }
}
