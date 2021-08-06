using UnityEngine;
using UnityEngine.UI;

public class PointsText : MonoBehaviour
{
    private Text pointsText;

    public PointsType pointsType;

    private void OnEnable()
    {
        if (pointsType == PointsType.Score)
        {
            Score.Changed += Points_Changed;
        }
        else if (pointsType == PointsType.Coins)
        {
            Coins.Increased += CoindsIncreased;
        }
    }

    void Start()
    {
        pointsText = GetComponent<Text>();

        if (pointsType == PointsType.Score)
        {
            Display(Score.count);
        }
        else if (pointsType == PointsType.Coins)
        {
            Display(Coins.EarnByRound);
        }
    }

    void Display(int points)
    {
        pointsText.text = points.ToString();
    }

    void Points_Changed(int points)
    {
        Display(points);
    }
    void CoindsIncreased(int points)
    {
        Display(points);
    }

    private void OnDisable()
    {
        if (pointsType == PointsType.Score)
        {
            Score.Changed -= Points_Changed;
        }
        else if (pointsType == PointsType.Coins)
        {
            Coins.Increased -= CoindsIncreased;
        }
    }
}
