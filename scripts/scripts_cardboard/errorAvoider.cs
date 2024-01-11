using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class errorAvoider : MonoBehaviour
{
    private bool doNothing;

    /**
     *  @desc This method is called by the Main Camera when it starts gazing at this GameObject.
     */
    public void OnPointerEnter() {
        doNothing = true;
    }

    /**
     *  @desc This method is called by the Main Camera when it stops gazing at this GameObject.
     */
    public void OnPointerExit() {
        doNothing = false;
    }
}
