using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

    public Text nameText;
    public Text DialogText;

    public Animator animator;

    private Queue<string> sentences;
    private float previousTimeScale = 0f;
    private bool inDialog = false;

    void Start() {
        sentences = new Queue<string>();
    }

    void Update() {
        if (inDialog && Input.GetButton("Submit")) {
            DisplayNextSentence();
        }
    }

    public void StartDialog(Dialog Dialog) {
        inDialog = true;

        if (Time.timeScale != 0f)
            previousTimeScale = Time.timeScale;
    
        Time.timeScale = 0f;

        animator.SetBool("IsOpen", true);

        nameText.text = Dialog.name;

        sentences.Clear();

        foreach (string sentence in Dialog.sentences) {
            sentences.Enqueue(sentence);
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
        inDialog = false;
        animator.SetBool("IsOpen", false);
        Time.timeScale = previousTimeScale;
    }

}
