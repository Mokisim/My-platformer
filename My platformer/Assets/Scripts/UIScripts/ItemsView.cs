using UnityEngine;

public abstract class ItemsView : MonoBehaviour
{
    [SerializeField] private PlayerCollisionHandler _playerCollisionHandler;
    private int _itemsCount;
    private int _currentItems = 0;
    private Item[] _items;

    private void Start()
    {
        _items = FindObjectsOfType<Gem>();
        _itemsCount = _items.Length;
        SetValues(_currentItems, _itemsCount);
    }

    private void OnEnable()
    {
        _playerCollisionHandler.CristalPicked += UpdateValues;
    }

    private void OnDisable()
    {
        _playerCollisionHandler.CristalPicked -= UpdateValues;
    }

    public abstract void SetValues(int currentItems, int maxItems);

    public abstract void UpdateValues();
}
