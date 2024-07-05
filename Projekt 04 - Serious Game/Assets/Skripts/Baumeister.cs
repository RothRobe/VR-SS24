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
    private bool isRight = false;

    public void OnButtonClick(Button clickedButton){
        if(clickedButton.name == "Button accept"){
            prove();
            buttonAccept();
        }
        if(clickedButton.name == "Button Next"){
            baumeister.gameObject.SetActive(false);
        }
    }

    public void buttonAccept(){
        if(isRight){
            textMeshPro.text += "\nHerzlichen Glückwunsch! Das sieht gut aus! Du erhälst einen Punkt.";
            //Gesamtpunktzahl erhöhen
        }else{
            textMeshPro.text += "\nIn deinem Bauwerk hat sich leider ein Fehler eingeschlichen. Dafür gibt es keinen Punkt :(";
        }
        ActivateNext();
    }

    void ActivateNext(){
        nextButton.gameObject.SetActive(true);
    }

    void prove(){
        //noch prüfen, ob die Lösung richtig ist
        isRight = true;
    }
}
