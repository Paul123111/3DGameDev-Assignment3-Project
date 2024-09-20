using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    Transform healthBar;
    [SerializeField] StateManager stateManager;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponentInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.localScale = new Vector3(((float) stateManager.getHealth() / stateManager.getMaxHealth()), 1, 1);
        if (stateManager.getHealth() <= 0) {
            Destroy(gameObject);
        }
    }
}
