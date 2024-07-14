using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WürfelZählen : MonoBehaviour
{

    int anzahl = 1119;
    int answer;

    public TextMeshProUGUI textMeshPro;
    public Button nextButton;
    public GameObject würfel;
    public Spielstand spielstand;

    public void OnButtonClick(Button clickedButton){
        switch(clickedButton.name){
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
                buttonAccept();
                break;
            case "Button delete":
                textMeshPro.text = "";
                break;
            case "Button Next":
                würfel.gameObject.SetActive(false);
                break;
        }
    }

    public void buttonAccept(){
        answer = int.Parse(textMeshPro.text);
        if(answer == anzahl){
            textMeshPro.text += "\nHerzlichen Glückwunsch! Die Antwort ist richtig! Du erhälst einen Punkt.";
            spielstand.incrementPunkteStand();
        }else{
            textMeshPro.text += "\nDie Antwort ist leider falsch. Dafür gibt es keinen Punkt :(";
        }
        DeactivateAllButtons();
        ActivateNext();
    }

    void DeactivateAllButtons() {
        // Finde alle Buttons im Spielobjekt
        Button[] buttons = GetComponentsInChildren<Button>();

        // Gehe durch alle Buttons und deaktiviere ihre Interaktivität
        foreach(Button button in buttons) {
            button.interactable = false;
        }
    }

    void ActivateNext(){
        nextButton.gameObject.SetActive(true);
    }
}
