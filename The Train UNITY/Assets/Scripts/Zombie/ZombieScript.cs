using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public GameObject designated_carriage;
    public GameObject game_manager;
    public bool carriage_in_range = false;
    public float hit_points;
    public float move_speed;
    public float attack_damage;
    public float attack_speed;
    public float attack_timer;
    public float attack_range;
    public int scrap_value;
    public CircleCollider2D attack_range_hitbox;
    private GameObject sound_manager;

    void Start()
    {
        sound_manager = GameObject.FindGameObjectWithTag("SoundManager");
        game_manager = GameObject.FindGameObjectWithTag("GameController");
        attack_range_hitbox.radius = attack_range * 4;
        GameObject[] carriages = GameObject.FindGameObjectsWithTag("Carriage");
        float closest_carriage_dist = 1000;
        foreach (GameObject carriage in carriages)
        {
            Vector3 distance_vec = carriage.transform.position - this.transform.position;
            float distance = distance_vec.sqrMagnitude;
            if (distance < closest_carriage_dist)
            {
                designated_carriage = carriage;
                closest_carriage_dist = distance;
            }
        }
    }

    void Update()
    {
        if (hit_points <= 0)
        {
            Die();
        }
        else if (carriage_in_range)
        {
            Attack();
        }
        else
        {
            Move();
        }
        attack_timer += Time.deltaTime;
    }

    public void Die()
    {
        //stuff later
        sound_manager.GetComponent<SoundManager>().PlaySound(2, this.transform.position);
        game_manager.GetComponent<GameManager>().scrap += scrap_value;
        Destroy(this.gameObject);
    }

    public void Attack()
    {
        if (attack_timer > attack_speed)
        {
            //Carriage takes damage
            sound_manager.GetComponent<SoundManager>().PlaySound(5, this.transform.position);
            game_manager.GetComponent<GameManager>().train_hp = game_manager.GetComponent<GameManager>().train_hp - attack_damage;
            attack_timer = 0;
        }
    }

    public void Move()
    {
        Vector3 move_direction = this.transform.position - designated_carriage.transform.position;
        move_direction.Normalize();
        this.transform.position = this.transform.position - move_speed * move_direction * Time.deltaTime;
    }
}
