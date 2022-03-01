using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    enum FireRateState : int
    {
        SLOW,
        MEDIUM,
        FAST,
        VERY_FAST
    }

    public float damage_per_second;
    public int damage_per_hit;
    private FireRateState fire_rate;
    public float min_reload_time;
    public float fire_range;
    public int level;
    public float reload_timer;
    public GameObject barrel_obj;
    public GameObject bullet_prefab;
    private GameObject sound_manager;

    void Start()
    {
        sound_manager = GameObject.FindGameObjectWithTag("SoundManager");
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
    }

    public void Track(Vector3 track_position)
    {
        Vector3 pos_difference = track_position - this.transform.position;
        pos_difference = new Vector3(pos_difference.x, pos_difference.y, 0);
        float angle = Mathf.Atan(pos_difference.y / pos_difference.x);
        angle = angle * Mathf.Rad2Deg;
        if (pos_difference.x < 0)
        {
            angle -= 180;
        }
        this.transform.eulerAngles = new Vector3(0, 0, angle);
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
