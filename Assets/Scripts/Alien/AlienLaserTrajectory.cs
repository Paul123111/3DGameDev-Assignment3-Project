using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienLaserTrajectory : MonoBehaviour
{
    [SerializeField] float lifeTime;

    float currentTime = 0;

    // Start is called before the first frame update
    void Start() {
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
        if (other.CompareTag("EnemyHurtbox")) {
            Destroy(gameObject);
        }
    }
}
