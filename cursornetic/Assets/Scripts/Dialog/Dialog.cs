using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class Dialog {

    public string name;

    [TextArea(3, 10)]
    public string[] sentences;

    public string Script;
}
