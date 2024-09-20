using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    [SerializeField] float timeToDestroy;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

}
