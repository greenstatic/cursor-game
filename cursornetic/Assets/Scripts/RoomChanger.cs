using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChanger : MonoBehaviour {

    private GameObject player, cam;
    public GameObject nextDoor;
    //public Animator animator;

    private Vector2 interval;
    private Vector2 doorLoc;

    private Vector2 Up = new Vector2(0, 1);
    private Vector2 Right = new Vector2(1, 0);
    private Vector2 Down = new Vector2(0, -1);
    private Vector2 Left = new Vector2(-1, 0);

    private void OnTriggerEnter2D(Collider2D col) {

        if (col.CompareTag("Player")) {

            //PlayTransition(); // (In case we decide to add more sofisticated animation)

            // Retrieving camera and player objects in scene
            player = GameObject.FindGameObjectWithTag("Player");
            cam = GameObject.FindGameObjectWithTag("MainCamera");

            // To be setted in the inspector
            doorLoc = nextDoor.transform.position;

            if (nextDoor.tag.Equals("Up")) 
                interval = Up;
            else if (nextDoor.tag.Equals("Right")) 
                interval = Right;
            else if (nextDoor.tag.Equals("Down"))
                interval = Down;
            else if (nextDoor.tag.Equals("Left"))
                interval = Left;

            // Setting location of player and camera for next room
            player.transform.position = doorLoc + interval*2;
            cam.transform.position = doorLoc + interval;
        }
    }

    //public void PlayTransition() {
    //    animator.SetTrigger("TransitionIn");
    //}
}
