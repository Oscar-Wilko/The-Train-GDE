using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float train_hp;
    public float max_train_hp;
    public GameObject tutorial_ui;
    public GameObject hp_slider;
    public GameObject forcefield_obj;
    public GameObject forcefield_ui;
    public Toggle toggle_auto_player;
    public GameObject player_tur;
    public TextMeshProUGUI scrap_text;
    public GameDataScript game_data;
    public int scrap;
    public bool forcefield_active = false;
    private float forcefield_timer = 10;
    private float forcefield_duration = 5;
    private float forcefield_recharge = 10;

    void Start()
    {
        game_data = GameObject.FindGameObjectWithTag("DataHandler").GetComponent<GameDataScript>();
        scrap = game_data.scrap;
    }

    void Update()
    {
        forcefield_timer += Time.deltaTime;
        if (forcefield_active && forcefield_timer >= forcefield_duration)
        {
            forcefield_active = false;
            forcefield_timer = 0;
        }
        forcefield_obj.SetActive(forcefield_active);
        if (forcefield_active)
        {
            forcefield_ui.GetComponent<Image>().fillAmount = 1 - forcefield_timer / forcefield_duration;
        }
        else
        {
            forcefield_ui.GetComponent<Image>().fillAmount = forcefield_timer / forcefield_recharge;
        }
        if (!game_data.tutorial_check)
        {
            tutorial_ui.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            tutorial_ui.SetActive(false);
        }
        scrap_text.text = scrap.ToString();
        game_data.scrap = scrap;
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

    public void Forcefield()
    {
        if (!forcefield_active && forcefield_recharge <= forcefield_timer)
        {
            forcefield_timer = 0;
            forcefield_active = true;
        }
    }
}
