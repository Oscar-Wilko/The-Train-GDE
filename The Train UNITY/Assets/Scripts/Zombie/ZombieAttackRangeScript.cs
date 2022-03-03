using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttackRangeScript : MonoBehaviour
{
    public GameObject parent;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Carriage")
        {
            parent.GetComponent<ZombieScript>().carriage_in_range = true;
        }
    }
    
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Carriage")
        {
            parent.GetComponent<ZombieScript>().carriage_in_range = false;
        }
    }
}
