using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float train_hp;
    public float max_train_hp;
    public GameObject hp_slider;
    public Toggle toggle_auto_player;
    public GameObject player_tur;

    void Start()
    {

    }

    void Update()
    {
        player_tur.GetComponent<PlayerController>().enabled = !toggle_auto_player.isOn;
        player_tur.GetComponent<AutoController>().enabled = toggle_auto_player.isOn;
        hp_slider.transform.localScale = new Vector3 (train_hp / max_train_hp,1,1);
        if (train_hp <= 0)
        {
            hp_slider.transform.localScale = new Vector3 (0,1,1);
            //Lost game
            SceneManager.LoadScene("Game Lost Scene", LoadSceneMode.Single);
        }
    }
}
