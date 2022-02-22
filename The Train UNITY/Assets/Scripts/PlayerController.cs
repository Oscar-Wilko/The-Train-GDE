using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public TurretScript tur_script;
    void Start()
    {
        tur_script = this.GetComponent<TurretScript>();
    }

    void Update()
    {
        Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
            //Track
            tur_script.Track(mouse_pos);

            //Fire
            tur_script.Shoot();
        }
    }
}
