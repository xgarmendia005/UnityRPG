using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RPGBattle : MonoBehaviour
{
    public GameObject[] characters;
    private int curChar = 0;
    private GameObject tempChar;
    private List<int> orderIndex;
    private List<int> playerIndex;
    private List<int> enemyIndex;

    private int curTarget;
    public GameObject targetSelector;

    private const int BATTLESTART = 0;
    private const int TURNSTART = 1;
    private const int TARGETSEL = 2;

    public int battleState = BATTLESTART;

    void Start()
    {
        orderIndex = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
        playerIndex = new List<int> { 0, 1, 2, 3 };
        enemyIndex = new List<int> { 4, 5, 6, 7 };
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            characters[0].GetComponent<HealthManager>().ModifyHealth(-15);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            characters[1].GetComponent<HealthManager>().ModifyHealth(-15);
        }
        
        switch(battleState)
        {
            case BATTLESTART:
                battleState = TURNSTART;
                break;
            case TURNSTART:
                tempChar = characters[orderIndex[curChar]];
                tempChar.GetComponent<Renderer>().material.SetFloat("_OutlineAlpha", 1f);
                HideTargetSelector();
                if (tempChar.GetComponent<Stats>().player) CreateTargetSelector();
                battleState = TARGETSEL;
                break;
            case TARGETSEL:
                if (!tempChar.GetComponent <Stats>().player){
                    
                    characters[playerIndex[Random.Range(0, playerIndex.Count)]].GetComponent<HealthManager>().ModifyHealth(-15f);
                    tempChar.GetComponent<Renderer>().material.SetFloat("_OutlineAlpha", 0f);
                    curChar++;
                    battleState = TURNSTART;
                }
                if (curChar >= orderIndex.Count) curChar = 0;
                break;
        }
    }

    public void CalculateIndex()
    {
        playerIndex = new List<int>();
        enemyIndex = new List<int>();
        for (int i = 0; i < characters.Length; i++)
        {
            if (!characters[i].GetComponent<HealthManager>().dead){
                if (characters[i].GetComponent<Stats>().player) playerIndex.Add(i);
                else enemyIndex.Add(i);
            }
        }
    }

    public void KillCharacter(int i)
    {
        if (orderIndex.IndexOf(curTarget) <= curChar) curChar--;
        orderIndex.Remove(curTarget);
        CalculateIndex();
        characters[curTarget].SetActive(false);
    }

    public void CreateTargetSelector()
    {
        for (int i = 0; i < enemyIndex.Count; i++)
        {
            Transform tempButton = targetSelector.transform.GetChild(i);
            tempButton.GetChild(0).GetComponent<Text>().text = characters[enemyIndex[i]].name;
            tempButton.gameObject.SetActive(true);
        }
        targetSelector.SetActive(true);
        EventSystem.current.SetSelectedGameObject(targetSelector.transform.GetChild(0).gameObject);
    }

    public void HideTargetSelector ()
    {
        for (int i = 0; i < 4; i++)
        {
            targetSelector.transform.GetChild(i).gameObject.SetActive(false);
        }
        EventSystem.current.SetSelectedGameObject(null);
        targetSelector.SetActive(false);
    }

    public void HighlightTarget (int id, bool b)
    {
        if (b)
        {
            characters[enemyIndex[id]].GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.red);
            characters[enemyIndex[id]].GetComponent<Renderer>().material.SetFloat("_OutlineAlpha", 1f);
        }
        else
        {
            characters[enemyIndex[id]].GetComponent<Renderer>().material.SetFloat("_OutlineAlpha", 0f);
        }
    }

    public void TargetButton(int id)
    {
        curTarget = enemyIndex[id];
        if (characters[curTarget].GetComponent<HealthManager>().ModifyHealth(-15f))
        {
            KillCharacter(curTarget);
        }
        characters[curTarget].GetComponent<Renderer>().material.SetFloat("_OutlineAlpha", 0f);
        tempChar.GetComponent<Renderer>().material.SetFloat("_OutlineAlpha", 0f);
        curChar++;
        battleState = TURNSTART;
    }
}

