using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class onGaze : MonoBehaviour
{
    public delegate void mensaje(string name);
    public event mensaje tileInteract;
    
    public Material InactiveMaterial;
    public Material GazedAtMaterial;
    public XRSimpleInteractable module;

    public bool clicked = false;
    public bool gazedAt = false;
    public bool locked = false; // Bool para que el GameManager pueda bloquear el cambio de color o envio de evento

    // Start is called before the first frame update
    void Start()
    {
        SetMaterial();
    }

    // Update is called every frame
    void Update() {
        if(gazedAt && clicked && !locked) {
            tileInteract(transform.parent.name);
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
            GetComponent<Renderer>().material = gazedAt && !locked ? GazedAtMaterial : InactiveMaterial;
        }
    }
}
