using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraZoomController : MonoBehaviour
{
    private Camera cam;
    private Slider slider;
    public float max_zoom;
    public float min_zoom;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        slider = this.GetComponent<Slider>();
        slider.value = (cam.orthographicSize - min_zoom) / (max_zoom - min_zoom);
    }

    void Update()
    {
        cam.orthographicSize = min_zoom + (max_zoom - min_zoom) * slider.value;
    }
}
