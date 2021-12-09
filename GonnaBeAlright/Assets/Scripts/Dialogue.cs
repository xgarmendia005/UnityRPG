using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextAsset textFile;
    public int currentSide;
    private string[] sentences;
    private string[] currentSentence;
    
    private Text nameDisplay;
    private Text textDisplay;
    public int index;
    public float typingSpeed;

    public GameObject dialogueContainer;
    private RectTransform containerTrans;
    public GameObject dialogueBoxPrefab;
    private GameObject dialogueBox;
    private ScrollRect scroll;

    private void Start()
    {
        sentences = textFile.text.Split('\n');

 

        containerTrans = dialogueContainer.GetComponent<RectTransform>();
        scroll = GetComponent<ScrollRect>();
        containerTrans.sizeDelta = new Vector2(containerTrans.sizeDelta.x, containerTrans.sizeDelta.y + 200);

        dialogueBox = Instantiate(dialogueBoxPrefab);
        dialogueBox.transform.SetParent(dialogueContainer.transform);
       
        nameDisplay = dialogueBox.transform.GetChild(0).GetComponent<Text>();
        textDisplay = dialogueBox.transform.GetChild(1).GetComponent<Text>();


        scroll.verticalNormalizedPosition = 0;

        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        currentSentence = sentences[index].Split('/');

        currentSide = int.Parse(currentSentence[0]);
        if (currentSide == 0)
        {
            dialogueBox.GetComponent<RectTransform>().localPosition = new Vector2(-50, -containerTrans.sizeDelta.y / 2 + 50);
        }
        else
        {
            dialogueBox.GetComponent<RectTransform>().localPosition = new Vector2(50, -containerTrans.sizeDelta.y / 2 + 50);
        }
        

        nameDisplay.text = currentSentence[1];

        foreach (char letter in currentSentence[2].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence ()
    {
            index++;
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
        if (Input.GetMouseButtonDown(0) && textDisplay.text == currentSentence[2])
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

}
