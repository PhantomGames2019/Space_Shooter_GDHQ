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
    private float _GameOverFlash;
    [SerializeField]
    private GameObject _Resetbutton;



    private void Update()
    {
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
            yield return new WaitForSeconds(_GameOverFlash);
            _GameOverTxt.SetActive(false);
            yield return new WaitForSeconds(_GameOverFlash);
            _GameOverTxt.SetActive(true);
        }

    }


}
