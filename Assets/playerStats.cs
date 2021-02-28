using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playerStats : MonoBehaviour
{
    public Slider healthBar;

    public void changeHealthUI(int decrease = 0)
    {
        if (decrease > healthBar.value)
        {
            healthBar.value = 0;
        }
        healthBar.value -= decrease;
    }

    void Start()
    {
        healthBar = GameObject.FindGameObjectWithTag("healthBar").GetComponent<Slider>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
