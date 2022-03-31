using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonScript : MonoBehaviour
{
    public float damage_per_hit;
    public float min_reload_time;
    public float fire_range;

    public int level;
    public int person_number;

    public float reload_timer;
    public GameObject barrel_obj;
    public GameObject bullet_prefab;
    private GameObject sound_manager;
    private GameObject game_manager;
    private GameObject data_handler;
    private Vector3 last_track_pos;

    void Start()
    {
        sound_manager = GameObject.FindGameObjectWithTag("SoundManager");
        game_manager = GameObject.FindGameObjectWithTag("GameController");
        data_handler = GameObject.FindGameObjectWithTag("DataHandler");
        level = data_handler.GetComponent<GameDataScript>().person_levels[person_number];
        min_reload_time = min_reload_time * Mathf.Pow(0.8f, level);
        damage_per_hit = damage_per_hit * Mathf.Pow(1.5f, level);
    }
    /*
    void Start()
    {
        switch (fire_rate)
        {
            case FireRateState.SLOW:
                min_reload_time = 1f;
                break;
            case FireRateState.MEDIUM:
                min_reload_time = 0.5f;
                break;
            case FireRateState.FAST:
                min_reload_time = 0.3f;
                break;
            case FireRateState.VERY_FAST:
                min_reload_time = 0.15f;
                break;
        }
    }
    */
    void Update()
    {
        reload_timer += Time.deltaTime;
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
        bool zombie_in_range = false;
        Vector3 closest_zombie_vec = new Vector3(0, 0, 0);
        float closest_zombie_dist = 100;
        foreach (GameObject zombie in zombies)
        {
            Vector3 distance_vec = zombie.transform.position - this.transform.position;
            float distance = distance_vec.sqrMagnitude;
            if (distance < fire_range && distance < closest_zombie_dist)
            {
                closest_zombie_vec = zombie.transform.position;
                closest_zombie_dist = distance;
                zombie_in_range = true;
            }
        }
        if (zombie_in_range)
        {
            Track(closest_zombie_vec);
            Shoot();
        }
    }

    public void UpgradePerson()
    {
        if (game_manager.GetComponent<GameManager>().scrap >= 100)
        {
            game_manager.GetComponent<GameManager>().scrap -= 100;
            min_reload_time = min_reload_time * 0.8f;
            damage_per_hit = damage_per_hit * 1.5f;
            level++;
            data_handler.GetComponent<GameDataScript>().person_levels[person_number] = level;
        }
    }

    public void Track(Vector3 track_position)
    {
        Vector3 pos_difference = track_position - this.transform.position;
        pos_difference = new Vector3(pos_difference.x, pos_difference.y, 0);
        float angle = Mathf.Atan(pos_difference.y / pos_difference.x);
        angle = angle * Mathf.Rad2Deg;
        if (pos_difference.x > 0)
        {
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            this.transform.localScale = new Vector3(1, 1, 1);
        }
        this.transform.eulerAngles = new Vector3(0, 0, angle);
        last_track_pos = track_position;
    }

    public void Shoot()
    {
        if (reload_timer >= min_reload_time)
        {
            sound_manager.GetComponent<SoundManager>().PlaySound(1, this.transform.position);
            GameObject bullet = Instantiate(bullet_prefab, barrel_obj.transform.position, this.transform.rotation);
            Vector3 pos_dif = barrel_obj.transform.position - this.transform.position;
            pos_dif.Normalize();
            bullet.GetComponent<BulletScript>().move_dir = pos_dif;
            bullet.GetComponent<BulletScript>().damage = damage_per_hit;
            reload_timer = 0;
        }
    }
}
