using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update

    public Healthbar healthbar;
    public float currentHealth;
    public float maxHealth = 10;
    void Start()
    {
        currentHealth = maxHealth;
        healthbar.updateHealthbar(currentHealth, maxHealth);
    }

    // Update is called once per frame
    private void OnMouseDown()
    {
        currentHealth -= 2;
        healthbar.updateHealthbar(currentHealth, maxHealth);
        if(currentHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
