using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataScript : MonoBehaviour
{
    public int[] turret_levels;
    public string[] turret_types;
    public int scrap;
    public float sfx_vol;
    public float music_vol;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
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
}
