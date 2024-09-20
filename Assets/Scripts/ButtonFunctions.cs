using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ButtonFunctions : MonoBehaviour
{
    [SerializeField] Animator animSettings;
    [SerializeField] Animator animStart;
    [SerializeField] Animator animPanel;

    private void Start() {
        
    }

    public void Settings() {
        animStart.SetBool("isHidden", true);
        animSettings.SetBool("isHidden", true);
        animPanel.SetBool("isHidden", false);
    }

    public void ExitSettings() {
        animStart.SetBool("isHidden", false);
        animSettings.SetBool("isHidden", false);
        animPanel.SetBool("isHidden", true);
    }

    public void FirstLevel() {
        SceneManager.LoadScene(1);
    }
}
