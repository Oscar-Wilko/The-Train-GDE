using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHandler : MonoBehaviour
{
    public bool paused = false;
    public GameObject settings_obj;
    public GameObject ui_obj;
    void Update()
    {
        if (paused)
        {
            settings_obj.SetActive(true);
            ui_obj.SetActive(false);
        }
        else
        {
            settings_obj.SetActive(false);
            ui_obj.SetActive(true);
        }
    }

    public void toggleMenu()
    {
        paused = !paused;
    }
}
