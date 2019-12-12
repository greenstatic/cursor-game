using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseCursor : MonoBehaviour {

    // Start is called before the first frame update
    void Start() {
        Disable();
    }

    // Update is called once per frame
    void Update() {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = cursorPos;
    }

    public static void Enable() {
        Cursor.visible = true;
    }

    public static void Disable() {
        Cursor.visible = false;
    }
}
