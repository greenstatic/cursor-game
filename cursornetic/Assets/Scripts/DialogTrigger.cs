using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour {

    public void OnTriggerEnter2D(Collider2D col) {
        if (this.name == "DialogTriggerGear") {
            DialogGear(col);
            return;
        }

        // We can add here other DialogTriggers for other dialogs, then simply
        // add the dialog specific function to trigger on collison.
    }

    // Dialog Trigger for the Gear character.
    public void DialogGear(Collider2D col) {
        // Enable the weapons system for the player.
        Weapon.EnableWeapon();
    }
}
