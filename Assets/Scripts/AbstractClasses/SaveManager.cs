using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public Shop Shop;
    public AbilitiesMenu AbilitiesMenu;

    private string _filePath;

    public static SaveManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(this != instance)
        {
            Destroy(this);
        }

        _filePath = Application.persistentDataPath + "data.gamesave";

        LoadGame();
        SaveGame();
    }

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(_filePath, FileMode.Create);

        Save save = new Save();

        save.Coins = Coins.count;
        save.Record = Score.Record;
        save.ActiveBird = (int)Shop.CurrentBird;

        save.SaveBoughtBirds(Shop.Bird_Items);
        save.SaveLevelOfBirds(LevelsOfBirds.dict);

        save.SaveLevelsOfAbilities(AbilitiesMenu.Abilities);

        bf.Serialize(fs, save);
        fs.Close();
    }

    public void LoadGame()
    {
        if (!File.Exists(_filePath))
            return;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(_filePath, FileMode.Open);

        Save save = (Save)bf.Deserialize(fs);

        Coins.TrySetCount(save.Coins);
        Score.TrySetRecord(save.Record);
        Shop.CurrentBird = (TypeOfBird)save.ActiveBird;

        foreach (var item in Shop.Bird_Items)
        {
            item.Bought = save.BoughtBirds[(int)item.TypeOfBird];
            item.Level = save.LevelsOfBirds[(int)item.TypeOfBird];
        }
        LevelsOfBirds.SetLevelOfBirds(Shop.Bird_Items);

        foreach (var ability in AbilitiesMenu.Abilities)
        {
            ability.Level = save.LevelsOfAbilities[(int)ability.EAbility];
        }

        fs.Close();
    }

    public void ResetGame()
    {
        File.Delete(_filePath);
    }
}

[System.Serializable]
public class Save
{
    public int Coins;
    public int Record;

    public int ActiveBird;

    public List<bool> BoughtBirds = new List<bool>();
    public List<int> LevelsOfBirds = new List<int>();
    public List<int> LevelsOfAbilities = new List<int>();

    public void SaveBoughtBirds(Bird_Item[] bird_Items)
    {
        foreach (var item in bird_Items)
        {
            BoughtBirds.Add(item.Bought);
        }
    }

    public void SaveLevelOfBirds(Dictionary<TypeOfBird, int> LevelsOfBirdsdict)
    {
        foreach (var item in LevelsOfBirdsdict)
        {
            LevelsOfBirds.Add((int)item.Value);
        }
    }

    public void SaveLevelsOfAbilities(Ability[] abilities)
    {
        foreach (var ability in abilities)
        {
            LevelsOfAbilities.Add(ability.Level);
        }
    }
}