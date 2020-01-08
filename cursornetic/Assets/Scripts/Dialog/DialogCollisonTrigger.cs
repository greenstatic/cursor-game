using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCollisonTrigger : MonoBehaviour {

    public DialogTrigger dialogTrigger;
    public string uniqueName;
    public bool onlyOnce = true;

    public void Start() {
        if (dialogTrigger == null) {
            Debug.LogWarning("Missing dialogTrigger variable in DialogCollisonTrigger");
        }

        if (uniqueName == "" || uniqueName == null) {
            Debug.LogWarning("Missing uniqueName variable in DialogCollisonTrigger");
        }

    }

    public void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "Player") {
            if (onlyOnce && GlobalState.triggeredDialogs.Contains(uniqueName)) {
                return;
            }

            if (onlyOnce) {
                GlobalState.triggeredDialogs.Add(uniqueName);
            }

            dialogTrigger.TriggerDialogue();
            return;
        }
    }
}
