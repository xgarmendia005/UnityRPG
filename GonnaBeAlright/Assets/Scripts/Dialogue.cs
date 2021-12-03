using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public TextAsset textFile;
    public int currentSpeaker;
    private string[] sentences;
    private string[] currentSentence;

    public GameObject dialogueBox;
    public Text nameDisplay;
    public Text textDisplay;
    public int index;
    public float typingSpeed;

    private void Start()
    {
        sentences = textFile.text.Split('\n');
        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
        currentSentence = sentences[index].Split('-');

        currentSpeaker = int.Parse(currentSentence[0]);

        nameDisplay.text = currentSentence[1];

        foreach (char letter in currentSentence[2].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence ()
    {
        if (textDisplay.text == currentSentence[2])
        {
            if (index < sentences.Length - 1)
            {
                index++;
                textDisplay.text = "";
                StartCoroutine(Type());
            }
            else
            {
                textDisplay.text = "";
            }
        }
    }

    public void HideDialogue ()
    {
        if (textDisplay.text == currentSentence[2])
        {
            dialogueBox.SetActive(false);
        } 
    }

    public void ShowDialogue ()
    {
        NextSentence();
        dialogueBox.SetActive(true);
    }
}
