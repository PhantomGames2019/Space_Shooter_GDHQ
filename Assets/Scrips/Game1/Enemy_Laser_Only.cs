﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Laser_Only : MonoBehaviour
{
    [SerializeField]
    private float _EnemyLaserSpeed;
    private AudioSource _ExplosionSound;
    private Player player;

    private void Start()
    {
        _ExplosionSound = GameObject.Find("ExplosionSound").GetComponent<AudioSource>();
    }

    void Update()
    {
        transform.Translate(Vector3.down * _EnemyLaserSpeed * Time.deltaTime);
        if (transform.position.y < -7)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Player _Player = collision.transform.GetComponent<Player>();
            if (_Player != null)
            {
                _ExplosionSound.Play();
                _Player.Damage();
                Destroy(this.gameObject);
            }
        }
    }
}