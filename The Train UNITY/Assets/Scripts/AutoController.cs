using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoController : MonoBehaviour
{
    public TurretScript tur_script;
    void Start()
    {
        tur_script = this.GetComponent<TurretScript>();
    }

    void Update()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
        bool zombie_in_range = false;
        Vector3 closest_zombie_vec = new Vector3(0, 0, 0);
        float closest_zombie_dist = 100;
        foreach (GameObject zombie in zombies)
        {
            Vector3 distance_vec = zombie.transform.position - this.transform.position;
            float distance = distance_vec.sqrMagnitude;
            if (distance < tur_script.fire_range && distance < closest_zombie_dist)
            {
                closest_zombie_vec = zombie.transform.position;
                closest_zombie_dist = distance;
                zombie_in_range = true;
            }
        }
        if (zombie_in_range)
        {
            tur_script.Track(closest_zombie_vec);
            tur_script.Shoot();
        }
    }
}
