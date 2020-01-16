using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHourglass : MonoBehaviour
{
    // This function is automatically called by DialogManager when attached to a DialogTrigger
    public static void Run() {
        Debug.Log("Giving player hourglass ability.");
        GlobalState.playerHasSlowTime = true;
    }
}
