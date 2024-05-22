using TMPro;
using UnityEngine;

    public class TreeDialog : MonoBehaviour
    {
        public TextMeshProUGUI tmp;
        public GameObject treeSpawner;
        public GameObject score;
        public GameObject bird;
        private int index = -1;
        private bool disabled = false;
        private string[] textItems = new[]
        {
            "Ich brauche deine Hilfe. Ich bin der letzte der sprechenden Bäume.",
            "Die böse Maus hat mir meine ganzen Samen geklaut. Nun werden die sprechenden Bäume mit mir aussterben.",
            "Die Samen finden sich immer an den größten Bäumen.",
            "Kannst du zu diesen Bäumen fliegen bevor sie sterben und mir 10 Samen bringen?",
            "Die Zeit läuft davon. Bitte beeil dich!"
        };

        public void NextDialog()
        {
            if (!disabled)
            {
                index++;
                if (index >= textItems.Length)
                {
                    disabled = true;
                    tmp.text = "";
                    treeSpawner.SetActive(true);
                    score.SetActive(true);
                    bird.GetComponent<BirdMovementVR>().enabled = true;
                    return;
                }

                tmp.text = textItems[index];
            }
        }
    }