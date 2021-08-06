using UnityEngine;

public class CharactersOfBird : MonoBehaviour
{
    [SerializeField] private Characters Red;
    [SerializeField] private Characters Yellow;
    [SerializeField] private Characters Blue;
    [SerializeField] private Characters Gray;

    public void Show(Bird_Item bird)
    {
        DeactiveAll();
        switch (bird.TypeOfBird)
        {
            case TypeOfBird.Red:
                Red.gameObject.SetActive(true);
                Red.ShowPowerCoolDown(bird);
                break;
            case TypeOfBird.Yellow:
                Yellow.gameObject.SetActive(true);
                Yellow.ShowPowerCoolDown(bird);
                break;
            case TypeOfBird.Blue:
                Blue.gameObject.SetActive(true);
                Blue.ShowPowerCoolDown(bird);
                break;
            case TypeOfBird.Gray:
                Gray.gameObject.SetActive(true);
                Gray.ShowPowerCoolDown(bird);
                break;
        }
    }

    private void DeactiveAll()
    {
        Red.gameObject.SetActive(false);
        Yellow.gameObject.SetActive(false);
        Blue.gameObject.SetActive(false);
        Gray.gameObject.SetActive(false);
    }
}
