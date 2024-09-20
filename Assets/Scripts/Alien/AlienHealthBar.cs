using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienHealthBar : MonoBehaviour
{
    Transform healthBar;
    [SerializeField] AlienController alienController;

    // Start is called before the first frame update
    void Start() {
        healthBar = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update() {
        healthBar.localScale = new Vector3(((float)alienController.getHealth() / alienController.getMaxHealth())*3f, 0.4f, 1);
        if (alienController.getHealth() <= 0) {
            Destroy(gameObject);
        }
    }
}
