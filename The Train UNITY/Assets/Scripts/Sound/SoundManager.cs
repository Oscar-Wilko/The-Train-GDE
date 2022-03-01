using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] clips;
    public float[] volumes;
    public GameObject sound_pre;
    public Slider music_slider;
    public Slider sfx_slider;

    public void PlaySound(int sound_num, Vector3 position)
    {
        //0 is bullet hit, 1 is bullet shot, 2 is zombie death, 3 is train death, 4 is explosion, 5 is train hit, 6 is attach carriage
        GameObject sound = Instantiate(sound_pre, position, Quaternion.identity);
        sound.GetComponent<AudioSource>().clip = clips[sound_num];
        sound.GetComponent<AudioSource>().volume = volumes[sound_num] * sfx_slider.value;
        sound.GetComponent<AudioSource>().Play();
    }
}
