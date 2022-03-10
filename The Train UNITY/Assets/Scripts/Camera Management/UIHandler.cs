using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public bool paused = false;
    public bool upgrading = false;
    public bool prompt_provided = false;
    public GameObject settings_obj;
    public GameObject ui_obj;
    public GameObject upgrade_obj;
    public GameObject upgrade_text_obj;
    public GameObject current_carriage_selected;

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
            Time.timeScale = 1;
            if (upgrading)
            {
                upgrade_obj.SetActive(true);
            }
            else
            {
                upgrade_obj.SetActive(false);
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
        }
    }
}
