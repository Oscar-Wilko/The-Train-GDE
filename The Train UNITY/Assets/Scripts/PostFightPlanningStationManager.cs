using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostFightPlanningStationManager : MonoBehaviour
{
    public void PlanningComplete()
    {
        SceneManager.LoadScene("Route Selector Scene", LoadSceneMode.Single);
    }
}
