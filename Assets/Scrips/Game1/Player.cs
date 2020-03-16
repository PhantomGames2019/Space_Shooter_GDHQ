using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    [SerializeField]
    private float _Speed;
    [SerializeField]
    private float _SpeedUp;
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
    private CameraShake _CamShakeAnim;

    [SerializeField]
    private int _Ammo = 15;
   
    private int _FullAmmo = 15;

    [SerializeField]
    private int _ShieldLife = 3;

    [SerializeField]
    private GameObject _SuperPowerMissile;
    
    private bool _MissileIsActive = false;
    
    private bool _CanFire = true;

    

    void Start()
    {
       
        _CamShakeAnim = GameObject.Find("Main Camera").GetComponent<CameraShake>();
        if(_CamShakeAnim == null)
        {
            Debug.LogError("Animator for Cam_Shake is null");
        }

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
        UpdateAmmo();
        PlayerMovement();

        if (_MissileIsActive == true && _CanFire == true)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                uiManager.MissileImgFlashStop();
                
                _CanFire = false;
                Vector3 MissileTransform = new Vector3(Random.Range(-3f, 3f), -7, 0f);
                Instantiate(_SuperPowerMissile, MissileTransform, Quaternion.identity);

                StartCoroutine("SuperPowerCounter");
                
            }
        }
      
     
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _Canfire && _Ammo >= 1 && _CanFire == true)
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
            _Ammo -= 1;
           
            _LaserAudio.Play();
        }

    }

    public void AddHealth()
    {
        if (_Health == 3 )
        {
            return;
        }
        else if (_Health <3)
        {
            _Health += 1;
            uiManager.LivesDisplayUpDate(_Health);
        }
        if (_Health == 2)
        {
            _Engineleft.SetActive(false);
        }
        else if (_Health == 3)
        {
            _EngineRight.SetActive(false);
        }
    }
    
    public void Damage()
    {
        if (_ShieldIsActive == true)
        {
            _ShieldLife -= 1;

            if (_ShieldLife == 0)
            {
                uiManager.UpdateShieldSprite(_ShieldLife);
                _ShieldPowerUp.SetActive(false);
                _ShieldIsActive = false;
                _ShieldLife = 3;
            }
           
            uiManager.UpdateShieldSprite(_ShieldLife);
            return;
        }
        else
        {
            _Health--;

            if (_Health == 2)
            {
                _CamShakeAnim.CamShake();
                _EngineRight.SetActive(true);
            }
            else if (_Health == 1)
            {
                _CamShakeAnim.CamShake();
                _Engineleft.SetActive(true);
                
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
        uiManager.ShieldImgActive();
        _ShieldPowerUp.SetActive(true);
    }

    public void AddScore()
    {
        _score += 10;
        uiManager.UpdateScore(_score);
    }
    private void TestBoost()
    {
        uiManager.BoostDrop();
    }
   
    public void IncreaseSpeed()
    {
           _Speed = _SpeedUp;
    }
    public void DecreaseSpeed()
    {
        _Speed = 8;
    }

      

    private void UpdateAmmo()
    {
        uiManager.AmmoUpdate(_Ammo);
    }
   
    public void AmmoReload()
    {
        _Ammo = _FullAmmo;
    }

    public void SuperPowerMissile()
    {
       _MissileIsActive = true;
    }
    
    IEnumerator SuperPowerCounter()
    {
        yield return new WaitForSeconds(5f);
        _MissileIsActive = false;
        _CanFire = true;
    }
    
}
