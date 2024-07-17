using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spielstand : MonoBehaviour
{
    int punkteStand = 0;
    private int maxPunkte = 2;
    //List<string> bestenListe;

    public void incrementPunkteStand(){
        punkteStand++;
    }

    public int GetPunkteStand()
    {
        return punkteStand;
    }

    public void ResetPunktestand()
    {
        punkteStand = 0;
    }

    public int GetMaxPunkte()
    {
        return maxPunkte;
    }
}
