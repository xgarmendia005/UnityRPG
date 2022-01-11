using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public void ModifyHealth (int change)
    {
        currentHealth += change;
        print(transform.name + ": " + currentHealth);
    }
}
