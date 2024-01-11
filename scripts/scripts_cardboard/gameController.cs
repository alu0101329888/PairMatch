using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;
using TMPro;

public class gameController : MonoBehaviour
{
    public List<onGaze> notificador; // Comunicación con los "botones"
    public buttonGaze notificadorReset; // Comunicación con el boton reset
    public TMP_Text guessText;
    public TMP_Text winText;
    
    public List<GameObject> btns = new List<GameObject>();
    public Material[] colors;
    public List<Material> gameColors = new List<Material>();

    private bool firstGuess, secondGuess;
    private int countGuesses = 0, countCorrectGuesses = 0, gameGuesses;
    private int firstGuessIndex, secondGuessIndex;
    private string firstGuessName, secondGuessName;
    private int defaultLayer = 0, interactiveLayer = 3;  
    
    /**
     *  @desc Pasos a ejecutar al abrirse el script, viene antes de Start()
     */
    void Awake() {
        colors = Resources.LoadAll<Material>("GameColors");
    }
    
    /**
     *  @desc Pasos a ejecutar al comienzo
     */
    void Start()
    {
        foreach(onGaze notif in notificador) {
            notif.tileInteract += pickAPuzzle;
        }
        notificadorReset.resetGame += resetState;
        GetButtons();
        AddGameMats();
        shuffle(gameColors);
        gameGuesses = gameColors.Count / 2;
    }

    /**
     *  @desc Consigue una lista con x2 de cada Material
     */
    void AddGameMats() {
        int looper = btns.Count;
        int index = 0;

        for(int i = 0; i < looper; i++) {
            if(index == looper/2) {
                index = 0;
            }
            gameColors.Add(colors[index]);
            index++;
        }
    }

    /**
     *  @desc Consigue la lista (desordenada) de todos los "botones"
     */
    void GetButtons() {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("Puzzle Tile");
        for(int i = 0; i < objects.Length; i++) {
            btns.Add(objects[i]);
        }
    }

    /**
     *  @desc Maneja la elección de piezas por parte del usuario
     */
    public void pickAPuzzle(string name) {
        if(!firstGuess) { // Primer Intento
            firstGuess = true;
            for(int i = 0; i < btns.Count; i++) { // Buscar indice del elemento con "name"
                if(btns[i].name == name) {
                    firstGuessIndex = i;
                    break;
                } 
            }
            btns[firstGuessIndex].transform.Find("Color").GetComponent<MeshRenderer>().material = gameColors[int.Parse(name)]; // Asignar color apropiado
            firstGuessName = gameColors[int.Parse(name)].name; // Nombre del Color
            btns[firstGuessIndex].transform.Rotate(0, 180, 0);
            btns[firstGuessIndex].transform.Find("Base").GetComponent<onGaze>().locked = true;
            changeAppearence(defaultLayer, true, firstGuessIndex);

        } else if(!secondGuess && name != btns[firstGuessIndex].name) { // Segundo Intento, no pueden ser la misma casilla
            secondGuess = true;
            for(int i = 0; i < btns.Count; i++) {
                if(btns[i].name == name) {
                    secondGuessIndex = i;
                    break;
                } 
            }
            btns[secondGuessIndex].transform.Find("Color").GetComponent<MeshRenderer>().material = gameColors[int.Parse(name)];
            secondGuessName = gameColors[int.Parse(name)].name; // name != secondGuessIndex (btns no esta ordenado, gameColors si)
            btns[secondGuessIndex].transform.Rotate(0, 180, 0);
            btns[secondGuessIndex].transform.Find("Base").GetComponent<onGaze>().locked = true;
            changeAppearence(defaultLayer, true, secondGuessIndex);
            
            StartCoroutine(checkIfPuzzlesMatch());
        }
    }

    /**
     *  @desc Corutina que mira ver si las dos piezas seleccionadas son iguales
     */
    IEnumerator checkIfPuzzlesMatch() {
        
        yield return new WaitForSeconds(1f);

        countGuesses++;
        guessText.text = "Guesses: \n" + countGuesses.ToString();
        if(firstGuessName == secondGuessName) {
            GetComponent<AudioSource>().Play(0);
            changeAppearence(defaultLayer, false, firstGuessIndex);
            changeAppearence(defaultLayer, false, secondGuessIndex);
            buttonsTurnReset(true); // True para que no se pueda interactuar con "botones" ya acertados
            checkIfTheGameIsFinished();
        } else {
            changeAppearence(interactiveLayer, true, firstGuessIndex);
            changeAppearence(interactiveLayer, true, secondGuessIndex);
            buttonsTurnReset();
        }

        firstGuess = secondGuess = false;
    }

    /**
     *  @desc Mira ver si la partida se ha concluido
     */
    void checkIfTheGameIsFinished() { 
        countCorrectGuesses++;
        if(countCorrectGuesses == gameGuesses) {
            winText.text = "COMPLETED";
        }
    }

    /**
     *  @desc Se puede modificar la capa y visibilidad de un elemento de btns
     */
    void changeAppearence(int layerNum, bool visibility, int pos) {
        btns[pos].layer = layerNum;
        foreach(Transform child in btns[pos].transform) {
            child.GetComponent<MeshRenderer>().enabled = visibility;
            child.gameObject.layer = layerNum;
        }
    }

    /**
     *  @desc Baraja la lista de colores a asignar
     */
    void shuffle(List<Material> list) {
        for(int i = 0; i < list.Count; i++) {
            Material temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    /**
     *  @desc Vuelva la funcionalidad interactiva a los botones (con respecto a la reticula) y su rotación
     */
    void buttonsTurnReset(bool lockVar = false) {
        btns[firstGuessIndex].transform.Rotate(0, -180, 0);
        btns[firstGuessIndex].transform.Find("Base").GetComponent<onGaze>().locked = lockVar;
        btns[secondGuessIndex].transform.Rotate(0, -180, 0);
        btns[secondGuessIndex].transform.Find("Base").GetComponent<onGaze>().locked = lockVar;
    }

    /**
     *  @desc Devuelve el juego a su estado original
     */
    void resetState() {
        if(firstGuess && !secondGuess) { // Si solo se ha elegido un boton, todavia saldra un guess adicional pero esto se dejara asi
            btns[firstGuessIndex].transform.Rotate(0, -180, 0);
        }
        firstGuess = secondGuess = false; // Ambos intentos reseteados, por si se activa en medio de la partida
        countGuesses = countCorrectGuesses = 0; // Reseteamos los intentos
        guessText.text = "Guesses: \n" + countGuesses.ToString(); // ACtualizar texto a reflejar los intentos actuales
        winText.text = ""; // Por si se ha ganado
        for(int i = 0; i < btns.Count; i++) { // Visibilidad e interactividad de los botones
            changeAppearence(interactiveLayer, true, i);
            btns[i].transform.Find("Base").GetComponent<onGaze>().locked = false;
        }
        shuffle(gameColors); // Reasignar colores
    }
}
