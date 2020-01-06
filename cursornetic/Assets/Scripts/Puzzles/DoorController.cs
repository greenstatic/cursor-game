using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    public SpriteRenderer spriteButton;
    public Sprite spriteOn;

    public void Start() {
        spriteButton.sprite = spriteButton.sprite;
    }

    public void TurnOn() {
        GetComponent<Animator>().SetTrigger("TurnOn");
        spriteButton.sprite = spriteOn;
    }
}
