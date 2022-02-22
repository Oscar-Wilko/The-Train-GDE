using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public void TutorialComplete()
    {
        SceneManager.LoadScene("Route Selector Scene", LoadSceneMode.Single);
    }
}
