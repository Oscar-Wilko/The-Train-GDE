using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    public GameObject prompt;
    public GameObject info;
    private GameDataScript game_data;

    void Start()
    {
        game_data = GameObject.FindGameObjectWithTag("DataHandler").GetComponent<GameDataScript>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (game_data.tutorial_check)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void showInfo()
    {
        info.SetActive(true);
        prompt.SetActive(false);
    }

    public void finishTutorial()
    {
        game_data.tutorial_check = true;
    }
}
