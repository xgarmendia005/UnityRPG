using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGBattle : MonoBehaviour
{
    public GameObject[] characters;
    private int curChar = 0;
    private int[] enemyIndex;

    private const int BATTLESTART = 0;
    private const int TURNSTART = 1;
    private const int TARGETSEL = 2;

    public int battleState = BATTLESTART;

    void Start()
    {
        enemyIndex = new int[] { 4, 5, 6, 7 };
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
                characters[curChar].GetComponent<Renderer>().material.SetFloat("_OutlineAlpha", 1f);
                battleState = TARGETSEL;
                break;
            case TARGETSEL:
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    characters[enemyIndex[0]].GetComponent<HealthManager>().ModifyHealth(-15);
                    characters[curChar].GetComponent<Renderer>().material.SetFloat("_OutlineAlpha", 0f);
                    curChar++;
                    battleState = TURNSTART;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    characters[enemyIndex[1]].GetComponent<HealthManager>().ModifyHealth(-15);
                    characters[curChar].GetComponent<Renderer>().material.SetFloat("_OutlineAlpha", 0f);
                    curChar++;
                    battleState = TURNSTART;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    characters[enemyIndex[2]].GetComponent<HealthManager>().ModifyHealth(-15);
                    characters[curChar].GetComponent<Renderer>().material.SetFloat("_OutlineAlpha", 0f);
                    curChar++;
                    battleState = TURNSTART;
                }
                else if (Input.GetKeyDown(KeyCode.Alpha4))
                {
                    characters[enemyIndex[3]].GetComponent<HealthManager>().ModifyHealth(-15);
                    characters[curChar].GetComponent<Renderer>().material.SetFloat("_OutlineAlpha", 0f);
                    curChar++;
                    battleState = TURNSTART;
                }
                break;
        }
    }
}
