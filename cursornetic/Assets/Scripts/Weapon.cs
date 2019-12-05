using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public Transform firePoint;
    public GameObject bulletPrefab;

    public int bulletRefillTimeDelta = 3;

    public GameObject bulletUI1;
    public GameObject bulletUI2;
    public GameObject bulletUI3;

    private int maxBullets = 3;  // this should reflect bulletUI* elements
    private float lastBulletSpawn = 0;

    // Start is called before the first frame update
    void Start() {
        BulletUISet(GetNumberOfBullets());
    }

    // Update is called once per frame
    void Update() {
        float time = Time.time;
        if (GetNumberOfBullets() < maxBullets && lastBulletSpawn + bulletRefillTimeDelta < time) {
            // respawn
            GlobalState.bullets++;
            lastBulletSpawn = time;
        }

        if (CanShoot() && Input.GetButtonDown("Fire1")) {
            Shoot();
            GlobalState.bullets--;
        }

        BulletUISet(GlobalState.bullets);
    }

    void Shoot() {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bulletPrefab.tag = "Bullet";
    }

    void BulletUISet(int bullets) {
        if (bullets > maxBullets) {
            Debug.LogWarning(string.Format("Number of bullets cannot be over {0}", maxBullets));
        }

        switch (bullets) {
            case 1:
                BulletEnableDisable(bulletUI1, true);
                BulletEnableDisable(bulletUI2, false);
                BulletEnableDisable(bulletUI3, false);
                break;
            case 2:
                BulletEnableDisable(bulletUI1, true);
                BulletEnableDisable(bulletUI2, true);
                BulletEnableDisable(bulletUI3, false);
                break;
            case 3:
                BulletEnableDisable(bulletUI1, true);
                BulletEnableDisable(bulletUI2, true);
                BulletEnableDisable(bulletUI3, true);
                break;
            default:
                BulletEnableDisable(bulletUI1, false);
                BulletEnableDisable(bulletUI2, false);
                BulletEnableDisable(bulletUI3, false);
                break;
        }
    }

    void BulletEnableDisable(GameObject bulletUI, bool enable) {
        if (bulletUI) {
            SpriteRenderer sr = bulletUI.GetComponent<SpriteRenderer>();
            if (sr) {
                sr.enabled = enable;
            }
        }
    }

    int GetNumberOfBullets() {
        return GlobalState.bullets;
    }

    bool CanShoot() {
        return GlobalState.bullets > 0;
    }
}
