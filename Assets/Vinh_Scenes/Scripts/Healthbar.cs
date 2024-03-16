using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    // Start is called before the first frame update
    public Image _healthbar;
    public void updateHealthbar(float currentHealth, float maxHealth)
    {
        _healthbar.fillAmount = currentHealth/maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
