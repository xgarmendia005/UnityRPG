using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //Saves if game is currently in dialogue
    public bool isDialogue = false;
    //Saves if dialogue is paused
    public bool dialoguePaused = false;

    //Dialogue that is going to appear on screen
    private Dialogue dialogues;
    //Text file from which the dialogue is loaded
    public TextAsset textFile;
    //All the sentences of the dialogue
    private string[] sentences;
    //Current sentence containing position, name and text
    private string[] currentSentence;
  
    //Current dialogue index
    public int sentenceNum;
    //Speed of dialogue's letters
    public float typingSpeed;

    //UI
    private Text nameDisplay;
    private Text textDisplay;
    public GameObject dialogueScroll;
    public GameObject dialogueContainer;
    private RectTransform containerTrans;
    public GameObject dialogueBoxPrefab;
    private GameObject dialogueBox;
    private ScrollRect scroll;
    public GameObject portraitCover0, portraitCover1;

    private void Start()
    {
        //Get UI components
        containerTrans = dialogueContainer.GetComponent<RectTransform>();
        scroll = dialogueScroll.GetComponent<ScrollRect>();
        containerTrans.sizeDelta = new Vector2(containerTrans.sizeDelta.x, containerTrans.sizeDelta.y + 200);

        //Load dialogues from text file
        LoadDialogues();

        //Start loaded dialogue
        StartDialogue();
    }

    //Load dialogues from text file
    private void LoadDialogues ()
    {
        //Split dialogue in sentences using line feed
        sentences = textFile.text.Split('\n');
        dialogues = new Dialogue(sentences.Length);

        //Get necessary info from each sentence
        for (int i = 0; i < sentences.Length; i++)
        {
            currentSentence = sentences[i].Split('/');
            dialogues.sides[i] = int.Parse(currentSentence[0]);
            dialogues.names[i] = currentSentence[1];
            dialogues.sentences[i] = currentSentence[2];

       }
    }

    //Begin current dialogue
    public void StartDialogue ()
    {
        //Set game in dialogue
        isDialogue = true;
        dialoguePaused = false;

        //Instantiate a dialogue box in the scroll
        dialogueBox = Instantiate(dialogueBoxPrefab);
        dialogueBox.transform.SetParent(dialogueContainer.transform);

        //Get UI elements to store name and sentence
        nameDisplay = dialogueBox.transform.GetChild(0).GetComponent<Text>();
        textDisplay = dialogueBox.transform.GetChild(1).GetComponent<Text>();
        
        //Set scroll to origin
        scroll.verticalNormalizedPosition = 0;

        //Start typing dialogue
        StartCoroutine(Type());
    }

    //Type current sentence
    IEnumerator Type()
    {
        //Set portraits to black
        portraitCover0.SetActive(true);
        portraitCover1.SetActive(true);

        //Change dialogue box position and portrait's visibility depending on interlocutor's position
        if (dialogues.sides[sentenceNum] == 0)
        {
            portraitCover0.SetActive(false);
            dialogueBox.GetComponent<RectTransform>().localPosition = new Vector2(-50, -containerTrans.sizeDelta.y / 2 + 50);
        }
        else
        {
            portraitCover1.SetActive(false);
            dialogueBox.GetComponent<RectTransform>().localPosition = new Vector2(50, -containerTrans.sizeDelta.y / 2 + 50);
        }

        //Display interlocutor's name
        nameDisplay.text = dialogues.names[sentenceNum];

        //Display sentence's letters one by one
        foreach (char letter in dialogues.sentences[sentenceNum].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    //Start next sentence
    public void NextSentence ()
    {
        if (textDisplay.text == dialogues.sentences[sentenceNum])
        {
            //Create new dialogue box and expand scroll
            containerTrans.sizeDelta = new Vector2(containerTrans.sizeDelta.x, containerTrans.sizeDelta.y + 200);
            dialogueBox = Instantiate(dialogueBoxPrefab);
            dialogueBox.transform.SetParent(dialogueContainer.transform);
            dialogueBox.GetComponent<RectTransform>().localPosition = new Vector2(0, -containerTrans.sizeDelta.y / 2 + 50);

            //Get UI elements to store name and sentence
            nameDisplay = dialogueBox.transform.GetChild(0).GetComponent<Text>();
            textDisplay = dialogueBox.transform.GetChild(1).GetComponent<Text>();

            //Set scroll to origin
            scroll.verticalNormalizedPosition = 0;

            sentenceNum++;
            //If no more sentences, end dialogue
            if (sentenceNum == dialogues.sentences.Length)
            {
                EndDialogue();
                return;
            }
            //Type next sentence
            StartCoroutine(Type());
        }
    }

    //Hide dialogue UI
    public void HideDialogue ()
    {
        if (textDisplay.text == dialogues.sentences[sentenceNum])
        {
            dialogueScroll.transform.parent.gameObject.SetActive(false);
            dialoguePaused = true;
        } 
    }

    public void ResumeDialogue ()
    {
        dialogueScroll.transform.parent.gameObject.SetActive(true);
        dialoguePaused = false;
        NextSentence();
    }

    //Finish dialogue
    private void EndDialogue ()
    {
        dialogueScroll.transform.parent.gameObject.SetActive(false);
        isDialogue = false;
    }

}
