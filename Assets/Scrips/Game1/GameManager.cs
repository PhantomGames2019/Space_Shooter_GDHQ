using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            MouseControl();
        }
    }

    private void MouseControl()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

}
