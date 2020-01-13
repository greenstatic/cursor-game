﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalState {
    public static bool debug = Application.isEditor;

    public static bool spawnInMiddleCpu = true;

    public static float health = 100;
    public static int bullets = 0;
    public static bool playerHasWeapon = false; //debug;

    public static List<string> triggeredDialogs = new List<string>();
}
