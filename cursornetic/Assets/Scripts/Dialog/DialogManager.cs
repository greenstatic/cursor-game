using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    public Text nameText;
    public Text DialogText;

    public Animator animator;

    private Queue<string> sentences;
    private Queue<MonoScript> scripts;
    private float previousTimeScale = 0f;

    private float previousGetButtonSubmit = 0;

    void Start() {
        sentences = new Queue<string>();
        scripts = new Queue<MonoScript>();
    }

    void Update() {
        if (GlobalState.isInDialog && Input.GetButton("Submit") && (Time.realtimeSinceStartup - previousGetButtonSubmit) > 1.0f) {
            DisplayNextSentence();
            previousGetButtonSubmit = Time.realtimeSinceStartup;
        }
    }

    public void StartDialog(Dialog Dialog) {
        GlobalState.isInDialog = true;

        if (Time.timeScale != 0f)
            previousTimeScale = Time.timeScale;
    
        Time.timeScale = 0f;

        animator.SetBool("IsOpen", true);

        nameText.text = Dialog.name;

        sentences.Clear();

        foreach (string sentence in Dialog.sentences) {
            sentences.Enqueue(sentence);
        }

        if (Dialog.Script) {
            scripts.Enqueue(Dialog.Script);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialog();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence) {
        DialogText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            DialogText.text += letter;
            yield return null;
        }
    }

    void EndDialog() {

        if (scripts.Count != 0) {
            for (int i = 0; i < scripts.Count; i++) {
                MonoScript script = scripts.Dequeue();
                MethodInfo scriptRunMethod = script.GetClass().GetMethod("Run");
                if (scriptRunMethod != null)
                    scriptRunMethod.Invoke(script.GetClass(), null);
            }
        }

        GlobalState.isInDialog = false;
        animator.SetBool("IsOpen", false);
        Time.timeScale = previousTimeScale;
    }

}
