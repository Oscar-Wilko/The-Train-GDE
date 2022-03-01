using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroySound : MonoBehaviour
{
    void Update()
    {
        if (!this.GetComponent<AudioSource>().isPlaying)
        {
            Destroy(this.gameObject);
        }
    }
}
