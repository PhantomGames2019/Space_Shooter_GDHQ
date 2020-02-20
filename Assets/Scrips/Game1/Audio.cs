using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    [SerializeField]
    private AudioSource _PowerUpAudio;


    public void PowerUpAudio()
    {
        _PowerUpAudio.Play();
    }

}
