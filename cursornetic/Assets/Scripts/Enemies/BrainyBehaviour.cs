using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class to manage movements of Brainy ememies
public class BrainyBehaviour : MonoBehaviour {

    private Rigidbody2D rb;
    private float distance;
    private GameObject player;

    public void Start() {
        rb = GetComponent<Rigidbody2D>();

    }

    public void Update() {

        player = GameObject.FindGameObjectWithTag("Player");
        distance = Vector2.Distance(transform.position, player.transform.position);


    }
}
