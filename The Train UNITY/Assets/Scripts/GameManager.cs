using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float train_hp;
    public float max_train_hp;
    public Slider hp_slider;

    void Start()
    {

    }

    void Update()
    {
        hp_slider.value = train_hp / max_train_hp;
        if (train_hp <= 0)
        {
            //Lost game
        }
    }
}
