using TMPro;
using UnityEngine;

public class GemsView : ItemsView
{
    [SerializeField] private TMP_Text _text;
    private int _maxValue;
    private int _currentValue;
    
    public override void SetValues(int currentItems, int maxItems)
    {
        _maxValue = maxItems;
        _currentValue = currentItems;
        _text.text = $"{_currentValue}/{_maxValue}";
    }

    public override void UpdateValues()
    {
        _text.text = $"{++_currentValue}/{_maxValue}";
    }
}
