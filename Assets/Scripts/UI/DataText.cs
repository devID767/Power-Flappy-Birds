using UnityEngine;
using UnityEngine.UI;

public class DataText : MonoBehaviour
{
    private Text _dataText;

    public PointsType pointsType;

    void Start()
    {
        _dataText = GetComponent<Text>();
        Coins.ChangedCount += Coins_ChangedCount;
        if (pointsType == PointsType.Score)
        {
            Display(Score.Record);
        }
        else if (pointsType == PointsType.Coins)
        {
            Display(Coins.count);
        }
    }

    private void Coins_ChangedCount(int obj)
    {
        if (pointsType == PointsType.Coins)
        {
            Display(Coins.count);
        }
    }

    private void Display(int points)
    {
        _dataText.text = points.ToString();
    }

    private void OnDestroy()
    {
        Coins.ChangedCount -= Coins_ChangedCount;
    }
}
