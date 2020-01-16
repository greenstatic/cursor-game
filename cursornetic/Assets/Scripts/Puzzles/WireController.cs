using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WireController : MonoBehaviour {

    [HideInInspector]
    public bool state = false; //T=on, F=off

    private SpriteRenderer spriteButton;
    public Sprite spriteOn, spriteOff;
    public GameObject wire;
    public Sprite lightOn, lightOff;

    public void Start() {
        spriteButton = GetComponent<SpriteRenderer>();
    }

    public void TurnOn() {
        state = true;

        FindObjectOfType<AudioManager>().Play("Switch");
        GetComponent<Animator>().SetTrigger("TurnOn");
        spriteButton.sprite = spriteOn;

        if (wire != null)
            wire.GetComponent<SpriteRenderer>().sprite = lightOn;

        // Add the button to the sequence
        transform.parent.GetComponent<WirePuzzleController>().ButtonPress(gameObject);
        if (GetComponent<WirePuzzleController>() != null)
            GetComponent<WirePuzzleController>().ButtonPress(gameObject);
    }

    public void TurnOff() {
        state = false;

        FindObjectOfType<AudioManager>().Play("Switch");
        GetComponent<Animator>().SetTrigger("TurnOff");
        spriteButton.sprite = spriteOff;

        if (wire != null)
            wire.GetComponent<SpriteRenderer>().sprite = lightOff;
    }
}