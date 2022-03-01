using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public int damage;
    public float bullet_speed;
    public Vector3 move_dir;
    private GameObject sound_manager;

    void Start()
    {
        sound_manager = GameObject.FindGameObjectWithTag("SoundManager");
    }

    void FixedUpdate()
    {
        this.transform.position = this.transform.position + new Vector3(bullet_speed * move_dir.x * Time.deltaTime, bullet_speed * move_dir.y * Time.deltaTime, 0);
        if (this.transform.position.x > 3 || this.transform.position.x < -3)
        {
            Destroy(this.gameObject);
        }
        if (this.transform.position.y > 6 || this.transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Zombie")
        {
            sound_manager.GetComponent<SoundManager>().PlaySound(0, this.transform.position);
            col.gameObject.GetComponentInParent<ZombieScript>().hit_points -= damage;
            Destroy(this.gameObject);
        }
    }
}
