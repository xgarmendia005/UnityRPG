using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGBattle : MonoBehaviour
{
    public GameObject[] characters;
    private int curChar = 0;
    private GameObject tempChar;
    private List<int> orderIndex;
    private List<int> playerIndex;
    private List<int> enemyIndex;

    private int curTarget;

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
                battleState = TARGETSEL;
                break;
            case TARGETSEL:
                if (tempChar.GetComponent <Stats>().player){
                    if (Input.GetKeyDown(KeyCode.Alpha1))
                    {
                        curTarget = enemyIndex[0];
                        if (characters[curTarget].GetComponent<HealthManager>().ModifyHealth(-15))
                        {
                            KillCharacter(curTarget);
                        }
                        tempChar.GetComponent<Renderer>().material.SetFloat("_OutlineAlpha", 0f);
                        curChar++;
                        battleState = TURNSTART;
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha2))
                    {
                        curTarget = enemyIndex[1];
                        if (characters[curTarget].GetComponent<HealthManager>().ModifyHealth(-15))
                        {
                            KillCharacter(curTarget);
                        }
                        tempChar.GetComponent<Renderer>().material.SetFloat("_OutlineAlpha", 0f);
                        curChar++;
                        battleState = TURNSTART;
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha3))
                    {
                        curTarget = enemyIndex[2];
                        if (characters[curTarget].GetComponent<HealthManager>().ModifyHealth(-15))
                        {
                            KillCharacter(curTarget);
                        }
                        tempChar.GetComponent<Renderer>().material.SetFloat("_OutlineAlpha", 0f);
                        curChar++;
                        battleState = TURNSTART;
                    }
                    else if (Input.GetKeyDown(KeyCode.Alpha4))
                    {
                        curTarget = enemyIndex[3];
                        if (characters[curTarget].GetComponent<HealthManager>().ModifyHealth(-15))
                        {
                            KillCharacter(curTarget);
                        }
                        tempChar.GetComponent<Renderer>().material.SetFloat("_OutlineAlpha", 0f);
                        curChar++;
                        battleState = TURNSTART;
                    }
                    
                }
                else
                {
                    characters[playerIndex[Random.Range(0, playerIndex.Count)]].GetComponent<HealthManager>().ModifyHealth(-15);
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

    public void KillCharacter (int i)
    {
        if (orderIndex.IndexOf(curTarget) <= curChar) curChar--;
        orderIndex.Remove(curTarget);
        CalculateIndex();
        characters[curTarget].SetActive(false);
    }
}

