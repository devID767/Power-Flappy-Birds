using System;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    [SerializeField] private Button _self;

    [SerializeField] private Text _cost;
    private Bird_Item _currentItem;

    public event Action Bought;

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
        _cost.text = _currentItem.Cost.ToString();

        if(Coins.count < bird_Item.Cost)
        {
            _self.interactable = false;
        }
        else
        {
            _self.interactable = true;
        }
    }
    public void OnBuyButtonClicked()
    {
        if (Coins.AddPoints(-_currentItem.Cost))
        {
            _currentItem.Bought = true;
            Bought?.Invoke();
            SaveManager.instance.SaveGame();
        }
    }

    private void OnDisable()
    {
        Coins.ChangedCount -= Coins_ChangedCount;
    }
}
