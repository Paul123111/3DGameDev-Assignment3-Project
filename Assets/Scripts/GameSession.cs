using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameSession : MonoBehaviour
{

    private void Awake() {
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        if (numGameSessions > 1) {
            Destroy(gameObject);
        } else {
            //DontDestroyOnLoad(gameObject);
        }
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void NextLevelWait() {
        StartCoroutine(Exit());
    }

    public void Mainmenu() {
        SceneManager.LoadScene(1);
    }

    IEnumerator Exit() {
        GameSession gameSession = FindFirstObjectByType<GameSession>();
        yield return new WaitForSeconds(8f);
        gameSession.NextLevel();
    }
}
