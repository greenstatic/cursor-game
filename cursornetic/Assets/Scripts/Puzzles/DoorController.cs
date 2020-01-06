using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    private SpriteRenderer spriteButton;
    public Sprite spriteOn;

    public void Start() {
        spriteButton = GetComponent<SpriteRenderer>();
    }

    public void TurnOn() {
        GetComponent<Animator>().SetTrigger("TurnOn");
        spriteButton.sprite = spriteOn;
        FindObjectOfType<AudioManager>().Play("Switch");
    }
}
