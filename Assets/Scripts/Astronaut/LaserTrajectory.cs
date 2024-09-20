using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrajectory : MonoBehaviour
{
    //[SerializeField] float laserSpeed;
    [SerializeField] float lifeTime;
    [SerializeField] int damage;
    AlienController alienController;

    float currentTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        alienController = player.GetComponent<AlienController>();
        //myRigidbody.velocity = (laserSpeed * gameObject.transform.forward);
    }

    //update is called once per frame
    void Update() {
        currentTime += Time.deltaTime;
        if (currentTime > lifeTime) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            alienController.decreaseHealth(damage);
            alienController.hurt();
            Destroy(gameObject);
        }
    }
}
