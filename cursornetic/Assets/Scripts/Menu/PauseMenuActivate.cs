using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Add this script to each scene you wish the PauseMenu will be available

public class PauseMenuActivate : MonoBehaviour
{
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.Pause(this);
        }
    }

}
