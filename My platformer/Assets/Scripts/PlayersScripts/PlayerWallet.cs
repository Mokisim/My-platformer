using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField] PlayerCollisionHandler _playerCollisionHandler;
    private int _balance = 0;

    private void OnEnable()
    {
        _playerCollisionHandler.CristalPicked += AddBalance;
    }

    private void OnDisable()
    {
        _playerCollisionHandler.CristalPicked -= AddBalance;
    }

    private void AddBalance()
    {
        _balance++;
    }
}
