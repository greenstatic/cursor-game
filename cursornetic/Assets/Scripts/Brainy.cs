using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brainy : MonoBehaviour {

    public Sprite left;
    public Sprite right;

    private float previousXCoord;
    private bool movementSpriteIsRight;

    void Start() {
        if (left == null) {
            Debug.LogWarning("No left sprite for Brainy");
        }
        if (right == null) {
            Debug.LogWarning("No right sprite for Brainy");
        }

        MovementSpriteSetInit();
    }

    void Update() {
        MovementSpriteUpdate();
    }
    private void MovementSpriteUpdate() {
        float currentXCoord = this.transform.position.x;

        if (currentXCoord - previousXCoord > 0) {
            // Right sprite
            if (!movementSpriteIsRight)
                MovementSpriteSet(true);
        }
        else {
            // Left sprite
            if (movementSpriteIsRight)
                MovementSpriteSet(false);
        }

        previousXCoord = currentXCoord;
    }

    private void MovementSpriteSet(bool movingRight) {
        SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
        if (!sr) {
            Debug.LogWarning("Brainy script game object does not contain Sprite Renderer");
            return;
        }

        if (movingRight) {
            sr.sprite = right;
            movementSpriteIsRight = true;
        } else {
            sr.sprite = left;
            movementSpriteIsRight = false;
        }
    }

    private void MovementSpriteSetInit() {
        MovementSpriteSet(true);
    }
}
