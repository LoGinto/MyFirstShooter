using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ChoicedDialogue
{
    public class DialogueWithChoices : MonoBehaviour
    {
        //TO DO: after creating enemy we can be hostile
        //Little Trading and rewards
        //Add an actual sentences
        [Header("Choice stuff")]
        public GameObject textBox;
        public GameObject choice1;
        public GameObject choice2;
        public GameObject choice3;
        public GameObject exitChoice;
        public int choiceMade;
        [Space(5)]
        [Header("Canvas stuff")]
        public Text textE;
        public Canvas dialogueCanvas;
        //*****************************************************************//
        //*****************************************************************//
        [Space(10)]
        [Header("Canvas To Disable")]
        public CanvasGroup healthCanvas;
        public CanvasGroup bulletCanvas;
        public CanvasGroup inventoryCanvas; // I could do it with finding tags(and I tried) but it was time consuming to code
        public CanvasGroup crosshairCanvas;
        public CanvasGroup needCanvas;
        bool isTalking = false;
        public void ChoiceOption1()
        {
            textBox.GetComponent<Text>().text = "Choice number 1";
            choiceMade = 1;
        }
        public void ChoiceOption2()
        {
            textBox.GetComponent<Text>().text = "Choice number 2";
            choiceMade = 2;
        }
        public void ChoiceOption3()
        {
            textBox.GetComponent<Text>().text = "Choice number 3";
            choiceMade = 3;
        }
        public void ChoiceExit()
        {
            HideDialogue();
            ShowCanvas();
            isTalking = false;
        }

        private void Update()
        {
            if (isTalking == true)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }


            if (choiceMade == 1)
            {
                choice1.GetComponent<Text>().text = "Again choice 1 ";
            }
            if (choiceMade == 2)
            {
                //choice2.GetComponent<Text>().text = "Again choice 1";
                ClickChanger(choice2, "continue second path");

            }
            if (choiceMade == 3)
            {
                choice3.GetComponent<Text>().text = "Again choice 3 ";
            }
        }

        private void OnTriggerStay(Collider other)
        {
            textE.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                isTalking = true;
                textE.gameObject.SetActive(false);
                HideCanvas();
                ShowDialogue();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            textE.gameObject.SetActive(false);
        }

        private void ShowDialogue()
        {
            dialogueCanvas.GetComponent<CanvasGroup>().alpha = 1f;
            dialogueCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;

        }

        public void HideCanvas()
        {

            healthCanvas.alpha = 0f;
            healthCanvas.blocksRaycasts = false;
            bulletCanvas.alpha = 0f;
            bulletCanvas.blocksRaycasts = false;
            inventoryCanvas.alpha = 0f;
            inventoryCanvas.blocksRaycasts = false;
            crosshairCanvas.alpha = 0f;
            crosshairCanvas.blocksRaycasts = false;//or true 
            needCanvas.alpha = 0f;
            needCanvas.blocksRaycasts = false;
            
        }
        public void ShowCanvas()
        {
            healthCanvas.alpha = 1f;
            healthCanvas.blocksRaycasts = true;
            bulletCanvas.alpha = 1f;
            bulletCanvas.blocksRaycasts = true;
            inventoryCanvas.alpha = 1f;
            inventoryCanvas.blocksRaycasts = true;
            crosshairCanvas.alpha = 1f;
            crosshairCanvas.blocksRaycasts = true;//or true 
            needCanvas.alpha = 1f;
            needCanvas.blocksRaycasts = true;

        }
        private void HideDialogue()
        {
            dialogueCanvas.GetComponent<CanvasGroup>().alpha = 0f;
            dialogueCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;

        }
        private void ClickChanger(GameObject buttonGameObject, string txt)
        {
            //print a new text on button and text box(replace buttons)
            buttonGameObject.GetComponent<Text>().text = txt;
            buttonGameObject.GetComponent<Button>().onClick.AddListener(ChangeTextForChoice2);
        }
        void ChangeTextForChoice2()
        {
            SetText("Yay continuation of dialogue, I have won");
        }

        void SetText(string txt)
        {
            textBox.GetComponent<Text>().text = txt;
        }


    }
}
