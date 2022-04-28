using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIHandler : MonoBehaviour
{
    public bool paused = false;
    public bool upgrading = false;
    public bool prompt_provided = false;
    public string[] prompt_list;
    public GameObject settings_obj;
    public GameObject ui_obj;
    public GameObject upgrade_obj;
    public GameObject upgrade_text_obj;
    public TextMeshProUGUI upgrade_text;
    public GameObject current_carriage_selected;
    public GameObject upgrade_types;
    public GameObject upgrade_level;
    public string selected_type;
    public GameObject[] ticks;
    public GameObject[] non_ticks;
    private GameDataScript game_data;

    void Start()
    {
        game_data = GameObject.FindGameObjectWithTag("DataHandler").GetComponent<GameDataScript>();
        ticks = GameObject.FindGameObjectsWithTag("Tick");
        non_ticks = GameObject.FindGameObjectsWithTag("Non_Ticks");
        foreach (GameObject tick in ticks)
        {
            tick.SetActive(false);
        }
        foreach (GameObject non_tick in non_ticks)
        {
            non_tick.SetActive(true);
        }
    }

    void Update()
    {
        if (paused)
        {
            settings_obj.SetActive(true);
            ui_obj.SetActive(false);
            Time.timeScale = 0;
            upgrade_obj.SetActive(false);
        }
        else
        {
            settings_obj.SetActive(false);
            ui_obj.SetActive(true);
            if (upgrading)
            {
                string tur_type = current_carriage_selected.GetComponent<TurretScript>().type;
                upgrade_obj.transform.position = Camera.main.WorldToScreenPoint(current_carriage_selected.transform.position);
                upgrade_obj.SetActive(true);
                if (tur_type == "Normal")
                {
                    upgrade_types.SetActive(true);
                    upgrade_level.SetActive(false);
                }
                else
                {
                    upgrade_types.SetActive(false);
                    upgrade_level.SetActive(true);
                }
                Time.timeScale = 0.1f;
            }
            else
            {
                upgrade_obj.SetActive(false);

                foreach (GameObject tick in ticks)
                {
                    tick.SetActive(false);
                }
                foreach (GameObject non_tick in non_ticks)
                {
                    non_tick.SetActive(true);
                }

                if (!game_data.tutorial_check)
                {
                    Time.timeScale = 0;
                }
                else
                {
                    Time.timeScale = 1;
                }
            }
            if (prompt_provided)
            {
                upgrade_text_obj.SetActive(true);
            }
            else
            {
                upgrade_text_obj.SetActive(false);
            }
        }
    }

    public void toggleMenu()
    {
        paused = !paused;
    }

    public void UpgradeButton()
    {
        if (prompt_provided)
        {
            upgrading = false;
            prompt_provided = false;
            // go into carriage script to upgrade
            current_carriage_selected.GetComponent<TurretScript>().UpgradeCarriage();
        }
        else
        {
            prompt_provided = true;
            upgrade_text.text = prompt_list[0];
        }
    }

    public void UpgradeType(string type)
    {
        foreach (GameObject tick in ticks)
        {
            tick.SetActive(false);
        }
        foreach (GameObject non_tick in non_ticks)
        {
            non_tick.SetActive(true);
        }
        if (selected_type == type)
        {
            upgrading = false;
            prompt_provided = false;
            current_carriage_selected.GetComponent<TurretScript>().UpgradeType(selected_type);
            selected_type = null;
        }
        else
        {
            selected_type = type;
            prompt_provided = true;
            if (type == "Fire")
            {
                upgrade_text.text = prompt_list[1];
            }
            if (type == "Explosive")
            {
                upgrade_text.text = prompt_list[2];
            }
            if (type == "Artillery")
            {
                upgrade_text.text = prompt_list[3];
            }
        }

    }

    public void CancelUpgrade()
    {
        prompt_provided = false;
        upgrading = false;
        selected_type = null;
        foreach (GameObject tick in ticks)
        {
            tick.SetActive(false);
        }
        foreach (GameObject non_tick in non_ticks)
        {
            non_tick.SetActive(true);
        }
    }
}
