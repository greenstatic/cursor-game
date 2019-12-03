using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private bool state = false;
    public SpriteRenderer sprite;
    public Sprite spriteOn, spriteOff;
    private GameObject[] walls;

    public void Start() {
       walls = GameObject.FindGameObjectsWithTag("ElectricWall");
    }

    public void TurnOn() {
        state = true;
        sprite.sprite = spriteOn;
    }

    public void TurnOff() {
        state = false;
        sprite.sprite = spriteOff;
    }

    public void Toggle() {
        if (!state)
            TurnOn();
        else
            TurnOff();

        for (int i = 0; i < walls.Length; i++) {
            ElectricWallController wallScript = walls[i].GetComponent<ElectricWallController>();
            wallScript.Toggle();
        }
    }
}
