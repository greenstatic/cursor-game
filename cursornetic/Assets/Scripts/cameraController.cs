using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour {
    public Transform player;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    private Animator animator;

    private void Start() {
        animator = GetComponent<Animator>();
    }

    public void FixedUpdate() {
        // Camera follows the player with specified offset position and smoothing
        Vector3 finalPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, finalPosition, smoothSpeed * Time.fixedDeltaTime);
        
        transform.position = smoothedPosition;
    }

    public void ShakeCamera() {
        // animator.SetTrigger("Shake");
    }
}
