using UnityEngine;

public class BuyOrSelect : MonoBehaviour
{
    [SerializeField] private BuyButton _buyButton;
    [SerializeField] private GameObject _selectButton;
    [SerializeField] private UpgrateButton _upgradeButton;
    [SerializeField] private GameObject MaxLvl;


    private Bird_Item _currentItem;

    private void OnEnable()
    {
        _buyButton.Bought += _buyButton_Bought;
        _upgradeButton.Upgraded += _upgradeButton_Upgraded;
    }

    public void KnowOut(Bird_Item bird_Item)
    {
        _currentItem = bird_Item;
        if (!bird_Item.Bought)
        {
            OnBuyButton();
            _buyButton.SetCurrentItem(bird_Item);
        }
        else
        {
            OnSelectButton();
            OnUpgradeButton();
        }
    }

    public void OnBuyButton()
    {
        MaxLvl.SetActive(false);
        _selectButton.SetActive(false);
        _upgradeButton.gameObject.SetActive(false);
        _buyButton.gameObject.SetActive(true);
    }
    public void OnSelectButton()
    {
        MaxLvl.SetActive(false);
        _buyButton.gameObject.SetActive(false);
        _selectButton.SetActive(true);
        
    }
    public void OnUpgradeButton()
    {
        if ((int)LevelsOfBirds.dict[_currentItem.TypeOfBird] < 2)
        {
            _upgradeButton.gameObject.SetActive(true);
            _upgradeButton.SetCurrentItem(_currentItem);
        }
        else
        {
            _upgradeButton.gameObject.SetActive(false);
            MaxLvl.SetActive(true);
        }
        
    }

    private void _buyButton_Bought()
    {
        OnSelectButton();
        OnUpgradeButton();
    }
    private void _upgradeButton_Upgraded()
    {
        OnSelectButton();
        OnUpgradeButton();
    }

    private void OnDisable()
    {
        _buyButton.Bought -= _buyButton_Bought;
        _upgradeButton.Upgraded -= _upgradeButton_Upgraded;
    }
}
