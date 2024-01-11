using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonGaze : MonoBehaviour
{
    public delegate void mensaje();
    public event mensaje resetGame;

    public Material InactiveMaterial;
    public Material GazedAtMaterial;

    public bool clicked = false;
    public bool gazedAt = false;

    // Start is called before the first frame update
    void Start()
    {
        SetMaterial();
    }

    // Update is called every frame
    void Update() {
        if(gazedAt && clicked) {
            resetGame();
        }
    }

    public void OnHoverEntered() {
        gazedAt = true;
        SetMaterial();
    }

    public void OnHoverExited() {
        gazedAt = false;
        SetMaterial();
    }

    public void OnSelectEntered() {
        clicked = true;
    }

    public void OnSelectExited() {
        clicked = false;
    }

    /**
     *  @desc Cambia el material dependiendo del estado de gazedAt
     */
    private void SetMaterial() {
        if (InactiveMaterial != null && GazedAtMaterial != null) {
            GetComponent<Renderer>().material = gazedAt ? GazedAtMaterial : InactiveMaterial;
        }
    }
}
