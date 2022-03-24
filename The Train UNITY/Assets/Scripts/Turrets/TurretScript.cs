using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretScript : MonoBehaviour
{
    enum FireRateState : int
    {
        SLOW,
        MEDIUM,
        FAST,
        VERY_FAST
    }

    public Sprite[] turret_sprites; // 0 is normal, 1 is fire, 2 is explosive, 3 is artillery
    
    public float damage_per_second;
    public float damage_per_hit;
    private FireRateState fire_rate;
    public float min_reload_time;
    public float fire_range;

    public int level;
    public string type;
    public int turret_number;

    public float reload_timer;
    public GameObject barrel_obj;
    public GameObject bullet_prefab;
    public GameObject fire_prefab;
    public GameObject mortar_prefab;
    public GameObject artillery_bullet_prefab;
    private GameObject ui_handler;
    private GameObject sound_manager;
    private GameObject game_manager;
    private GameObject upgrade_ui;
    private GameObject data_handler;
    private Vector3 last_track_pos;
    public BoxCollider2D click_coll;

    void Start()
    {
        sound_manager = GameObject.FindGameObjectWithTag("SoundManager");
        ui_handler = GameObject.FindGameObjectWithTag("UIHandler");
        game_manager = GameObject.FindGameObjectWithTag("GameController");
        upgrade_ui = GameObject.FindGameObjectWithTag("Upgrades");
        data_handler = GameObject.FindGameObjectWithTag("DataHandler");
        level = data_handler.GetComponent<GameDataScript>().turret_levels[turret_number];
        SetType(data_handler.GetComponent<GameDataScript>().turret_types[turret_number]);
        min_reload_time = min_reload_time * Mathf.Pow(0.8f, level);
        damage_per_hit = damage_per_hit * Mathf.Pow(1.5f, level);
    }
    /*
    void Start()
    {
        switch (fire_rate)
        {
            case FireRateState.SLOW:
                min_reload_time = 1f;
                break;
            case FireRateState.MEDIUM:
                min_reload_time = 0.5f;
                break;
            case FireRateState.FAST:
                min_reload_time = 0.3f;
                break;
            case FireRateState.VERY_FAST:
                min_reload_time = 0.15f;
                break;
        }
    }
    */
    void Update()
    {
        reload_timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Transform parentObj = this.transform.parent;
            if (click_coll.transform.position.x - (click_coll.size.x * parentObj.transform.localScale.x/2) <= mousePos.x && mousePos.x <= (click_coll.transform.position.x + (click_coll.size.x * parentObj.transform.localScale.x / 2)))
            {
                if (click_coll.transform.position.y + (click_coll.size.y * parentObj.transform.localScale.y / 2) >= mousePos.y && mousePos.y >= (click_coll.transform.position.y - (click_coll.size.y * parentObj.transform.localScale.y / 2)))
                {
                    ui_handler.GetComponent<UIHandler>().upgrading = true;
                    ui_handler.GetComponent<UIHandler>().current_carriage_selected = this.gameObject;
                    upgrade_ui.transform.position = Camera.main.WorldToScreenPoint(this.transform.position);
                }
            }
        }
    }

    public void UpgradeCarriage()
    {
        if (game_manager.GetComponent<GameManager>().scrap >= 100)
        {
            game_manager.GetComponent<GameManager>().scrap -= 100;
            min_reload_time = min_reload_time * 0.8f;
            damage_per_hit = damage_per_hit * 1.5f;
            level++;
            data_handler.GetComponent<GameDataScript>().turret_levels[turret_number] = level;
        }
    }

    public void UpgradeType(string new_type)
    {
        if (game_manager.GetComponent<GameManager>().scrap >= 150)
        {
            game_manager.GetComponent<GameManager>().scrap -= 150;
            data_handler.GetComponent<GameDataScript>().SetTurData(turret_number, level, new_type);
            SetType(new_type);
        }
    }

    public void SetType(string new_type)
    {
        type = new_type;
        if (new_type == "Normal")
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = turret_sprites[0];
        }
        else if (new_type == "Fire")
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = turret_sprites[1];
            damage_per_hit = 5;
            min_reload_time = 0.15f;
            
        }
        else if (new_type == "Explosive")
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = turret_sprites[2];
            damage_per_hit = 10;
            min_reload_time = 1.5f;
        }
        else if (new_type == "Artillery")
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = turret_sprites[3];
            damage_per_hit = 2;
            min_reload_time = 0.2f;
        }
    }

    public void Track(Vector3 track_position)
    {
        Vector3 pos_difference = track_position - this.transform.position;
        pos_difference = new Vector3(pos_difference.x, pos_difference.y, 0);
        float angle = Mathf.Atan(pos_difference.y / pos_difference.x);
        angle = angle * Mathf.Rad2Deg;
        if (pos_difference.x < 0)
        {
            angle -= 180;
        }
        this.transform.eulerAngles = new Vector3(0, 0, angle);
        last_track_pos = track_position;
    }

    public void Shoot()
    {
        if (reload_timer >= min_reload_time)
        {
            if (type == "Normal")
            {
                ShootNormalBullet();
            }
            else if (type == "Fire")
            {
                ShootFire();
            }
            else if (type == "Explosive")
            {
                ShootExplosive();
            }
            else if (type == "Artillery")
            {
                ShootArtilleryBullet();
            }
        }
    }

    public void OnDisable()
    {
        data_handler.GetComponent<GameDataScript>().SetTurData(turret_number, level, type);
    }

    public void ShootNormalBullet()
    {
        sound_manager.GetComponent<SoundManager>().PlaySound(1, this.transform.position);
        GameObject bullet = Instantiate(bullet_prefab, barrel_obj.transform.position, this.transform.rotation);
        Vector3 pos_dif = barrel_obj.transform.position - this.transform.position;
        pos_dif.Normalize();
        bullet.GetComponent<BulletScript>().move_dir = pos_dif;
        bullet.GetComponent<BulletScript>().damage = damage_per_hit;
        reload_timer = 0;
    }

    public void ShootFire()
    {
        sound_manager.GetComponent<SoundManager>().PlaySound(1, this.transform.position);
        GameObject bullet = Instantiate(fire_prefab, barrel_obj.transform.position, this.transform.rotation);
        Vector3 pos_dif = barrel_obj.transform.position - this.transform.position;
        pos_dif.Normalize();
        bullet.GetComponent<FireScript>().move_dir = pos_dif;
        bullet.GetComponent<FireScript>().damage = damage_per_hit;
        reload_timer = 0;
    }

    public void ShootExplosive()
    {
        sound_manager.GetComponent<SoundManager>().PlaySound(1, this.transform.position);
        GameObject bullet = Instantiate(mortar_prefab, barrel_obj.transform.position, this.transform.rotation);
        Vector3 pos_dif = barrel_obj.transform.position - this.transform.position;
        pos_dif.Normalize();
        bullet.GetComponent<MortarScript>().end_pos = last_track_pos;
        bullet.GetComponent<MortarScript>().move_dir = pos_dif;
        bullet.GetComponent<MortarScript>().damage = damage_per_hit;
        reload_timer = 0;
    }

    public void ShootArtilleryBullet()
    {
        sound_manager.GetComponent<SoundManager>().PlaySound(1, this.transform.position);
        GameObject bullet = Instantiate(artillery_bullet_prefab, barrel_obj.transform.position, this.transform.rotation);
        Vector3 pos_dif = barrel_obj.transform.position - this.transform.position;
        pos_dif.Normalize();
        bullet.GetComponent<BulletScript>().move_dir = pos_dif;
        bullet.GetComponent<BulletScript>().damage = damage_per_hit;
        reload_timer = 0;
    }
}
