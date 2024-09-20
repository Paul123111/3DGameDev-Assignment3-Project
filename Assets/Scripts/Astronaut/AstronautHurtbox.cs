using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstronautHurbox : MonoBehaviour
{
    [SerializeField] StateManager stateManager;
    [SerializeField] AudioSource hurtAudio;
    bool iFrames = false;

    private void OnTriggerEnter(Collider other) {
        if (!iFrames && other.CompareTag("PunchHitbox")) {
            stateManager.setHealth(stateManager.getHealth() - 10);
            if (!hurtAudio.isPlaying) {
                hurtAudio.Play();
            }
            StartCoroutine(iFramesActivation(0.25f));
        }
        if (!iFrames && other.CompareTag("AlienLaserHitbox")) {
            stateManager.setHealth(stateManager.getHealth() - 3);
            if (!hurtAudio.isPlaying) {
                hurtAudio.Play();
            }
            StartCoroutine(iFramesActivation(0.1f));
        }
    }

    IEnumerator iFramesActivation(float time) {
        iFrames = true;
        yield return new WaitForSeconds(time);
        iFrames = false;
    }

}
