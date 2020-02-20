using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField]
    private float _Speed;
    [SerializeField]
    private GameObject _LaserPreFab;
    private float _Canfire = -1;
    [SerializeField]
    private float _FireRate = 0.5f;
    [SerializeField]
    private int _Health = 3;

    private Spawnmanager _spawnmanager;
    
    private bool _TrippleShotActive = false;
    [SerializeField]
    private GameObject _TrippleShot;

    private bool _SpeedBoostActive = false;
    private bool _ShieldIsActive = false;
    [SerializeField]
    private GameObject _ShieldPowerUp;
    private UiManager uiManager;
    [SerializeField]
    private GameObject _Engineleft, _EngineRight;
    [SerializeField]
    private AudioSource _LaserAudio;
    [SerializeField]
    private int _score;


    void Start()
    {
        uiManager = GameObject.Find("Canvas").GetComponent<UiManager>();
        if(uiManager == null)
        {
            Debug.LogError("Canvas was not grabbed from player script");
        }

        transform.position = new Vector3(0, 0, 0);
        _spawnmanager = GameObject.Find("Spawn_Manager").GetComponent<Spawnmanager>();
        if (_spawnmanager == null)
        {
            Debug.LogError("Spawn_Manager has failed to grab");
        }
    }


    void Update()
    {
        PlayerMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _Canfire)
        {
            LaserFire();
        }
    }

    private void PlayerMovement()
    {
        float HorizontalInput = Input.GetAxis("Horizontal");
        float VerticalInput = Input.GetAxis("Vertical");

        transform.Translate(new Vector3(HorizontalInput, VerticalInput, 0) * _Speed * Time.deltaTime);

        if (_SpeedBoostActive == true)
        {
            _Speed = 10;
            StartCoroutine("SpeedBoostControl");
        }

        if (transform.position.y >= 1.5f)
        {
            transform.position = new Vector3(transform.position.x, 1.5f, transform.position.z);
        }
        else if (transform.position.y <= -4)
        {
            transform.position = new Vector3(transform.position.x, -4, transform.position.z);
        }

        if (transform.position.x <= -11.69)
        {
            transform.position = new Vector3(11.74f, transform.position.y, transform.position.z);
        }

        else if (transform.position.x >= 11.74)
        {
            transform.position = new Vector3(-11.69f, transform.position.y, transform.position.z);
        }
    }

    private void LaserFire()
    {

        _Canfire = Time.time + _FireRate;

        if (_TrippleShotActive == true)
        {
            Instantiate(_TrippleShot, transform.position, Quaternion.identity);
            _LaserAudio.Play();
        }
        else
        {
            Instantiate(_LaserPreFab, transform.position + new Vector3(0, 1f, 0), Quaternion.identity);
            _LaserAudio.Play();
        }

    }

    public void Damage()
    {
        if (_ShieldIsActive == true)
        {
            _ShieldIsActive = false;
            _ShieldPowerUp.SetActive(false);
            return;
        }
        else
        {
            _Health--;
            if (_Health == 2)
            {
                _Engineleft.SetActive(true);
            }
            else if (_Health == 1)
            {
                _EngineRight.SetActive(true);
            }

            uiManager.LivesDisplayUpDate(_Health);

            if (_Health < 1)
            {
                _spawnmanager.OnPlayerdeath();
                uiManager.GameOverAction();
                Destroy(this.gameObject);
            }
        }
    }

    public void PowerUpCollected()
    {
        _TrippleShotActive = true;
        StartCoroutine("PowerUpControl");
    }

    IEnumerator PowerUpControl()
    {
        yield return new WaitForSeconds(5f);
        _TrippleShotActive = false;
    }

    public void SpeedBoost()
    {
        _SpeedBoostActive = true;
    }

    IEnumerator SpeedBoostControl()
    {
        yield return new WaitForSeconds(5f);
        _Speed = 8;
        _SpeedBoostActive = false;
    }

    public void ShieldPowerUp()
    {
        _ShieldIsActive = true;
        _ShieldPowerUp.SetActive(true);
    }

    public void AddScore()
    {
        _score += 10;
        uiManager.UpdateScore(_score);
    }
}
