using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private RectTransform bar;

    // Start is called before the first frame update
    void Start(){
        bar = GetComponent<RectTransform>();
    }

    public void SetSize(float sizeNormalized) {
        Debug.Log(bar.localScale);
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
