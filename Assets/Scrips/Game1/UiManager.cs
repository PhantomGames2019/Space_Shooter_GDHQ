using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{

    [SerializeField]
    private Text _ScoreText;
    [SerializeField]
    private Sprite[] _CurrentSprite; 
    [SerializeField]
    private Image _CurrentImg;
    [SerializeField]
    private GameObject _GameOverTxt;
    [SerializeField]
    private float _FlashWaitTime;
    [SerializeField]
    private float _MissileFlashWaitTime;
    [SerializeField]
    private GameObject _Resetbutton;
    [SerializeField]
    private Text _Ammo;
    [SerializeField]
    private Sprite[] _ShieldSprites;
    [SerializeField]
    private Image _ShieldImg;
    [SerializeField]
    private GameObject _Shield_Img_Comp;
    private bool _StartFlashing = false;
   
    [SerializeField]
    private GameObject _MissileImg;

    [SerializeField]
    private GameObject _MissileText;

    //SerField for fill image;
    // Pvt Image _FillImage;
    [SerializeField]
    private Image _Boostfill;

    // serfield -- Pvt float _MaxBoost = 100f;
    [SerializeField]
    private float _MaxBoost = 100f;
    // serfield -- Pvt float -CurrentBoost;
    [SerializeField]
    private float _CurrentBoost;

    private Player _player;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("_Player is null inside the UiManager script");
        }
        // _currentBoost = _MaxBoost;
        _CurrentBoost = _MaxBoost;
    }
    
    private void Update()
    {
        BoostDrop();
        if (_Resetbutton.activeInHierarchy == true && Input.GetKeyDown(KeyCode.R) )
        {
            SceneManager.LoadScene(1);
        }
    }

    


    public void UpdateScore(int score)
    {
        _ScoreText.text = "Score:" + " " + score;
    }

    public void LivesDisplayUpDate(int CurrentLives)
    {
        _CurrentImg.sprite = _CurrentSprite[CurrentLives];
    }

    public void GameOverAction()
    {
        _GameOverTxt.SetActive(true);
        _Resetbutton.SetActive(true);
        StartCoroutine("FlashGameOver");
        
    }

    IEnumerator FlashGameOver()
    {
        while(true)
        {

            yield return new WaitForSeconds(_FlashWaitTime);
            _GameOverTxt.SetActive(false);
            yield return new WaitForSeconds(_FlashWaitTime);
            _GameOverTxt.SetActive(true);
        }

    }

    public void AmmoUpdate(int AmmoCount)
    {
        _Ammo.text = "Ammo:" + AmmoCount + "/15";
    }

    public void UpdateShieldSprite(int CurrentShieldHealth)
    {
       
        switch (CurrentShieldHealth)
        {
            case 0:
                _Shield_Img_Comp.SetActive(false);
               
                break;

            case 1:
                _ShieldImg.sprite = _ShieldSprites[0];

                break;

            case 2:
                _ShieldImg.sprite = _ShieldSprites[1];

                break;

            case 3:
                _ShieldImg.sprite = _ShieldSprites[2];

                break;

            default:
                print("Default");

                break;
        }
        
    }

    public void TurnShieldImgOn()
    {
        if (_Shield_Img_Comp == false)
        {
            _Shield_Img_Comp.SetActive(true);
           
        }
    }


    public void ShieldImgActive()
    {
        _Shield_Img_Comp.SetActive(true);
    }

    public void MissileImgFlashing()
    {
        StartCoroutine("MissileImgFlash");
        _MissileText.SetActive(true);
    }
    
    IEnumerator MissileImgFlash()
    {
        while (true)
        {
            yield return new WaitForSeconds(_MissileFlashWaitTime);
            _MissileImg.SetActive(true);
            yield return new WaitForSeconds(_MissileFlashWaitTime);
            _MissileImg.SetActive(false);
        }
    }

    public void MissileImgFlashStop()
    {
        StopCoroutine("MissileImgFlash");
        _MissileImg.SetActive(false);
        _MissileText.SetActive(false);
    }

  
    public void BoostDrop()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && _CurrentBoost == 100)
        {
            StartCoroutine("RepeatDropBoost");
            _player.IncreaseSpeed();
        }
       
        float CalBoost = _CurrentBoost / _MaxBoost;
        CommunicateWithFill(CalBoost);
    }

    private void CommunicateWithFill(float Boost)
    {
        _Boostfill.fillAmount = Boost;
    }

    IEnumerator RepeatDropBoost()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            _CurrentBoost -= 1f;
            if (_CurrentBoost == 0)
            {
                _player.DecreaseSpeed();
                yield return new WaitForSeconds(5f);
                StartCoroutine("RepeatRaiseBoost");
                StopCoroutine("RepeatDropBoost");
            }
        }
    }

    IEnumerator RepeatRaiseBoost()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            _CurrentBoost += 1;
            if (_CurrentBoost == 100)
            {
                StopCoroutine("RepeatRaiseBoost");
            }
           
        }
    }

}
