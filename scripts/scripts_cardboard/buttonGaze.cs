using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class buttonGaze : MonoBehaviour
{
    public delegate void mensaje();
    public event mensaje resetGame;
    
    public Material InactiveMaterial;
    public Material GazedAtMaterial;

    public bool gazedAt = false;

    public TMP_Text barText;
    public int maxCounter = 40; // ~1500 en PC
    private int counter = 0;
    private int amount = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetMaterial();
    }

    // Update is called every frame
    void Update() {
        if(gazedAt && Input.GetMouseButtonDown(0)) {
            GetComponent<AudioSource>().Play(0);
            resetGame();
        }
        if(gazedAt) {
            counter++;
            amount = (int)((((float)counter / maxCounter) * 100)/4); // 100/4 = 20, y caben 20 '|' en la barra
            if(amount > 20) {
                barText.text = new string('|', 20);
            } else {
                barText.text = new string('|', amount);
            }
        }
        if(amount == 22) { // 22 Para mejorar el 'feel'
            GetComponent<AudioSource>().Play(0);
            resetGame();
            barText.text = ""; // Reset del estado aqui para quie se pueda dar a reset varias veces
            counter = 0;
            amount = 0;
        }
    }

    /**
     *  @desc This method is called by the Main Camera when it starts gazing at this GameObject.
     */
    public void OnPointerEnter() {
        gazedAt = true;
        SetMaterial();
    }

    /**
     *  @desc This method is called by the Main Camera when it stops gazing at this GameObject.
     */
    public void OnPointerExit() {
        gazedAt = false;
        SetMaterial();
        barText.text = "";
        counter = 0;
        amount = 0;
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
