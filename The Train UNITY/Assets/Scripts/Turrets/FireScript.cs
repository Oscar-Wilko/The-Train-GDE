using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    public float damage;
    public float bullet_speed;
    public Vector3 move_dir;
    private GameObject sound_manager;
    public float life_duration;
    public float life_timer;

    void Start()
    {
        sound_manager = GameObject.FindGameObjectWithTag("SoundManager");
    }

    void FixedUpdate()
    {
        if (life_timer >= life_duration)
        {
            Destroy(this.gameObject);
        }
        life_timer += Time.deltaTime;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Zombie")
        {
            Debug.Log("Hit Fire");
            //sound_manager.GetComponent<SoundManager>().PlaySound(0, this.transform.position);
            col.gameObject.GetComponentInParent<ZombieScript>().hit_points -= damage * Time.deltaTime;
        }
    }
}
