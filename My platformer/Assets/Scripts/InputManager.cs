using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class InputManager : MonoBehaviour
{
    public Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    public float GetHorizontalAxisInput()
    {
        return Input.GetAxisRaw("Horizontal");
    }

    public float GetVerticalAxisInput()
    {
        return Input.GetAxisRaw("Vertical");
    }

    public bool GetJumpInput()
    {
        return Input.GetButtonDown("Jump");
    }

    public bool GetDashInput()
    {
        return Input.GetKeyDown(KeyCode.Mouse1);
    }
}

