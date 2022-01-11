using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public int[] sides;

    //Array of interlocutors names
    public string[] names;

    //Array of dialogue sentences
    public string[] sentences;

    public Dialogue (int size)
    {
        sides = new int[size];
        names = new string[size];
        sentences = new string[size];
    }
}
