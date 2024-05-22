using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class SpeechTree : MonoBehaviour
{
    [Serializable]
    public class DialogItem
    {
        [Multiline(5)]
        public string Text;

        public Sprite Sprite;
    }

    [SerializeField]
    private DialogItem[] Items;

    [SerializeField]
    private TextMeshProUGUI Text;

    [SerializeField]
    private Image Image;

    private int itemindex = -1; //Index wird erst erhöht und dann genutzt, da wir bei 0 starten wollen, müssen wir -1 angeben

    private void Awake(){
        SpeakNextItem();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            SpeakNextItem();
        }
    }

    private void SpeakNextItem(){
        itemindex = ++ itemindex % Items.Length;

        Speak(Items[itemindex]);
    }

    private void Speak(DialogItem item){
        if (item.Sprite){
            Image.sprite = item.Sprite;
        }
        TypewriterEffect.Start(Text, item.Text);
    }
}