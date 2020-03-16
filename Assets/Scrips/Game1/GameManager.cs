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
        EscControl();
    }

    private void MouseControl()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void EscControl()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            MouseControl();
        }
    }
}
