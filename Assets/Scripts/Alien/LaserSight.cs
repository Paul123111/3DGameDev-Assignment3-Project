using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSight : MonoBehaviour
{
    //Credits - https://www.youtube.com/watch?v=hUg3UfE186Q

    private LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit)) {
            if (hit.collider) {
                //Debug.Log(new Vector3(0, 0, hit.distance));
                line.SetPosition(1, new Vector3(0, 0, hit.distance));
            }
        } else {
            line.SetPosition(1, new Vector3(0, 0, 5000));
        }
    }
}
