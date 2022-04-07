using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonWalkScript : MonoBehaviour
{
    public float move_speed;
    public float min_bound_y;
    public float min_bound_x;
    public float max_bound_y;
    public float max_bound_x;
    public Transform spawn;

    void Update()
    {
        if (InBox())
        {
            GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
            Vector3 closest_zombie_vec = new Vector3(0, 0, 0);
            float closest_zombie_dist = 100;
            foreach (GameObject zombie in zombies)
            {
                Vector3 distance_vec = zombie.transform.position - this.transform.position;
                float distance = distance_vec.sqrMagnitude;
                if (distance < closest_zombie_dist)
                {
                    closest_zombie_vec = zombie.transform.position;
                    closest_zombie_dist = distance;
                }
            }
            if (closest_zombie_vec == new Vector3(0, 0, 0))
            {
                Walk(spawn.position);
            }
            else
            {
                Walk(closest_zombie_vec);
            }
        }
        else
        {
            Walk(spawn.position);
        }
    }

    public void Walk(Vector3 zombie_vec)
    {
        Vector3 pos_diff = zombie_vec - this.transform.position;
        pos_diff.Normalize();
        this.transform.position = this.transform.position + (pos_diff * Time.deltaTime * move_speed);
    }

    public bool InBox()
    {
        Debug.Log(this.transform.localPosition);
        if (this.transform.localPosition.x < max_bound_x && this.transform.localPosition.x > min_bound_x)
        {
            if (this.transform.localPosition.y < max_bound_y && this.transform.localPosition.y > min_bound_y)
            {
                return true;
            }
        }
        return false;
    }
}
