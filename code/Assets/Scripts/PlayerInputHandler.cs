using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public static PlayerInputHandler Instance;
    public float lookSensitivity = 1f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //��겻�ɼ�
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public Vector3 GetMoveInput()
    {
        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        move = Vector3.ClampMagnitude(move, 1);
        return move;
    }

    public float GetMouseLookHorizontal()
    {
        return GetMouseLookAxis("Mouse X");
    }

    public float GetMouseLookVertical()
    {
        return GetMouseLookAxis("Mouse Y");
    }

    public float GetMouseLookAxis(string mouseInputName)
    {
        float i = Input.GetAxisRaw(mouseInputName);
        i *= lookSensitivity * 0.01f;
        return i;
    }

    public bool GetJumpInputDown()
    {
        return Input.GetButtonDown("Jump");
    }

    public bool GetFireInputHeld()
    {
        return Input.GetButton("Fire");
    }
}
