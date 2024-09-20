using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Reset : MonoBehaviour
{

    void OnRetry() {
        GameSession gameSession = FindFirstObjectByType<GameSession>();
        gameSession.Mainmenu();
    }

    private void OnQuit() {
        //Debug.Log("Bye");
        Application.Quit();
    }
}
