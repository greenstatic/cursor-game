using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalState {
    public static bool debug = Application.isEditor;

    public static bool spawnInMiddleCpu = true;

    public static float health = 100;
    public static int bullets = 0;
    public static bool playerHasWeapon = debug;
    public static bool playerHasSlowTime = debug;

    public static List<string> triggeredDialogs = new List<string>();
    public static bool isInDialog = false;

    public static bool brainyAlive = true;
    public static bool wormAlive = true;
    public static bool hasWon = false;

    public static bool wormCanShoot = true;

    public static void Reset() {
        // Deadlines make us do crazy things.

        spawnInMiddleCpu = true;
        health = 100;
        bullets = 0;
        playerHasWeapon = debug;

        playerHasSlowTime = debug;

        triggeredDialogs = new List<string>();
        isInDialog = false;

        brainyAlive = true;
        wormAlive = true;
        hasWon = false;

        wormCanShoot = true;
    }
}
