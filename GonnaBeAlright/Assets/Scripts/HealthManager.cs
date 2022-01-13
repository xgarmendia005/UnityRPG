using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;

    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public bool ModifyHealth (int change)
    {
        currentHealth += change;
        print(transform.name + ": " + currentHealth);

        if (currentHealth <= 0)
        {
            dead = true;
            return true;
        }

        return false;
    }
}
