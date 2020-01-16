using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityWeapon
{
    // This function is automatically called by DialogManager when attached to a DialogTrigger
    public static void Run() {
        Debug.Log("GIving player weapon.");
        Weapon.EnableWeapon();
    }
}
