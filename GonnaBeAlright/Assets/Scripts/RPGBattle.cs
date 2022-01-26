using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RPGBattle : MonoBehaviour
{
    //Array with all characters in battle (player party first, then enemies)
    public GameObject[] characters;
    //Current active character
    private int curChar = 0;
    private GameObject tempChar;
    //List with characters' order
    private List<int> orderIndex;
    //Lists that identify player characters and enemies in characters array
    private List<int> playerIndex;
    private List<int> enemyIndex;

    //Action selector UI
    public GameObject actionSelector;
    //Current action
    private RPGAbility curAction;

    //Ability selector UI
    public GameObject abilitySelector;
    //Temporal array to store current character's abilities
    private RPGAbility[] tempAbilities;

    //Current target index
    private int curTarget;
    //Target selector UI
    public GameObject targetSelector;
    //Target selector for targeting allies
    public GameObject playerTargetSelector;

    //Battle states' declaration
    private const int BATTLESTART = 0;
    private const int TURNSTART = 1;
    private const int ACTIONSEL = 2;
    private const int ABILITYSEL = 3;
    private const int TARGETSEL = 4;

    //Current battle state
    public int battleState = BATTLESTART;

    void Start()
    {
        //Initialize order, player and enemies lists
        orderIndex = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
        playerIndex = new List<int> { 0, 1, 2, 3 };
        enemyIndex = new List<int> { 4, 5, 6, 7 };
    }

    public void Update()
    {
        switch(battleState)
        {
            case BATTLESTART:
                //Start turn
                battleState = TURNSTART;
                break;
            case TURNSTART:
                //Assign current active character
                tempChar = characters[orderIndex[curChar]];
                //Enable active character's outline
                tempChar.GetComponent<Renderer>().material.SetFloat("_OutlineAlpha", 1f);
                //If the active character is a player show action selector
                if (tempChar.GetComponent<Stats>().player)
                {
                    actionSelector.SetActive(true);
                    //Set first button as selected
                    EventSystem.current.SetSelectedGameObject(actionSelector.transform.GetChild(0).gameObject);
                    battleState = ACTIONSEL;
                }
                //If it's an enemy, go to target selection
                else battleState = TARGETSEL;
                break;
            case ACTIONSEL:
                break;
            case TARGETSEL:
                //If active character is enemy
                if (!tempChar.GetComponent <Stats>().player){
                    //Attack random player character
                    characters[playerIndex[Random.Range(0, playerIndex.Count)]].GetComponent<HealthManager>().ModifyHealth(-15f);
                    //Disable active character's outline
                    tempChar.GetComponent<Renderer>().material.SetFloat("_OutlineAlpha", 0f);
                    //Go to next character's turn
                    curChar++;
                    battleState = TURNSTART;
                }
                //If all characters had a turn, restart from first character
                if (curChar >= orderIndex.Count) curChar = 0;
                break;
        }
    }

    //Create two list identifying players and enemies in characters array
    public void CalculateIndex()
    {
        playerIndex = new List<int>();
        enemyIndex = new List<int>();
        //For each element in characters array
        for (int i = 0; i < characters.Length; i++)
        {
            //If character is alive, distribute to corresponding list
            if (!characters[i].GetComponent<HealthManager>().dead){
                if (characters[i].GetComponent<Stats>().player) playerIndex.Add(i);
                else enemyIndex.Add(i);
            }
        }
    }

    //Manage character's death
    public void KillCharacter(int i)
    {
        //If dead character is previous to active character, reduce current character index to avoid omitting characters
        if (orderIndex.IndexOf(curTarget) <= curChar) curChar--;
        //Remove dead character from order list
        orderIndex.Remove(curTarget);
        //Calculate new lists identifying players' and enemies' positions
        CalculateIndex();
        //Disable dead character
        characters[curTarget].SetActive(false);
    }

    //Create target selector UI
    public void CreateTargetSelector()
    {
        //For each enemy in battle
        for (int i = 0; i < enemyIndex.Count; i++)
        {
            //Enable button with enemy's name
            Transform tempButton = targetSelector.transform.GetChild(i);
            tempButton.GetChild(0).GetComponent<Text>().text = characters[enemyIndex[i]].name;
            tempButton.gameObject.SetActive(true);
        }
        //Enable target selector UI
        targetSelector.SetActive(true);
        //Set first button as selected
        EventSystem.current.SetSelectedGameObject(targetSelector.transform.GetChild(0).gameObject);
    }

    //Create ally target selector UI
    public void CreatePlayerTargetSelector()
    {
        //For each aly in battle
        for (int i = 0; i < playerIndex.Count; i++)
        {
            //Enable button with player's name
            Transform tempButton = playerTargetSelector.transform.GetChild(i);
            tempButton.GetChild(0).GetComponent<Text>().text = characters[playerIndex[i]].name;
            tempButton.gameObject.SetActive(true);
        }
        //Enable target selector UI
        playerTargetSelector.SetActive(true);
        //Set first button as selected
        EventSystem.current.SetSelectedGameObject(playerTargetSelector.transform.GetChild(0).gameObject);
    }

    //Hide target selector UI
    public void HideTargetSelector ()
    {
        //For each button in target selector
        for (int i = 0; i < 4; i++)
        {
            //Disable button
            targetSelector.transform.GetChild(i).gameObject.SetActive(false);
            playerTargetSelector.transform.GetChild(i).gameObject.SetActive(false);
        }
        //Deselect current button
        EventSystem.current.SetSelectedGameObject(null);
        //Disable target selector UI
        targetSelector.SetActive(false);
        playerTargetSelector.SetActive(false);
    }


    //Create ability selector UI
    public void CreateAbilitySelector()
    {
        tempAbilities = tempChar.GetComponent<Stats>().abilities;

        //For each ability of active character
        for (int i = 0; i < tempAbilities.Length; i++)
        {
            //Enable button with ability's name
            Transform tempButton = abilitySelector.transform.GetChild(0).GetChild(i);
            tempButton.GetChild(0).GetComponent<Text>().text = tempAbilities[i].name;
            tempButton.gameObject.SetActive(true);
        }
        //Enable target selector UI
        abilitySelector.SetActive(true);
        //Set first button as selected
        EventSystem.current.SetSelectedGameObject(abilitySelector.transform.GetChild(0).GetChild(0).gameObject);
    }


    //Take the next step in selected action
    public void ActionButton(int action)
    {
        //0 -> Attack
        //1 -> Abilities
        //2 -> Items
        //3 -> Flee
        switch (action)
        {
            case 0:
                actionSelector.SetActive(false);
                curAction = new RPGAbility("Attack", 0, -tempChar.GetComponent<Stats>().attack, 0, 0);
                CreateTargetSelector();
                battleState = TARGETSEL;
                break;
            case 1:
                actionSelector.SetActive(false);
                CreateAbilitySelector();
                battleState = ABILITYSEL;
                break;

        }
    }

    //Register player's selected ability
    public void AbilityButton(int ability)
    {
        curAction = tempAbilities[ability];
        abilitySelector.SetActive(false);
        if (curAction.type == 0) CreateTargetSelector();
        else if (curAction.type == 1) CreatePlayerTargetSelector();
        battleState = TARGETSEL;
    }

    //Highlight current selected target
    public void HighlightTarget (int id, bool b)
    {
        //If boolean flag is true
        if (b)
        {
            //Enable red outline to current target if it's an enemy
            if (curAction.type == 0)
            {
                characters[enemyIndex[id]].GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.red);
                characters[enemyIndex[id]].GetComponent<Renderer>().material.SetFloat("_OutlineAlpha", 1f);
            }
            //Enable green outline to current target if its an ally
            else if (curAction.type == 1)
            {
                characters[playerIndex[id]].GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.green);
                characters[playerIndex[id]].GetComponent<Renderer>().material.SetFloat("_OutlineAlpha", 1f);
            }

            }
        //If boolean flag is false
        else
        {
            //Disable outline to current target
            if (curAction.type == 0) characters[enemyIndex[id]].GetComponent<Renderer>().material.SetFloat("_OutlineAlpha", 0f);
            else if (curAction.type == 1) characters[playerIndex[id]].GetComponent<Renderer>().material.SetFloat("_OutlineAlpha", 0f);
        }
    }

    //Take action regarding selected target
    public void TargetButton(int id)
    {
        HideTargetSelector();
        if (curAction.type == 0) curTarget = enemyIndex[id];
        else if (curAction.type == 1) curTarget = playerIndex[id];
        //Modify target's health and kill if necessary
        if (characters[curTarget].GetComponent<HealthManager>().ModifyHealth(curAction.quantity))
        {
            KillCharacter(curTarget);
        }
        //Disable target's and active character's outlines
        characters[curTarget].GetComponent<Renderer>().material.SetFloat("_OutlineAlpha", 0f);
        tempChar.GetComponent<Renderer>().material.SetFloat("_OutlineAlpha", 0f);
        //Go to next character's turn
        curChar++;
        battleState = TURNSTART;
    }
}

