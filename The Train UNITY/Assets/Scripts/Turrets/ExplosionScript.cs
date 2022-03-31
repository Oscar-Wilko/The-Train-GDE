using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    public float duration;
    public float damage;
    private float timer = 0;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= duration)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col);
        if (col.gameObject.tag == "Zombie")
        {
            Debug.Log(damage);
            col.gameObject.GetComponentInParent<ZombieScript>().hit_points -= damage;
        }
    }
}
