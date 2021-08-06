using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private BuyOrSelect _buyOrSelect;
    [SerializeField] private BirdsChanger _birdsChanger;
    [SerializeField] private CharactersOfBird _charactersOfBird;

    public Bird_Item[] Bird_Items;

    private int _numOfCurrentBird;
    public TypeOfBird CurrentBird;

    public void OnShopButtonClicked()
    {
        foreach (var item in FindObjectsOfType<Bird>())
        {
            Destroy(item.gameObject);
        }

        _numOfCurrentBird = (int)CurrentBird;
        ShowBird();
    }

    public void ShowBird()
    {
        foreach (var item in Bird_Items)
        {
            item.gameObject.SetActive(false);
        }

        Bird_Items[_numOfCurrentBird].gameObject.SetActive(true);
        _charactersOfBird.Show(Bird_Items[_numOfCurrentBird]);
        _buyOrSelect.KnowOut(Bird_Items[_numOfCurrentBird]);
    }

    public void OnRightButtonClicked()
    {
        _numOfCurrentBird++;
        if (_numOfCurrentBird > Bird_Items.Length - 1)
        {
            _numOfCurrentBird = 0;
        }
        ShowBird();
    }
    public void OnLeftButtonClicked()
    {
        _numOfCurrentBird--;
        if (_numOfCurrentBird < 0)
        {
            _numOfCurrentBird = Bird_Items.Length - 1;
        }
        ShowBird();
    }

    public void OnSelectBirdCButtonlicked()
    {
        CurrentBird = Bird_Items[_numOfCurrentBird].TypeOfBird;
        _birdsChanger.CreateStartedBird(CurrentBird);
        SaveManager.instance.SaveGame();
    }

    public void OnMainMenuButtonClicked()
    {
        _birdsChanger.CreateStartedBird(CurrentBird);
    }

    public void OnGetMoneyButtonClicked(int count)
    {
        Coins.AddPoints(count);
    }
}
