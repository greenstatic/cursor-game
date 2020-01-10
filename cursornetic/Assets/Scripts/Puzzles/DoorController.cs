using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    private bool state;
    private SpriteRenderer spriteButton;
    public Sprite spriteOn, spriteOff;

    public void Start() {
        spriteButton = GetComponent<SpriteRenderer>();
    }

    public void TurnOn() {
        state = true;
        GetComponent<Animator>().SetTrigger("TurnOn");
        spriteButton.sprite = spriteOn;
        FindObjectOfType<AudioManager>().Play("Switch");
    }

    public void TurnOff() {
        state = false;
        GetComponent<Animator>().SetTrigger("TurnOff");
        spriteButton.sprite = spriteOff;
        FindObjectOfType<AudioManager>().Play("Switch");
    }

    public void Toggle() {
        if (state)
            TurnOff();
        else
            TurnOn();
    }
}
