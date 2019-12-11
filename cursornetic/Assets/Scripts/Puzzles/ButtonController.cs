using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private bool state = false; //T=on, F=off
    public SpriteRenderer sprite;
    public Sprite spriteOn, spriteOff;
    private GameObject[] walls;

    public void Start() {
       walls = GameObject.FindGameObjectsWithTag("ElectricWall");
    }

    public void TurnOn() {
        state = true;
        GetComponent<Animator>().SetTrigger("TurnOn");
        sprite.sprite = spriteOn;
    }

    public void TurnOff() {
        state = false;
        GetComponent<Animator>().SetTrigger("TurnOff");
        sprite.sprite = spriteOff;
    }

    public void Toggle() {
        if (state)
            TurnOff();
        else
            TurnOn();

        for (int i = 0; i < walls.Length; i++) {
            ElectricWallController wallScript = walls[i].GetComponent<ElectricWallController>();
            wallScript.Toggle();
            FindObjectOfType<AudioManager>().Play("Switch");
        }
    }
}
