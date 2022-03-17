using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] clips;
    public float[] volumes;
    public GameObject sound_pre;
    public AudioSource train_sound;
    public Slider music_slider;
    public Slider sfx_slider;
    public GameDataScript game_data;

    public void PlaySound(int sound_num, Vector3 position)
    {
        game_data = GameObject.FindGameObjectWithTag("DataHandler").GetComponent<GameDataScript>();
        sfx_slider.value = game_data.sfx_vol;
        music_slider.value = game_data.music_vol;
        //0 is bullet hit, 1 is bullet shot, 2 is zombie death, 3 is train death, 4 is explosion, 5 is train hit, 6 is attach carriage
        GameObject sound = Instantiate(sound_pre, position, Quaternion.identity);
        sound.GetComponent<AudioSource>().clip = clips[sound_num];
        sound.GetComponent<AudioSource>().volume = volumes[sound_num] * sfx_slider.value;
        sound.GetComponent<AudioSource>().Play();
    }

    void Update()
    {
        train_sound.volume = sfx_slider.value * 0.2f;
    }
}
