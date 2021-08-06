using System;
using UnityEngine;

public static class Settings
{
    private static int _slideControl = 0;
    public static bool SlideControl
    {
        get
        {
            Load();
            if (_slideControl == 1)
                return true;
            else
                return false;
        }
        private set
        {
            if(value == true)
            {
                _slideControl = 1;
            }
            else
            {
                _slideControl = 0;
            }
        }
    }

    private static int _musicOn = 1;
    public static bool MusicOn
    {
        get
        {
            Load();
            if (_musicOn == 1)
                return true;
            else
                return false;
        }
        private set
        {
            if (value == true)
            {
                _musicOn = 1;
            }
            else
            {
                _musicOn = 0;
            }
        }
    }

    public static event Action<bool> ChangedSlideControl;
    public static event Action<bool> ChangedMusicOn;

    public static void ChangeSlideControl(bool status)
    {
        Load();

        SlideControl = status;

        ChangedSlideControl?.Invoke(status);

        Save();
    }
    public static void ChangeMusicStatus(bool status)
    {
        Load();

        MusicOn = status;

        ChangedMusicOn?.Invoke(status);

        Save();
    }

    public static void Save()
    {
        PlayerPrefs.SetInt("SlideControl", _slideControl);
        PlayerPrefs.SetInt("Music", _musicOn);
    }

    public static void Load()
    {
        if (PlayerPrefs.HasKey("Music"))
        {
            _musicOn = PlayerPrefs.GetInt("Music");
        }
        if (PlayerPrefs.HasKey("SlideControl"))
        {
            _slideControl = PlayerPrefs.GetInt("SlideControl");
        }
    }

    public static void Reset()
    {
        PlayerPrefs.DeleteAll();

        _musicOn = 1;
        _slideControl = 1;
    }
}
