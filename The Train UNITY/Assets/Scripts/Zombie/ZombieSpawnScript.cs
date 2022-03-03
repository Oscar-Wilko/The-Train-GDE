using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ZombieSpawnScript : MonoBehaviour
{
    public float spawn_point_y;
    public float spawn_point_x;
    public bool on_tracks;
    public float spawn_frequency;
    public float spawn_timer = 0;
    public int wave_counter = 0;
    public int max_waves;
    public int zombies_per_wave;
    public int zombies_spawned_this_wave = 0;
    public GameObject zombie_prefab;
    public Animator next_wave_anim;
    public Animator train_anim;
    public TextMeshProUGUI wave_text;

    void Update()
    {
        wave_text.text = wave_counter.ToString() + "/" + max_waves.ToString();
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
        spawn_timer += Time.deltaTime;
        if (spawn_timer >= (1/spawn_frequency) && Random.Range(0,10) >= 8 && zombies_spawned_this_wave < zombies_per_wave)
        {
            Spawn();
        }
        else if (zombies.Length == 0 && zombies_spawned_this_wave >= zombies_per_wave)
        {
            if (wave_counter < max_waves)
            {
                wave_counter++;
                zombies_spawned_this_wave = 0;
                spawn_timer = -1;
                next_wave_anim.SetTrigger("NextWave");
            }
            else
            {
                StartCoroutine(LoadSceneDelay());
            }
        }
    }

    public void Spawn()
    {
        zombies_spawned_this_wave ++;
        spawn_timer = 0;
        float temp_x;
        float temp_y = Random.Range(-spawn_point_x, spawn_point_x);
        if (on_tracks && Random.Range(0,2) == 0)
        {
            temp_x = Random.Range(-spawn_point_x, spawn_point_x);
            temp_y = spawn_point_y * (Random.Range(0, 2) * 2 - 1);
        }
        else
        {
            temp_x = spawn_point_x * (Random.Range(0, 2) * 2 - 1);
            temp_y = Random.Range(-spawn_point_y, spawn_point_y);
        }
        Vector3 spawn_pos = new Vector3(temp_x, temp_y, 0);
        GameObject zombie_instance = Instantiate(zombie_prefab, spawn_pos, Quaternion.identity);
    }

    IEnumerator LoadSceneDelay()
    {
        train_anim.SetTrigger("Exit");
        yield return new WaitForSeconds(3);
        if (on_tracks)
        {
            SceneManager.LoadScene("Post Fight Planning Scene (Tracks)", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("Post Fight Planning Scene (Station)", LoadSceneMode.Single);
        }
    }
}
