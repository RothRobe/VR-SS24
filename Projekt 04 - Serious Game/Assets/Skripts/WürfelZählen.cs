using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class WürfelZählen : MonoBehaviour
{

    int anzahl = 1119;
    int answer;

    public TextMeshProUGUI textMeshPro;
    public GameObject nextButton;
    //public GameObject würfel;
    public Spielstand spielstand;
    public GameObject BauMeister;

    private bool _gameActive = true;

    public void OnButtonClick(string input)
    {
        if (!_gameActive) return;
        switch(input){
            case "Button 0":
                textMeshPro.text += "0";
                break;
            case "Button 1":
                textMeshPro.text += "1";
                break;
            case "Button 2":
                textMeshPro.text += "2";
                break;
            case "Button 3":
                textMeshPro.text += "3";
                break;
            case "Button 4":
                textMeshPro.text += "4";
                break;
            case "Button 5":
                textMeshPro.text += "5";
                break;
            case "Button 6":
                textMeshPro.text += "6";
                break;
            case "Button 7":
                textMeshPro.text += "7";
                break;
            case "Button 8":
                textMeshPro.text += "8";
                break;
            case "Button 9":
                textMeshPro.text += "9";
                break;
            case "Button accept":
                ButtonAccept();
                break;
            case "Button delete":
                textMeshPro.text = "";
                break;
            /*case "Button Next":
                würfel.gameObject.SetActive(false);
                break;*/
        }
    }

    void ButtonAccept(){
        answer = int.Parse(textMeshPro.text);
        if(answer == anzahl){
            textMeshPro.text += "\nHerzlichen Glückwunsch! Die Antwort ist richtig! Du erhälst einen Punkt.";
            spielstand.incrementPunkteStand();
        }else{
            textMeshPro.text += "\nDie Antwort ist leider falsch. Dafür gibt es keinen Punkt :(";
        }

        _gameActive = false;
        nextButton.gameObject.SetActive(true);
    }
    

    public void ActivateNextGame(){
        gameObject.SetActive(false);
        BauMeister.SetActive(true);
    }
}
