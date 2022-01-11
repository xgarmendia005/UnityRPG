using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction : MonoBehaviour
{

    bool inDialogue = true;
    public DialogueManager dialogueManager;

    public string[] characters;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && inDialogue)
        {
            //Healer gets to door
            if (dialogueManager.index == 23)
            {
                dialogueManager.HideDialogue();
                inDialogue = false;
            }
            //Healer leaves house and returns
            else if(dialogueManager.index == 32)
            {
                dialogueManager.HideDialogue();
                inDialogue = false;
            }
            //Scream outside
            else if (dialogueManager.index == 36)
            {
                dialogueManager.HideDialogue();
                inDialogue = false;
            }
            //Hero leaves house
            else if (dialogueManager.index == 38)
            {
                dialogueManager.HideDialogue();
                inDialogue = false;
            }
            //Healer leaves house
            else if (dialogueManager.index == 39)
            {
                dialogueManager.HideDialogue();
                inDialogue = false;
            }
            //Healer approaches hero and woman
            else if (dialogueManager.index == 45)
            {
                dialogueManager.HideDialogue();
                inDialogue = false;
            }
            //Glitch appears
            else if (dialogueManager.index == 47)
            {
                dialogueManager.HideDialogue();
                inDialogue = false;
            }
            //Woman disappears
            else if (dialogueManager.index == 51)
            {
                dialogueManager.HideDialogue();
                inDialogue = false;
            }
            //Hero starts to go away
            else if (dialogueManager.index == 61)
            {
                dialogueManager.HideDialogue();
                inDialogue = false;
            }
            //Short scene: Glitch takes hero, healer runs without thinking
            else if (dialogueManager.index == 62)
            {
                dialogueManager.HideDialogue();
                inDialogue = false;
            }
            //Hero disappears. Dialogue box falls to the ground
            else if (dialogueManager.index == 63)
            {
                dialogueManager.HideDialogue();
                inDialogue = false;
            }
            //Healer takes Hero text from dialogue box and put it in hers
            else if (dialogueManager.index == 64)
            {
                dialogueManager.HideDialogue();
                inDialogue = false;
            }
            //Hero changes
            else if (dialogueManager.index == 65)
            {
                dialogueManager.HideDialogue();
                inDialogue = false;
            }
            else
            {
                dialogueManager.NextSentence();
            }
        }

        if (Input.GetMouseButtonDown(1) && !inDialogue)
        {
            inDialogue = true;
            dialogueManager.NextSentence();
        }

    }
}
