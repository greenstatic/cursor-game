using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour {

    public float speed;
    public float angle;
    private Rigidbody2D rb;
    public bool sprint = false;
    public float waitTime = 2;

    private Vector2 moveInput;
    private Vector2 moveVelocity;
    private Vector2 frontDirection;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        moveVelocity = moveInput.normalized * speed;

        if (Input.GetButton("FrontAttack")) {
            sprint = true;
        }
    }

    void FixedUpdate() {
        
        // Translation
        rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);

        // Rotation
        if (moveInput != Vector2.zero) {
            angle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }

        if (sprint) {
            frontDirection = new Vector2(transform.right.x, transform.right.y);
            rb.MovePosition(rb.position - 50 * frontDirection * Time.fixedDeltaTime);
            rb.MovePosition(rb.position + 20 * frontDirection * Time.fixedDeltaTime);
            sprint = false;
            new WaitForSecondsRealtime(waitTime);
        }
    }
}
