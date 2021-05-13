using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace sp
{
    public static class Configs
    {
        public enum Types
        {
            Levels,
            Balance,
            StringsEn,
            StringsRu,
            Unknown
        }

        private static bool mIsInitialized = false;

        private static LevelsConfig mLevelsConfig;
        private static StringsConfig mStringsConfig;
        private static BalanceConfig mBalanceConfig;

        public static LevelsConfig Levels { get { return mLevelsConfig; } }
        public static StringsConfig Strings { get { return mStringsConfig; } }
        public static BalanceConfig Balance { get { return mBalanceConfig; } }

        public static void InitManager()
        {
            if (!mIsInitialized)
            {
                LoadConfigs();

                mIsInitialized = true;

                Debug.Log("Configs manager loaded");
            }
        }

        public static void ResetManager()
        {
            mIsInitialized = false;
        }

        public static void LoadConfigs()
        {
            mLevelsConfig  = LoadLevelsConfig();
            mBalanceConfig = LoadBalanceConfig();
            mStringsConfig = LoadStringsConfig();
        }

        private static LevelsConfig LoadLevelsConfig()
        {
            var configName = Path.ChangeExtension(Types.Levels.ToString(), "json");
            var configPath = Path.Combine(Application.streamingAssetsPath, configName);

            if (!File.Exists(configPath))
                Debug.LogError(String.Format("Can't find config \"{0}\"", configName));

            var body = File.ReadAllText(configPath);
            Debug.Log(String.Format("Config \"{0}\" loaded", configName));

            return JsonUtility.FromJson<LevelsConfig>(body);
        }

        public static void RewriteLevelsConfig(LevelsConfig levelsConfig)
        {
            var configName = Path.ChangeExtension(Types.Levels.ToString(), "json");
            var configPath = Path.Combine(Application.streamingAssetsPath, configName);
            var jsonString = JsonUtility.ToJson(levelsConfig, true);
            File.WriteAllText(configPath, jsonString);
        }

        private static BalanceConfig LoadBalanceConfig()
        {
            var configName = Path.ChangeExtension(Types.Balance.ToString(), "json");
            var configPath = Path.Combine(Application.streamingAssetsPath, configName);

            if (!File.Exists(configPath))
                Debug.LogError(String.Format("Can't find config \"{0}\"", configName));

            var body = File.ReadAllText(configPath);
            Debug.Log(String.Format("Config \"{0}\" loaded", configName));

            return JsonUtility.FromJson<BalanceConfig>(body);
        }

        private static StringsConfig LoadStringsConfig()
        {
            Types currentLanguage = Types.Unknown;

            if (Application.systemLanguage == SystemLanguage.Russian)
                currentLanguage = Types.StringsRu;
            else
                currentLanguage = Types.StringsEn;

            var configName = Path.ChangeExtension(currentLanguage.ToString(), "json");
            var configPath = Path.Combine(Application.streamingAssetsPath, configName);

            if (!File.Exists(configPath))
                Debug.LogError(String.Format("Can't find config \"{0}\"", configName));

            var body = File.ReadAllText(configPath);
            Debug.Log(String.Format("Config \"{0}\" loaded", configName));

            return JsonUtility.FromJson<StringsConfig>(body);
        }
    }

    [Serializable]
    public class LevelData
    {
        public LevelPlayground.Bullets BarrierType;
        public LevelPlayground.Wheels WheelType;

        public float WheelParamA;
        public float WheelParamB;
        public float WheelParamC;

        public List<float> PrizePositions;
        public List<float> BarrierPositions;
    }

    [Serializable]
    public class BlocData
    {
        public List<LevelData> Levels;

        public BlocData()
        {
            Levels = new List<LevelData>();
        }
    }

    [Serializable]
    public class LevelsConfig
    {
        public List<BlocData> Blocks;

        public LevelsConfig()
        {
            Blocks = new List<BlocData>();
        }
    }

    [Serializable]
    public class StringData
    {
        public string Key;
        public string Value;
    }

    [Serializable]
    public class StringsConfig
    {
        public List<StringData> Strings;

        public string GetString(string key)
        {
            var result = Strings.Find(x => x.Key == key);
            if (result == null)
            {
                Debug.LogError(String.Format("Can't find string (key) \"{0}\"", key));
                return "[ph]";
            }

            return result.Value;
        }
    }

    [Serializable]
    public class BalanceConfig
    {
        public int LevelReward;
        public int PaymentForContinue;
        public int Cost11;
        public int Cost12;
        public int Cost21;
        public int Cost22;
    }
}
