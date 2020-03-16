using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private int _FallSpeed;

    [SerializeField]
    private int _PowerUpId;

    private Audio _audio;

    private UiManager UiManager;

    private Spawnmanager _spawnmanager;


    private void Start()
    {
        UiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        if (UiManager == null)
        {
            Debug.LogError("ui manager script is null in the PowerUp class");
        }

        _audio = GameObject.Find("PowerUpAudio").GetComponent<Audio>();
        if (_audio == null)
        {
            Debug.LogError("_audio is null inside PowerUp Class");
        }

        _spawnmanager = GameObject.Find("Spawn_Manager").GetComponent<Spawnmanager>();
        if (_spawnmanager == null)
        {
            Debug.LogError("_spawnManager is null inside PowerUp Class");
        }

    }

    void Update()
    {
        transform.Translate(Vector3.down * _FallSpeed * Time.deltaTime);

        if (_PowerUpId == 5)
        {
            if (transform.position.y <= -7.0f)
            {
                _spawnmanager.StartMissileSpawn();
                Destroy(this.gameObject, 5f);
            }
        }
        else if (transform.position.y < -10.0f)
        {
          Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {

            Player player = collision.transform.GetComponent<Player>();
            if (player != null)
            {
                _audio.PowerUpAudio();

                switch (_PowerUpId)
                {
                    case 0:
                        player.PowerUpCollected();
                        
                        break;
                    case 1:
                        player.SpeedBoost();
                       
                        break;
                    case 2:
                        player.ShieldPowerUp();
                        
                        break;
                    case 3:
                        player.AmmoReload();

                        break;
                    case 4:
                        player.AddHealth();

                        break;
                    case 5:
                        player.SuperPowerMissile();
                        UiManager.MissileImgFlashing();

                        break;
                    default:
                        Debug.Log("Default");
                        break;

                }
                
            }

            Destroy(this.gameObject);
        }
    }
}
