using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public float maxHealth;
    private float currentHealth;

    public GameObject healthBar;
    public Image healthBarImage;

    public bool dead = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    public bool ModifyHealth (float change)
    {
        currentHealth += change;
        healthBarImage.fillAmount = Mathf.Clamp(currentHealth / maxHealth, 0, 1f);

        if (currentHealth <= 0)
        {
            dead = true;
            healthBar.SetActive(false);
            return true;
        }

        return false;
    }
}
