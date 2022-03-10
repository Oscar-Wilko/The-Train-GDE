using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairFollow : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mouse_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            this.transform.position = new Vector3(mouse_pos.x, mouse_pos.y, 0);
        }
    }
}
