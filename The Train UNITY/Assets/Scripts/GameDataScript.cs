using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataScript : MonoBehaviour
{
    public class TurretData
    {
        int level;
        string tur_type;
    }

    static TurretData[] turretdata;

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void SetTurData(int num, int lvl, string type)
    {
        turretdata[num].level = lvl;
        turretdata[num].tur_type = type;
    }
}
