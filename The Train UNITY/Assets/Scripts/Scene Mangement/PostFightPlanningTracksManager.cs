using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostFightPlanningTracksManager : MonoBehaviour
{
    public void PlanningComplete()
    {
        SceneManager.LoadScene("Gameplay Scene (Station)", LoadSceneMode.Single);
    }
}
