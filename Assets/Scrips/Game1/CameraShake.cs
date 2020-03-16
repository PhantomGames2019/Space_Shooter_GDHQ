using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private Animator _CamShakeAnim;

    private void Start()
    {
        _CamShakeAnim = GetComponent<Animator>();
        if (_CamShakeAnim == null)
        {
            Debug.LogError("_CamShakeAnim is null inside CameraShake Class");
        }
    }

    public void CamShake()
    {
        _CamShakeAnim.SetTrigger("CameraShake");
    }


}
