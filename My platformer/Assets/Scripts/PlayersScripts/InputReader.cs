using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string JumpInput = "Jump";
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";

    public Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw(HorizontalAxis), Input.GetAxisRaw(VerticalAxis));
    }

    public float GetHorizontalAxisInput()
    {
        return Input.GetAxisRaw(HorizontalAxis);
    }

    public float GetVerticalAxisInput()
    {
        return Input.GetAxisRaw(VerticalAxis);
    }

    public bool GetJumpInput()
    {
        return Input.GetButtonDown(JumpInput);
    }

    public bool GetJumpButton()
    {
        return Input.GetButton(JumpInput);
    }

    public bool GetDashInput()
    {
        return Input.GetKeyDown(KeyCode.Mouse1);
    }

    public bool GetAttackInput()
    {
        return Input.GetKeyDown(KeyCode.Mouse0);
    }

    public bool GetVampirismInput()
    {
        return Input.GetKeyDown(KeyCode.Q);
    }

    public bool GetFireballInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }
}

