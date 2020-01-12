using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBar : MonoBehaviour {
    private RectTransform bar;

    public void SetSize(float sizeNormalized) {
        bar = GetComponent<RectTransform>();
        bar.localScale = new Vector2(sizeNormalized, 0.3f);
    }
}
