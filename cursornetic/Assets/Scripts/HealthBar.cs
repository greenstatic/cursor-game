using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private RectTransform bar;

    public void SetSize(float sizeNormalized) {
        bar = GetComponent<RectTransform>();
        bar.localScale = new Vector2(sizeNormalized, 0.3f);

        if (sizeNormalized > 0.5f)
            SetColor(Color.green);
        else if (sizeNormalized <= 0.5f && sizeNormalized > 0.25f)
            SetColor(Color.yellow);
        else
            SetColor(Color.red);
    }

    public void SetColor(Color color) {
        GetComponent<Image>().color = color;
    }
}
