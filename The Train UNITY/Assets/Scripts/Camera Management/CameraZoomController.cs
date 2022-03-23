using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraZoomController : MonoBehaviour
{
    private Camera cam;
    public float max_zoom;
    public float min_zoom;
    public float cur_zoom;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        cur_zoom = (cam.orthographicSize - min_zoom) / (max_zoom - min_zoom);
    }

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        scroll = scroll * 0.1f;
        Debug.Log(scroll);
        if (scroll < 0)
        {
            if (cur_zoom >= 1 + scroll)
            {
                cur_zoom = 1;
            }
            else
            {
                cur_zoom += scroll;
            }
        } 
        else 
        {
            if (cur_zoom <= scroll)
            {
                cur_zoom = 0;
            }
            else
            {
                cur_zoom += scroll;
            }
        }
        cam.orthographicSize = min_zoom + (max_zoom - min_zoom) * cur_zoom;
    }
}
