using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollBehaviour : MonoBehaviour
{
    Rigidbody myRigidbody;
    [SerializeField] int deathKick;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        myRigidbody.velocity = new Vector3(Random.Range(-deathKick, deathKick), Random.Range(-deathKick, deathKick), Random.Range(-deathKick, deathKick));
    }
}
