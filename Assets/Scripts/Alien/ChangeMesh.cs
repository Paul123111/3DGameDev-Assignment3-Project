using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMesh : MonoBehaviour
{

    [SerializeField] Mesh changed;
    [SerializeField] SkinnedMeshRenderer original;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            original.sharedMesh = changed;
        }
    }
}
