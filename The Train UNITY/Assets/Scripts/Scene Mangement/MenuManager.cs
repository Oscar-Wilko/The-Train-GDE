using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject main_menu_object;
    public GameObject select_mode_object;
    public GameObject data_handler;

    void Start()
    {
        data_handler = GameObject.FindGameObjectWithTag("DataHandler");
    }

    public void NewGameButton()
    {
        main_menu_object.SetActive(false);
        select_mode_object.SetActive(true);
    }

    public void ContinueButton()
    {
        SceneManager.LoadScene("Post Fight Planning Scene (Station)", LoadSceneMode.Single);
    }

    public void CasualOrIdleButton()
    {
        SceneManager.LoadScene("Tutorial Scene", LoadSceneMode.Single);
        ResetData();
    }

    public void BackButton()
    {
        main_menu_object.SetActive(true);
        select_mode_object.SetActive(false);
    }

    public void ResetData()
    {
        data_handler.GetComponent<GameDataScript>().Reset();
    }
}
