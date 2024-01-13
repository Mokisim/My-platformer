using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string _jumpInput = "Jump";
    private const string _horizontalAxis = "Horizontal";
    private const string _verticalAxis = "Vertical";

    public Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw(_horizontalAxis), Input.GetAxisRaw(_verticalAxis));
    }

    public float GetHorizontalAxisInput()
    {
        return Input.GetAxisRaw(_horizontalAxis);
    }

    public float GetVerticalAxisInput()
    {
        return Input.GetAxisRaw(_verticalAxis);
    }

    public bool GetJumpInput()
    {
        return Input.GetButtonDown(_jumpInput);
    }

    public bool GetDashInput()
    {
        return Input.GetKeyDown(KeyCode.Mouse1);
    }
}

