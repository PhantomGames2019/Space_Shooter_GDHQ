using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{   [SerializeField]
    private float _Speed;

    [SerializeField]
    private GameObject _Explosion;

    private SpriteRenderer _spriteRenderer;

    private Spawnmanager _Spawnmanager;

    [SerializeField]
    private AudioSource _ExplosionAudio;

    private CircleCollider2D _Circlecollider2D;




    private void Start()
    {
        _Circlecollider2D = GetComponent<CircleCollider2D>();
        if (_Circlecollider2D == null)
        {
            Debug.LogError("CircleCollider On Asteroid is null");
        }

        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_spriteRenderer == null)
        {
            Debug.LogError("Sprite Renderer is null");
        }

        _Spawnmanager = GameObject.Find("Spawn_Manager").GetComponent<Spawnmanager>();
        if (_Spawnmanager == null)
        {
            Debug.LogError("Spawn Manager is null");
        }
    }

    void Update()
    {
        transform.Rotate(new Vector3 (0,0,_Speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Laser")
        {
            _ExplosionAudio.Play();
            
            _spriteRenderer.enabled = false;
            Destroy(collision.gameObject);
            GameObject _explosion = Instantiate(_Explosion, transform.position, Quaternion.identity);
            Destroy(_explosion.gameObject, 2f);
            _Spawnmanager.StartSpawing();
            _Circlecollider2D.enabled = false;
            Destroy(this.gameObject,2f);
        }
    }
}
