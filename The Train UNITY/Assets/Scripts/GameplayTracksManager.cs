using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayTracksManager : MonoBehaviour
{
    public void WavesComplete()
    {
        SceneManager.LoadScene("Post Fight Planning Scene (Tracks)", LoadSceneMode.Single);
    }

    public void GameLost()
    {
        SceneManager.LoadScene("Game Lost Scene", LoadSceneMode.Single);
    }
}
