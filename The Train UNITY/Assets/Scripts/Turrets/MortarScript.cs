using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarScript : MonoBehaviour
{
    public float damage;
    public float bullet_speed;
    public Vector3 move_dir;
    public Vector3 end_pos;
    private GameObject sound_manager;
    public GameObject explosion_prefab;
    public float start_dist;
    public float start_scale;

    void Start()
    {
        sound_manager = GameObject.FindGameObjectWithTag("SoundManager");
        Vector3 start_vec = end_pos - this.transform.position;
        start_vec = new Vector3(start_vec.x, start_vec.y, 0);
        start_dist = start_vec.magnitude;
        start_scale = this.transform.localScale.x;
    }

    void FixedUpdate()
    {
        Vector3 dist_vec = end_pos - this.transform.position;
        dist_vec = new Vector3(dist_vec.x, dist_vec.y, 0);
        float dist_float = dist_vec.magnitude;
        float scale_perc = dist_float / start_dist;
        float scale_mag = - 8 * ((scale_perc - 0.5f) * (scale_perc - 0.5f)) + 1;
        if (scale_mag < 0.2f)
        {
            scale_mag = 0.2f;
        }
        Debug.Log(scale_mag);
        scale_mag = scale_mag * start_scale * 2;
        this.transform.localScale = new Vector3(Mathf.Pow(scale_mag, 1.5f), scale_mag, 1);
        if (dist_float < 0.2f)
        {
            Explode();
        }
        else
        {
            this.transform.position = this.transform.position + new Vector3(bullet_speed * move_dir.x * Time.deltaTime, bullet_speed * move_dir.y * Time.deltaTime, 0);
        }
        if (this.transform.position.x > 3 || this.transform.position.x < -3)
        {
            Destroy(this.gameObject);
        }
        if (this.transform.position.y > 6 || this.transform.position.y < -6)
        {
            Destroy(this.gameObject);
        }
    }

    void Explode()
    {
        sound_manager.GetComponent<SoundManager>().PlaySound(4, this.transform.position);
        GameObject explosion = Instantiate(explosion_prefab, this.transform.position, Quaternion.identity);
        explosion.GetComponent<ExplosionScript>().damage = damage;
        Destroy(this.gameObject);
    }

}
