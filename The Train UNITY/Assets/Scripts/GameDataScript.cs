using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataScript : MonoBehaviour
{
    public int[] turret_levels;
    public string[] turret_types;
    public int[] person_levels;
    public int scrap;
    public float sfx_vol;
    public float music_vol;
    private int[] start_tur_lvl;
    private string[] start_tur_type;
    private int[] start_per_lvl;
    private int zombies_killed;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        start_tur_type = turret_types;
        start_tur_lvl = turret_levels;
        start_per_lvl = person_levels;
        zombies_killed = 0;
    }

    public void SetTurData(int num, int lvl, string type)
    {
        turret_levels[num] = lvl;
        turret_types[num] = type;
    }

    public void AddTurretData(int lvl, string type)
    {
        turret_levels[turret_levels.Length - 1] = lvl;
        turret_types[turret_types.Length - 1] = type;
    }

    public void Reset()
    {
        scrap = 100;
        turret_levels = start_tur_lvl;
        turret_types = start_tur_type;
        person_levels = start_per_lvl;
    }

    public void ZombieKilled()
    {
        zombies_killed++;
    }
}
