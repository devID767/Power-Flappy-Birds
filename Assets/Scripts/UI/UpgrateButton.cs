using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgrateButton : MonoBehaviour
{
    [SerializeField] private Button _self;

    [SerializeField] private Text _cost;
    [SerializeField] private Text _lvl;
    private Bird_Item _currentItem;

    public event Action Upgraded;

    private void OnEnable()
    {
        Coins.ChangedCount += Coins_ChangedCount;
    }

    private void Coins_ChangedCount(int obj)
    {
        SetCurrentItem(_currentItem);
    }

    public void SetCurrentItem(Bird_Item bird_Item)
    {
        _currentItem = bird_Item;
        _cost.text = _currentItem.Upgrade[_currentItem.Level].ToString();
        _lvl.text = (_currentItem.Level+2).ToString();

        if (Coins.count < _currentItem.Upgrade[_currentItem.Level])
        {
            _self.interactable = false;
        }
        else
        {
            _self.interactable = true;
        }
    }
    public void OnUgradeButtonClicked()
    {
        if (Coins.AddPoints(-_currentItem.Upgrade[_currentItem.Level]))
        {
            LevelsOfBirds.IncreaseLevel(_currentItem);
            Upgraded?.Invoke();
        }
    }

    private void OnDisable()
    {
        Coins.ChangedCount -= Coins_ChangedCount;
    }
}
