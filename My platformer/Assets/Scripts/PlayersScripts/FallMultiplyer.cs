using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class FallMultiplyer : MonoBehaviour
{
    private float _fallMultiplier = 7f;
    private float _lowJumpFallMultiplier = 2f;
    private InputReader _inputReader;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
    }

    public void FallMultiply(Rigidbody2D rigidbody)
    {
        if (rigidbody.velocity.y < 0)
        {
            rigidbody.gravityScale = _fallMultiplier;
        }
        else if (rigidbody.velocity.y > 0 && _inputReader.GetJumpButton() == false)
        {
            rigidbody.gravityScale = _lowJumpFallMultiplier;
        }
        else
        {
            rigidbody.gravityScale = 1f;
        }
    }
}
