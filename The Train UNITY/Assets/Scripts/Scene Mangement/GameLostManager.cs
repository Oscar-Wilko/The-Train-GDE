using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLostManager : MonoBehaviour
{
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu Scene", LoadSceneMode.Single);
    }
}
