using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private bool state = false;
    public SpriteRenderer sprite;
    public Sprite spriteOn, spriteOff;

    public void TurnOn() {
        state = true;
        sprite.sprite = spriteOn;
    }

    public void TurnOff() {
        state = false;
        sprite.sprite = spriteOff;
    }

    public void Toggle() {
        if (state)
            TurnOn();
        else
            TurnOff();
    }

    public void OnTriggerEnter2D(Collider2D col) {
        if (col.CompareTag("Player")) {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player.GetComponent<playerController>().isDashing) {
                Toggle();
            }

        } else if (col.CompareTag("Bullet")) {
            Toggle();
        }
    }
}
