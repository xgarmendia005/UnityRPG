using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Introduction : LevelManager
{
    public DialogueManager dialogueManager;

    [Header("Triggers")]
    public GameObject eventTrigger0;
    public GameObject eventTrigger1;
    public GameObject eventTrigger2;

    [Header("Characters")]
    public Transform healer;
    public Transform originalHero;

    [Header("Props")]
    public GameObject door;

    private void Update()
    {
        //If game is in dialogue and left mouse button is pressed, type next sentence
        if (Input.GetMouseButtonDown(0) && dialogueManager.isDialogue && !dialogueManager.dialoguePaused)
        {
            //Silence
            if (dialogueManager.sentenceNum == 18)
            {
                dialogueManager.HideDialogue();
                StartCoroutine(NarrativeEvent(-1));
            }
            //Silence
            else if (dialogueManager.sentenceNum == 22)
            {
                dialogueManager.HideDialogue();
                StartCoroutine(NarrativeEvent(-1));
            }
            //Healer goes to the door
            else if (dialogueManager.sentenceNum == 32)
            {
                dialogueManager.HideDialogue();
                Flip(healer);
                healer.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.5f, 0f);
            }
            //Yelling outside of the house
            else if (dialogueManager.sentenceNum == 36)
            {
                dialogueManager.HideDialogue();
                StartCoroutine(NarrativeEvent(-1));
            }
            //Original hero goes to the door
            else if (dialogueManager.sentenceNum == 38)
            {
                dialogueManager.HideDialogue();
                originalHero.GetComponent<Rigidbody2D>().velocity = new Vector2(-3f, 0f);
            }
            else if (dialogueManager.sentenceNum == 39)
            {
                dialogueManager.HideDialogue();
                Flip(healer);
                healer.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.5f, 0f);
            }
            else dialogueManager.NextSentence();
        }
    }

    public override IEnumerator NarrativeEvent(int id)
    {
        switch (id)
        {
            //Door is opened and healer disappears
            //After some time, healer appears and goes back to starting position
            case 0:
                door.SetActive(true);
                healer.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                healer.gameObject.SetActive(false);

                yield return new WaitForSeconds(4);

                Flip(healer);
                healer.gameObject.SetActive(true);
                healer.GetComponent<Rigidbody2D>().velocity = new Vector2(1f, 0f);
                door.SetActive(false);

                eventTrigger0.SetActive(false);
                eventTrigger1.SetActive(true);
                break;
            //Healer stops and dialogue resumes after some time
            case 1:
                healer.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                yield return new WaitForSeconds(2);
                dialogueManager.ResumeDialogue();

                eventTrigger1.SetActive(false);
                eventTrigger2.SetActive(true);
                break;
            //Original hero goes outside
            case 2:
                door.SetActive(true);
                originalHero.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
                originalHero.gameObject.SetActive(false);

                dialogueManager.ResumeDialogue();

                eventTrigger2.SetActive(false);
                break;
            //Pause
            default:
                yield return new WaitForSeconds(3);
                dialogueManager.ResumeDialogue();
                break;
        }
    } 

    public void Flip (Transform target)
    {
        target.localScale = new Vector2(-target.localScale.x, target.localScale.y);
    }
    
}
