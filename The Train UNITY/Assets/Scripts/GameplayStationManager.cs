using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayStationManager : MonoBehaviour
{
    public void WavesCompleteComplete()
    {
        SceneManager.LoadScene("Post Fight Planning Scene (Station)", LoadSceneMode.Single);
    }

    public void GameWon()
    {
        SceneManager.LoadScene("Game Won Scene", LoadSceneMode.Single);
    }

    public void GameLost()
    {
        SceneManager.LoadScene("Game Lost Scene", LoadSceneMode.Single);
    }
}
