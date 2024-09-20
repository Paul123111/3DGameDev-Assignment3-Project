using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CaptainAttackState : State
{
    //Attack should stand still, take out ray gun, and shoot until alien has taken cover

    Transform[] goals = new Transform[1];
    [SerializeField] Animator anim;
    [SerializeField] CaptainChaseState chaseState;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] GameObject laser;
    CharacterController player;
    [SerializeField] Renderer rayGunRenderer;
    [SerializeField] GameObject particles;

    [SerializeField] float accuracy;
    [SerializeField] int numShots;
    [SerializeField] float speedMultiplier;
    [SerializeField] int shotsAtOnce;
    [SerializeField] bool leadsShots;
    [SerializeField] float laserSpeed;

    [SerializeField] AudioSource laserAudio;
    [SerializeField] AudioClip[] laserSounds;

    [SerializeField] State[] states;

    bool returnToChase = false;

    private void Awake() {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        goals[0] = GameObject.FindGameObjectWithTag("PlayerAim").transform;
    }

    public override Transform[] getGoals() {
        return goals;
    }

    public override void InitialSwitch() {
        returnToChase = false;
        agent.speed = 1;
        agent.angularSpeed = 99999;
        StartCoroutine(Shoot());
    }

    public override State RunCurrentState() {
        if (returnToChase) {
            return chaseState;
        }
        return this;
    }

    public void leadShots() {
        gameObject.transform.rotation.SetLookRotation(goals[0].position);

        GameObject instantiatedProjectile = Instantiate(laser, transform.position, transform.rotation);
        Rigidbody rigidbody = instantiatedProjectile.GetComponent<Rigidbody>();

        Vector3 direction = goals[0].position - transform.position;
        float distance = direction.magnitude;
        float timeToHit = distance / laserSpeed;
        Vector3 futurePos = goals[0].position + (player.velocity * timeToHit);
        Vector3 newDir = futurePos - transform.position +
            new Vector3(Random.Range(-accuracy, accuracy), Random.Range(-accuracy, accuracy), Random.Range(-accuracy, accuracy));

        Vector3 aim = (newDir).normalized;
        instantiatedProjectile.transform.rotation = Quaternion.LookRotation(aim);
        rigidbody.velocity = aim * laserSpeed;
    }

    IEnumerator Shoot() {
        anim.SetTrigger("shoot");
        AnimatorClipInfo[] animClip = anim.GetCurrentAnimatorClipInfo(0);
        float animTime = animClip[0].clip.length / 2;
        yield return new WaitForSeconds(animTime + 0.1f);
        rayGunRenderer.enabled = true;
        particles.SetActive(true);

        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("Shooting")) {
            yield return null;
        }
        anim.speed = speedMultiplier;

        for (int i = 0; i < numShots; i++) {
            animClip = anim.GetCurrentAnimatorClipInfo(0);
            animTime = animClip[0].clip.length / (speedMultiplier * 2);
            yield return new WaitForSeconds(animTime);

            for (int j = 0; j < shotsAtOnce; j++) {
                if (!laserAudio.isPlaying) {
                    laserAudio.clip = laserSounds[Random.Range(0, 3)];
                    laserAudio.Play();
                }

                if (leadsShots) {
                    leadShots();
                } else {
                    transform.LookAt(goals[0].position + new Vector3(Random.Range(-accuracy, accuracy), Random.Range(-accuracy, accuracy), Random.Range(-accuracy, accuracy)));
                    GameObject instantiatedProjectile = Instantiate(laser, transform.position, transform.rotation);
                    Rigidbody rigidbody = instantiatedProjectile.GetComponent<Rigidbody>();
                    rigidbody.velocity = instantiatedProjectile.transform.forward * laserSpeed;
                }
            }
        }

        anim.speed = 1;
        anim.SetTrigger("shootingComplete");

        while (!anim.GetCurrentAnimatorStateInfo(0).IsName("Put Away Gun")) {
            yield return null;
        }
        yield return new WaitForSeconds(0.3f);
        rayGunRenderer.enabled = false;
        particles.SetActive(false);
        agent.speed = 20;
        returnToChase = true;
    }
}
