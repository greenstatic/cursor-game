using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireController : MonoBehaviour {

    private bool state = false; //T=on, F=off
    private SpriteRenderer spriteButton;
    public Sprite spriteOn, spriteOff;
    public GameObject wire;
    public Sprite lightOn, lightOff;
    public float timeOn;

    public void Start() {
        spriteButton = GetComponent<SpriteRenderer>();
    }

    public void TurnOn() {
        state = true;
        GetComponent<Animator>().SetTrigger("TurnOn");
        spriteButton.sprite = spriteOn;
        FindObjectOfType<AudioManager>().Play("Switch");

        wire.GetComponent<SpriteRenderer>().sprite = lightOn;

        StartCoroutine(Timer());
    }

    public void TurnOff() {
        state = false;
        FindObjectOfType<AudioManager>().Play("Switch");
        GetComponent<Animator>().SetTrigger("TurnOff");
        spriteButton.sprite = spriteOff;

        wire.GetComponent<SpriteRenderer>().sprite = lightOff;
    }
    
    IEnumerator Timer() {
        yield return new  WaitForSeconds(timeOn);
        TurnOff();
    }
}