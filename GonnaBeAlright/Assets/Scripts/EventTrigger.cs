using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public LevelManager levelManager;

    public int id;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(levelManager.NarrativeEvent(id));
    }
}
