using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _Speed;

    private Player player;

    private Animator _Anim;

    private SpriteRenderer _Renderer;

    private BoxCollider2D _BoxCollider2D;

    [SerializeField]
    private AudioSource _ExplosionAudio;

    [SerializeField]
    private GameObject _EnemyLaser;


    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null)
        {
            Debug.LogError("Player in Enemy was not grabbed");
        }

        _Anim = GetComponent<Animator>();
        if(_Anim == null)
        {
            Debug.LogError("_Anim is null");
        }
        _Renderer = GetComponent<SpriteRenderer>();
        if (_Renderer == null)
        {
            Debug.LogError("Sprite Renderer in Enemy Script null");
        }

        _BoxCollider2D = GetComponent<BoxCollider2D>();

        if(_BoxCollider2D == null)
        {
            Debug.LogError("_BoxCollider2D in Enemy Script null");
        }

        StartCoroutine("EnemyShoot");
    }


    void Update()
    {
        transform.Translate(Vector3.down * _Speed * Time.deltaTime);

        if (transform.position.y < -6f)
        {
            transform.position = new Vector3(Random.RandomRange(-9f,9f), 8f, 0f);
        }
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.tag == "Player" )
        {
            Player player = other.transform.GetComponent<Player>();
            if(player != null)
            {
                player.Damage();
            }
            
            _Anim.SetTrigger("OnEnemyDeath");
            _ExplosionAudio.Play();
            _BoxCollider2D.enabled = false;
            Destroy(this.gameObject,2.8f);
        }
        else if (other.gameObject.tag == "Laser")
        {

            _Anim.SetTrigger("OnEnemyDeath");
            _ExplosionAudio.Play();
            _BoxCollider2D.enabled = false;
            Destroy(other.gameObject);
            player.AddScore();
            Destroy(this.gameObject,2.8f);
        }
       
    }

    public void MissileDestroy()
    {
        _Anim.SetTrigger("OnEnemyDeath");
            _ExplosionAudio.Play();
            _BoxCollider2D.enabled = false;
            Destroy(this.gameObject, 2.8f);
    }

    
   
    IEnumerator EnemyShoot()
    {
        while(true)
        {
            float _RandomLaserFire = Random.Range(3,10);
            yield return new WaitForSeconds(_RandomLaserFire);
            Instantiate(_EnemyLaser, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(_RandomLaserFire);
            Instantiate(_EnemyLaser, transform.position, Quaternion.identity);
           
        }
    }

}
