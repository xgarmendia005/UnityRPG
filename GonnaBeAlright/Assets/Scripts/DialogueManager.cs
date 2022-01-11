using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    Dialogue dialogues;

    public TextAsset textFile;
    private string[] sentences;
    private string[] currentSentence;
    
    private Text nameDisplay;
    private Text textDisplay;
    public int index;
    public float typingSpeed;

    public GameObject dialogueScroll;
    public GameObject dialogueContainer;
    private RectTransform containerTrans;
    public GameObject dialogueBoxPrefab;
    private GameObject dialogueBox;
    private ScrollRect scroll;

    private void Start()
    {
        containerTrans = dialogueContainer.GetComponent<RectTransform>();
        scroll = dialogueScroll.GetComponent<ScrollRect>();
        containerTrans.sizeDelta = new Vector2(containerTrans.sizeDelta.x, containerTrans.sizeDelta.y + 200);

        LoadDialogues();

        StartDialogue();
    }

    private void LoadDialogues ()
    {
        sentences = textFile.text.Split('\n');
        dialogues = new Dialogue(sentences.Length);

        for (int i = 0; i < sentences.Length; i++)
        {
            currentSentence = sentences[i].Split('/');
            dialogues.sides[i] = int.Parse(currentSentence[0]);
            dialogues.names[i] = currentSentence[1];
            dialogues.sentences[i] = currentSentence[2];

       }
    }

    public void StartDialogue ()
    {
        dialogueBox = Instantiate(dialogueBoxPrefab);
        dialogueBox.transform.SetParent(dialogueContainer.transform);

        nameDisplay = dialogueBox.transform.GetChild(0).GetComponent<Text>();
        textDisplay = dialogueBox.transform.GetChild(1).GetComponent<Text>();


        scroll.verticalNormalizedPosition = 0;

        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        
        if (dialogues.sides[index] == 0)
        {
            dialogueBox.GetComponent<RectTransform>().localPosition = new Vector2(-50, -containerTrans.sizeDelta.y / 2 + 50);
        }
        else
        {
            dialogueBox.GetComponent<RectTransform>().localPosition = new Vector2(50, -containerTrans.sizeDelta.y / 2 + 50);
        }


        nameDisplay.text = dialogues.names[index];

        foreach (char letter in dialogues.sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence ()
    {
        index++;
        if (index == dialogues.sentences.Length)
        {
            EndDialogue();
            return;
        }
        StartCoroutine(Type());
    }

    public void HideDialogue ()
    {
        if (textDisplay.text == currentSentence[2])
        {
        } 
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && textDisplay.text == dialogues.sentences[index])
        {
            containerTrans.sizeDelta = new Vector2(containerTrans.sizeDelta.x, containerTrans.sizeDelta.y + 200);

            dialogueBox = Instantiate(dialogueBoxPrefab);
            dialogueBox.transform.SetParent(dialogueContainer.transform);
            dialogueBox.GetComponent<RectTransform>().localPosition = new Vector2(0, -containerTrans.sizeDelta.y / 2 + 50);

            nameDisplay = dialogueBox.transform.GetChild(0).GetComponent<Text>();
            textDisplay = dialogueBox.transform.GetChild(1).GetComponent<Text>();

            scroll.verticalNormalizedPosition = 0;

            NextSentence();
        }
    }

    private void EndDialogue ()
    {
        dialogueScroll.SetActive(false);
    }

}
