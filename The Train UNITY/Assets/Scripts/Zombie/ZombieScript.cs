using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    private GameObject designated_carriage;
    public bool carriage_in_range = false;
    public int hit_points;
    public float move_speed;
    public float attack_speed;
    public float attack_timer;
    public float attack_range;
    public CircleCollider2D attack_range_hitbox;
    void Start()
    {
        attack_range_hitbox.radius = attack_range * 4;
        GameObject[] carriages = GameObject.FindGameObjectsWithTag("Carriage");
        float closest_carriage_dist = 100;
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
        Destroy(this.gameObject);
    }

    public void Attack()
    {
        if (attack_timer > attack_speed)
        {
            //Carriage takes damage
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