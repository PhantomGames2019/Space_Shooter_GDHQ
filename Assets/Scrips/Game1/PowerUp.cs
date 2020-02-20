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

    private void Start()
    {
        _audio = GameObject.Find("PowerUpAudio").GetComponent<Audio>();
    }


    void Update()
    {
        transform.Translate(Vector3.down * _FallSpeed * Time.deltaTime);
        if (transform.position.y < -6.0f)
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
                    default:
                        Debug.Log("Default");
                        break;

                }
                
            }

            Destroy(this.gameObject);
        }
    }
}
