using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperPowerUp : MonoBehaviour
{
    [SerializeField]
    private float _MissileSpeed;

    [SerializeField]
    private GameObject _MissileExplo;

    private UiManager _uiManager;
   
    private bool _CanPlayExploAnim = false;

    private SpriteRenderer _spriteRenderer;

    private Spawnmanager _spawnManager;
   
   private GameObject _FinThruster;
    
    
    private void Start()
    {
        _FinThruster = this.gameObject.transform.GetChild(0).gameObject;
        if (_FinThruster == null)
        {
            Debug.LogError("_FinThurster is null inside the SuperPowerUp class");
        }

        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
        {
            Debug.LogError("_spriteRenderer is null inside the SuperPowerUp class");
        }
        

        _uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        if (_uiManager == null)
        {
            Debug.LogError("UI Manager inside SuperPowerUp Class is Null");
        }

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawnmanager>();
        if (_spawnManager == null)
        {
            Debug.LogError("_spawnManager inside SuperPowerUp Class is Null");
        }
    }

    void Update()
    {
        Movement();
        MissleNotFired();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {

            if (Input.GetKey(KeyCode.Space))
            {
                _spriteRenderer.enabled = false;
                _FinThruster.SetActive(false);
                Destroy(collision.gameObject);
                GameObject _MissileExplosion = Instantiate(_MissileExplo, transform.position, Quaternion.identity);
                Destroy(_MissileExplosion, 2f);
                _spawnManager.StartMissileSpawn();
                Destroy(this.gameObject, 2f);
            }
        }
       
    }

    private void Movement()
    {
        transform.Translate(Vector3.up * _MissileSpeed * Time.deltaTime);
    }

    private void MissleNotFired()
    {
        if (transform.position.y >= 10f)
        {
            _spawnManager.StartMissileSpawn();
            Destroy(this.gameObject, 5f);
        }
    }
    
}
