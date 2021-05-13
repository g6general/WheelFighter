using System;
using System.Collections.Generic;
using UnityEngine;

namespace sp
{
    public static class Profile
    {
        private static bool mIsInitialized = false;

        public static PlayerSettings Settings { get; set; }

        public static PlayerData Data { get; set; }

        public static void InitManager()
        {
            if (!mIsInitialized)
            {
                Settings = new PlayerSettings();
                Data = new PlayerData();

                LoadSettings();
                LoadProfile();

                CheckParameters();

                mIsInitialized = true;

                Debug.Log("Profile manager loaded");
            }
        }

        public static void ResetManager()
        {
            mIsInitialized = false;
        }

        public static void LoadSettings()
        {
            Settings.NeedResetSettings = PlayerPrefs.HasKey("NeedResetSettings") ? 
                (PlayerPrefs.GetInt("NeedResetSettings") > 0 ? true : false) : false;

            if (Settings.NeedResetSettings)
            {
                ResetProfile();
                Settings.NeedResetSettings = false;
            }

            Settings.IsSoundOn = PlayerPrefs.HasKey("IsSoundOn") ? (PlayerPrefs.GetInt("IsSoundOn") > 0 ? true : false) : true;
            Settings.CurrentBullet = PlayerPrefs.HasKey("CurrentBullet") ? (LevelPlayground.Bullets)PlayerPrefs.GetInt("CurrentBullet") : LevelPlayground.Bullets.Unknown;
            Settings.CurrentLevel = PlayerPrefs.HasKey("CurrentLevel") ? PlayerPrefs.GetInt("CurrentLevel") : 0;
            Settings.CurrentBlock = PlayerPrefs.HasKey("CurrentBlock") ? PlayerPrefs.GetInt("CurrentBlock") : 0;

            Debug.Log("Settings loaded");
        }

        public static void SaveSettings()
        {
            PlayerPrefs.SetInt("IsSoundOn", Settings.IsSoundOn ? 1 : 0);
            PlayerPrefs.SetInt("CurrentBullet", (int)Settings.CurrentBullet);
            PlayerPrefs.SetInt("CurrentLevel", Settings.CurrentLevel);
            PlayerPrefs.SetInt("CurrentBlock", Settings.CurrentBlock);
            PlayerPrefs.SetInt("NeedResetSettings", Settings.NeedResetSettings ? 1 : 0);
            PlayerPrefs.Save();

            Debug.Log("Settings saved");
        }

        public static void LoadProfile()
        {
            if (PlayerPrefs.HasKey("Profile"))
            {
                var profileBody = PlayerPrefs.GetString("Profile");
                JsonUtility.FromJsonOverwrite(profileBody, Data);

                Debug.Log("Profile loaded");
            }
            else
            {
                Debug.LogError("Can't find profile");
            }
        }

        public static void SaveProfile()
        {
            var profileBody = JsonUtility.ToJson(Data);
            PlayerPrefs.SetString("Profile", profileBody);
            PlayerPrefs.Save();

            Debug.Log("Profile saved");
        }

        public static void ResetProfile()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log("Reset profile");
        }

        private static void CheckParameters()
        {
            var numberOfBlocs  = Configs.Levels.Blocks.Count;
            var numberOfLevels = 0;

            if (numberOfBlocs != 0)
                numberOfLevels = Configs.Levels.Blocks[0].Levels.Count;

            if (Settings.CurrentBlock >= numberOfBlocs)
                Settings.CurrentBlock = 0;

            if (Settings.CurrentLevel >= numberOfLevels)
                Settings.CurrentLevel = 0;
        }
    }

    public class PlayerSettings
    {
        public bool IsSoundOn { get; set; }
        public LevelPlayground.Bullets CurrentBullet { get; set; }
        public int CurrentLevel { get; set; }
        public int CurrentBlock { get; set; }
        public bool NeedResetSettings { get; set; }
    }

    [Serializable]
    public class PlayerData
    {
        public int Currency;
        public int Score;
        public List<int> Purchases;

        public PlayerData()
        {
            Currency = 0;
            Score = 0;

            Purchases = new List<int>();
            Purchases.Add(0);
            Purchases.Add(0);
            Purchases.Add(0);
            Purchases.Add(0);
        }
    }
}
