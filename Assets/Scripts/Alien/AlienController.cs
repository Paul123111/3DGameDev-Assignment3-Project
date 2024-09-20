using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class AlienController : MonoBehaviour
{

    [SerializeField] int maxHealth;
    [SerializeField] float laserSpeed;
    [SerializeField] int fireRate;
    int health;
    float astronautArmour = 0;

    [SerializeField] GameObject[] hitbox;
    [SerializeField] GameObject ragdoll;
    [SerializeField] GameObject alienGeometry;
    [SerializeField] GameObject alienLaser;
    [SerializeField] GameObject[] rayGun;
    [SerializeField] Transform[] muzzle;
    [SerializeField] GameObject alienAstronaut;
    [SerializeField] Avatar alienAvatar;
    [SerializeField] Avatar alienAstronautAvatar;
    [SerializeField] ArmourPercentUI armourPercentUI;
    [SerializeField] GameObject gameOverUI;

    [SerializeField] GameObject normalView;
    [SerializeField] GameObject aimView;

    [SerializeField] AudioSource attackAudio;
    [SerializeField] AudioClip[] laserSounds;
    [SerializeField] AudioClip punch;
    
    [SerializeField] AudioSource hurtAudio;
    [SerializeField] AudioSource breakAudio;
    [SerializeField] bool bossFight;

    Animator anim;
    bool currentlyPunching = false;
    bool currentlyShooting = false;
    bool alive = true;
    bool aiming = false;
    int alienType = 0;
    bool canLoot = false;
    GameObject nearby;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        health = maxHealth;
        //SwapToAstronaut();
        StartCoroutine(bossArmour());
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive) return;
        if (health <= 0) {
            gameOverUI.SetActive(true);
            Instantiate(ragdoll, transform.position, transform.rotation);
            alienGeometry.SetActive(false);
            alive = false;
        }

        Aim();
    }

    void Aim() {
        if (alienType == 1 && Input.GetMouseButton(1)) {
            anim.SetBool("isAiming", true);
            aiming = true;
            normalView.SetActive(false);
            aimView.SetActive(true);
            if (!rayGun[alienType].activeSelf) {
                rayGun[alienType].SetActive(true);
            }
        } else {
            anim.SetBool("isAiming", false);
            aiming = false;
            normalView.SetActive(true);
            aimView.SetActive(false);
            if (rayGun[alienType].activeSelf) {
                rayGun[alienType].SetActive(false);
            }
        }

        if (!currentlyPunching && !currentlyShooting) {
            StartCoroutine(Shooting());
        }
    }

    void OnPunch() {
        if (!currentlyPunching && !currentlyShooting) {
            StartCoroutine(Punching());
        }
    }

    void OnInteract() {
        if (canLoot && alive) {
            SwapToAstronaut();
            Destroy(nearby);
            canLoot = false;
        }
    }

    void OnSprint() {
        //anim.SetBool("isAiming", true);
    }

    IEnumerator Punching() {
        currentlyPunching = true;
        anim.SetTrigger("isPunching");
        yield return new WaitForSeconds(0.2f);
        hitbox[alienType].SetActive(true);

        if (!attackAudio.isPlaying) {
            attackAudio.clip = punch;
            attackAudio.Play();
        }

        yield return new WaitForSeconds(0.2f);
        hitbox[alienType].SetActive(false);
        while (anim.GetCurrentAnimatorStateInfo(0).IsName("Punching2")) {
            yield return null;
        }
        currentlyPunching = false;
    }

    IEnumerator Shooting() {
        if (aiming && Mouse.current.leftButton.isPressed) {
            currentlyShooting = true;
            //anim.SetTrigger("isShooting");

            yield return new WaitForSeconds((float) 1/fireRate);
            if (!attackAudio.isPlaying) {
                attackAudio.clip = laserSounds[Random.Range(0, 3)];
                attackAudio.Play();
            }
            GameObject instantiatedProjectile = Instantiate(alienLaser, muzzle[alienType].position, muzzle[alienType].rotation);
            Rigidbody rigidbody = instantiatedProjectile.GetComponent<Rigidbody>();

            instantiatedProjectile.transform.rotation = Quaternion.LookRotation(muzzle[alienType].forward);
            rigidbody.velocity = muzzle[alienType].forward * laserSpeed;

            decreaseHealth(0.5f);

            currentlyShooting = false;

        }
    }

    void SwapToAstronaut() {
        alienGeometry.SetActive(false);
        alienAstronaut.SetActive(true);
        anim.avatar = alienAstronautAvatar;
        alienType = 1;
        astronautArmour += 40;
        if (astronautArmour > 100) {
            astronautArmour = 100;
        }
        armourPercentUI.SetPercent(astronautArmour);
    }

    void SwapToAlien() {
        if (!breakAudio.isPlaying) {
            breakAudio.Play();
        }
        alienGeometry.SetActive(true);
        alienAstronaut.SetActive(false);
        anim.avatar = alienAvatar;
        alienType = 0;
    }

    public int getHealth() {
        return health;
    }

    public int getMaxHealth() {
        return maxHealth;
    }

    public void setHealth(int health) {
        this.health = health;
    }

    public void increaseHealth(int heal) {
        if (alive) {
        health += heal;
        if (health > maxHealth) {
                health = maxHealth;
            }
        }
    }

    public void decreaseHealth(float damage) {
        if (astronautArmour > 0) {
            astronautArmour -= damage;
            //armour broke
            if (astronautArmour <= 0) {
                SwapToAlien();
                astronautArmour = 0;
            }
            armourPercentUI.SetPercent(astronautArmour);
        } else {
            health -= (int) damage;
        }
    }

    public void hurt() {
        if (!hurtAudio.isPlaying && alive) {
            hurtAudio.Play();
        }
    }

    IEnumerator bossArmour() {
        yield return new WaitForEndOfFrame();
        if (bossFight) {
            astronautArmour = 100;
            SwapToAstronaut();
        }
    }

    public bool getAlive() {
        return alive;
    }

    public bool getAiming() {
        return aiming;
    }

    public bool getCanLoot() {
        return canLoot;
    }

    public void setCanLoot(bool canLoot) {
        this.canLoot = canLoot;
    }

    public GameObject getNearby() {
        return nearby;
    }

    public void setNearby(GameObject nearby) {
        this.nearby = nearby;
    }

    void OnRetry() {
        if (!alive) {
            GameSession gameSession = FindFirstObjectByType<GameSession>();
            alive = true;
            gameSession.Restart();
        }
    }

    private void OnQuit() {
        //Debug.Log("Bye");
        Application.Quit();
    }
}
