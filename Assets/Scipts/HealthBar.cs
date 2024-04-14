using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{       
    public Slider healthBarSlider;
    public Slider easeHealthBarSlider;
    public float health ;
    public float healthBar = 100f;
    private float lerpSpeed= 0.05f; 
    // Start is called before the first frame update
    void Start()
    {
        health = healthBar;
    }

    // Update is called once per frame
    void Update()
    {
        if(healthBarSlider.value != health){
            healthBarSlider.value = health;
        }

        if(Input.GetKeyDown(KeyCode.Space)){
                takeDamage(10); 
        }

        if(easeHealthBarSlider.value != healthBarSlider.value){
            easeHealthBarSlider.value = Mathf.Lerp(easeHealthBarSlider.value,health,lerpSpeed); 
        }
    }

    void takeDamage(float damage ){
        health-= damage; 
    } 
}
