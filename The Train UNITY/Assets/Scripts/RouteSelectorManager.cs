using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RouteSelectorManager : MonoBehaviour
{
    public void RoutePlanningComplete()
    {
        SceneManager.LoadScene("Gameplay Scene (Tracks)", LoadSceneMode.Single);
    }
}
