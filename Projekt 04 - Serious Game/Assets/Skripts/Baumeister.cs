using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Baumeister : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public Button nextButton;
    public GameObject baumeister;

    public GameObject spielfeld;
    public GameObject[] spielsteine;
    public Vector3Int boardSize = new Vector3Int(5, 2, 5); 

    public Spielstand spielstand;

    public List<int[,,]> musterListe = new List<int[,,]>{
        new int[,,] {
            {
                { 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1 },
                { 1, 1, 0, 1, 1 }
            },
            {
                { 1, 0, 0, 0, 1 },
                { 0, 0, 0, 0, 0 },
                { 1, 0, 0, 0, 1 },
                { 0, 0, 0, 0, 0 },
                { 1, 0, 0, 0, 1 }
            }
        },
        new int[,,] {
            {
                { 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1 },
                { 0, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1 }
            },
            {
                { 1, 0, 1, 0, 1 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 1, 0, 1, 0, 1 }
            }
        },
        new int[,,] {
            {
                { 1, 1, 0, 1, 1 },
                { 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1 }
            },
            {
                { 1, 0, 0, 0, 1 },
                { 0, 0, 0, 0, 0 },
                { 1, 0, 0, 0, 1 },
                { 0, 0, 0, 0, 0 },
                { 1, 0, 0, 0, 1 }
            }
        },
        new int[,,] {
            {
                { 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 0 },
                { 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1 }
            },
            {
                { 1, 0, 1, 0, 1 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 0, 0, 0, 0, 0 },
                { 1, 0, 1, 0, 1 }
            }
        }
    };


    /*public void OnButtonClick(Button clickedButton){
        if(clickedButton.name == "Button confirm"){
            buttonAccept();
        }
        if(clickedButton.name == "Button Next"){
            baumeister.gameObject.SetActive(false);
        }
    }*/

    public void buttonAccept(){
        if(proveAllPatterns()){
            textMeshPro.text += "\nHerzlichen Glückwunsch! Das sieht gut aus! Du erhälst einen Punkt.";
            spielstand.incrementPunkteStand();
        }else{
            textMeshPro.text += "\nIn deinem Bauwerk hat sich leider ein Fehler eingeschlichen. Dafür gibt es keinen Punkt :(";
        }
        ActivateNext();
    }

    void ActivateNext(){
        nextButton.gameObject.SetActive(true);
    }

    bool provePattern(int[,,] muster){
        int[,,] field = new int[boardSize.x, boardSize.y, boardSize.z];
        
        Vector3 spielfeldPosition = spielfeld.transform.position;
        Vector3 spielfeldScale = spielfeld.transform.localScale;

        float cellBreite = spielfeldScale.x / boardSize.x;
        float cellHöhe = spielfeldScale.y / boardSize.y;
        float cellTiefe = spielfeldScale.z / boardSize.z;

        foreach (GameObject stein in spielsteine)
        {
            Vector3 localPos = stein.transform.position - spielfeldPosition;
            int x = Mathf.FloorToInt(localPos.x / cellBreite);
            int y = Mathf.FloorToInt(localPos.y / cellHöhe);
            int z = Mathf.FloorToInt(localPos.z / cellTiefe);

            
            if (x >= 0 && x < boardSize.x && y >= 0 && y < boardSize.y && z >= 0 && z < boardSize.z)
            {
                field[x, y, z] = 1;
            }
        }

        for (int x = 0; x < boardSize.x; x++)
        {
            for (int y = 0; y < boardSize.y; y++)
            {
                for (int z = 0; z < boardSize.z; z++)
                {
                    if (field[x, y, z] != muster[x, y, z])
                    {
                        return false;
                    }
                }
            }
        }

        return true;
    }

    bool proveAllPatterns() {
        foreach (var muster in musterListe){
            if (provePattern(muster)){
                return true;
            }
        }
        return false;
    }
}
