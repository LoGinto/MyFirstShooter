using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Dialogue : MonoBehaviour
{
    //casual dialogue
    [SerializeField] Text dialogueTextUI;
    [SerializeField] Text pressEtext;
    [SerializeField] string[] sentences;
    [SerializeField] string[] annoyedSentences; 
    [SerializeField] float timeBetweenSentences = 1.5f;
    private int timesTalk = 0;
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            pressEtext.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (timesTalk == 0)
                {
                    StartDialogue();
                }
                else
                {
                    pressEtext.gameObject.SetActive(false);
                    dialogueTextUI.gameObject.SetActive(true);
                    StartCoroutine("AnnoyedDialogue");
                }
            }

        }
    }

    private void StartDialogue()
    {
        pressEtext.gameObject.SetActive(false);
        dialogueTextUI.gameObject.SetActive(true);
        StartCoroutine("DialogueSwitch");
    }

    private void OnTriggerExit(Collider other)
    {
        pressEtext.gameObject.SetActive(false);
    }
    private void Update()
    {
        
    }
    IEnumerator DialogueSwitch()
    {
        
        for(int i = 0;i<=sentences.Length;i++)
        {
            yield return new WaitForSeconds(timeBetweenSentences);
            dialogueTextUI.text = sentences[i].ToString();
            //should work
            if (i == sentences.Length-1)
            {
                
                timesTalk += 1;
                break;
            }
        }
        dialogueTextUI.gameObject.SetActive(false);
    }
    IEnumerator AnnoyedDialogue()
    {
        for (int i = 0; i <= annoyedSentences.Length; i++)
        {
            yield return new WaitForSeconds(timeBetweenSentences);
            dialogueTextUI.text = annoyedSentences[i].ToString();
            if (i == annoyedSentences.Length - 1)
            {
                break;
            }

        }
        dialogueTextUI.gameObject.SetActive(false);
    }
    }
