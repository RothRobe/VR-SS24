using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;


public class Baumeister : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public GameObject retryButton;
    public GameObject confirmButton;
    public GameObject würfelzählen;

    public GameObject spielfeld;
    public GameObject[] spielsteine;

    public GameObject[] spielsteinCubes;
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

    private Vector3[] _steinposition;
    private Quaternion[] _steinrotation;

    private void Start()
    {
        int length = spielsteine.Length;
        _steinposition = new Vector3[length];
        _steinrotation = new Quaternion[length];
        for (int i = 0; i < length; i++)
        {
            _steinposition[i] = spielsteine[i].transform.position;
            _steinrotation[i] = spielsteine[i].transform.rotation;
        }
    }

    /*public void OnButtonClick(Button clickedButton){
        if(clickedButton.name == "Button confirm"){
            buttonAccept();
        }
        if(clickedButton.name == "Button Next"){
            baumeister.gameObject.SetActive(false);
        }
    }*/

    public void buttonAccept()
    {
        retryButton.SetActive(true);
        confirmButton.SetActive(false);
        if(proveAllPatterns()){
            textMeshPro.text = "\nHerzlichen Glückwunsch! Das sieht gut aus! Du erhälst einen Punkt.";
            spielstand.incrementPunkteStand();
        }else{
            textMeshPro.text = "\nIn deinem Bauwerk hat sich leider ein Fehler eingeschlichen. Dafür gibt es keinen Punkt :(";
        }

        textMeshPro.text += "\nDu hast " + spielstand.GetPunkteStand() + " von " + spielstand.GetMaxPunkte() +
                           " Punkten erreicht.";
    }

    public void Retry()
    {
        retryButton.gameObject.SetActive(false);
        confirmButton.SetActive(true);
        textMeshPro.text = "";
        for (int i = 0; i < spielsteine.Length; i++)
        {
            spielsteine[i].transform.position = _steinposition[i];
            spielsteine[i].transform.rotation = _steinrotation[i];
        }
        gameObject.SetActive(false);
        würfelzählen.SetActive(true);
        
    }

    bool provePattern(int[,,] muster){
        int[,,] field = new int[boardSize.x, boardSize.y, boardSize.z];

        for (int i = 0; i < boardSize.x; i++)
        {
            for (int j = 0; j < boardSize.y; j++)
            {
                for (int k = 0; k < boardSize.z; k++)
                {
                    field[i, j, k] = 0;
                }
            }
        }
        
        Vector3 spielfeldPosition = spielfeld.transform.position;
        Vector3 spielfeldScale = spielfeld.transform.localScale;

        float cellBreite = spielfeldScale.x / boardSize.x;
        float cellHöhe = spielfeldScale.y / boardSize.y;
        float cellTiefe = spielfeldScale.z / boardSize.z;

        foreach (GameObject stein in spielsteinCubes)
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

    bool proveAllPatterns()
    {
        foreach (var muster in musterListe){
            if (provePattern(muster)){
                return true;
            }
        }
        return false;
    }
}
