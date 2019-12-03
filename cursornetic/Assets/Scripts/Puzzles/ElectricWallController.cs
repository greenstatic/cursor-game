using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricWallController : MonoBehaviour
{
    public bool state;
    public SpriteRenderer sprite;
    public Collider2D coll;

    public void Start() {
        Toggle();
        Toggle();
    }

    public void TurnOn() {
        state = true;
        sprite.enabled = true;
        coll.enabled = true;
    }

    public void TurnOff() {
        state = false;
        sprite.enabled = false;
        coll.enabled = false;
    }

    public void Toggle() {
        if (state)
            TurnOff();
        else
            TurnOn();
    }
}
