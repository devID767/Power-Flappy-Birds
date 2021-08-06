using System;
using System.Collections.Generic;

public class LevelsOfBirds
{
    public static Dictionary<TypeOfBird, int> dict = new Dictionary<TypeOfBird, int>()
        {
        {TypeOfBird.Red, 0},
        {TypeOfBird.Yellow, 0},
        {TypeOfBird.Blue, 0},
        {TypeOfBird.Gray, 0},
        };
    public static event Action<Bird_Item> Changed;

    public static void SetLevelOfBirds(Bird_Item[] bird_Items)
    {
        foreach (var item in bird_Items)
        {
            dict.Remove(item.TypeOfBird);
            dict.Add(item.TypeOfBird, item.Level);
        }
    }
    
    public static void IncreaseLevel(Bird_Item bird_item)
    {
        if ((int)dict[bird_item.TypeOfBird] < 2)
        {
            bird_item.Level++;
            dict.Remove(bird_item.TypeOfBird);
            dict.Add(bird_item.TypeOfBird, bird_item.Level);
            Changed(bird_item);
            SaveManager.instance.SaveGame();
        }
    }
}
