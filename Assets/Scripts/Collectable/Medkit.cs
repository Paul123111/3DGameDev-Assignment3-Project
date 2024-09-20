using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    AlienController alienController;

    // Start is called before the first frame update
    void Start()
    {
        alienController = GameObject.FindGameObjectWithTag("Player").GetComponent<AlienController>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            alienController.increaseHealth(20);
            Destroy(gameObject);
        }
    }
}
