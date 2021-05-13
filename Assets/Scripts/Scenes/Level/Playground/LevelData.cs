using System.Collections.Generic;
using UnityEngine;
using sp;

public class LevelPlayground : MonoBehaviour
{
    public enum Bullets
    {
        Fork,
        Pencil,
        Sword,
        Knife,
        Unknown
    }

    public enum Wheels
    {
        Earth,
        Mars,
        Planet1,
        Planet2,
        Planet3,
        Coin1,
        Coin2,
        Coin3,
        Coin4,
        Coin5,
        Baseball,
        Basketball,
        Bowling,
        Football,
        Tennis,
        Disk1,
        Disk2,
        Disk3,
        Disk4,
        Disk5,
        Unknown
    }

    public enum LevelState
    {
        WinLevel,
        LoseLevel,
        Unknown
    }

    public static int NumberOfLevels  = 4;
    public static int NumberOfBullets = 5;

    public static string PREFABS_WHEELS_PATH   = "Prefabs/Wheels/";
    public static string PREFABS_BULLETS_PATH  = "Prefabs/Bullets/";
    public static string PREFABS_BARRIERS_PATH = "Prefabs/Barriers/";
    public static string PREFABS_SCALES_PATH   = "Prefabs/Scales/";

    private static Bullets mCurrentBarrier;
    private static Wheels  mCurrentWheel;

    public static float WheelParamA;
    public static float WheelParamB;
    public static float WheelParamC;

    private static List<float> mPrizes;
    private static List<float> mBarriers;

    private static GameObject WheelPrefab;
    private static GameObject BulletPrefab;
    private static GameObject BulletScalePrefab;
    private static GameObject PrizePrefab;
    private static GameObject BarrierPrefab;
    private static GameObject PointPrefab;

    private static GameObject mWheel;

    public static int CurrentNumberOfBullets;

    public static List<GameObject> BulletsList;
    public static List<GameObject> BulletsScales;
    public static List<GameObject> Points;

    public static int ActiveBulletIndex = 0;

    public static int NumberOfBlocs = 0;

    private static LevelState mLevelState = LevelState.Unknown;

    public static GameObject ActiveBullet;

    public static void SetLevelState(LevelState state)
    {
        mLevelState = state;
    }

    public static LevelState GetLevelState()
    {
        return mLevelState;
    }

    public static void UpLevel()
    {
        ++Profile.Settings.CurrentLevel;
    }

    public static void Resetlevels()
    {
        Profile.Settings.CurrentLevel = 0;
    }

    public static int GetCurrentLevel()
    {
        return Profile.Settings.CurrentLevel;
    }

    public static void UpBloc()
    {
        ++Profile.Settings.CurrentBlock;

        if (Profile.Settings.CurrentBlock == NumberOfBlocs)
            Profile.Settings.CurrentBlock = 0;
    }

    public static int GetCurrentBloc()
    {
        return Profile.Settings.CurrentBlock;
    }

    public static void SetNumberOfBlocs(int number)
    {
        NumberOfBlocs = number;
    }

    public static void LoadLevels()
    {
        var levels = Configs.Levels;
        SetNumberOfBlocs(levels.Blocks.Count);

        if (CheatsPanel.DesingMode)
        {
            var editorData = GameObject.Find("Level").GetComponent<LevelEditor>();

            mCurrentBarrier = editorData.BarrierType;
            mCurrentWheel   = editorData.WheelType;

            WheelParamA = editorData.WheelParamA;
            WheelParamB = editorData.WheelParamB;
            WheelParamC = editorData.WheelParamC;

            mPrizes   = editorData.PrizePositions;
            mBarriers = editorData.BarrierPositions;

            return;
        }

        if (levels.Blocks.Count == 0)
        {
            mPrizes   = new List<float>();
            mBarriers = new List<float>();
            return;
        }

        var currentBlock = levels.Blocks[GetCurrentBloc()];
        bool IsBlockValid = false;

        for (var i = 0; i < levels.Blocks.Count; ++i)
        {
            if (currentBlock.Levels.Count == NumberOfLevels)
            {
                IsBlockValid = true;
                break;
            }

            UpBloc();
            currentBlock = levels.Blocks[GetCurrentBloc()];
        }

        if (!IsBlockValid)
        {
            Debug.LogError("Invalid level config data");

            mPrizes = new List<float>();
            mBarriers = new List<float>();
            return;
        }

        var currentLevel = currentBlock.Levels[GetCurrentLevel()];

        mCurrentBarrier = currentLevel.BarrierType;
        mCurrentWheel   = currentLevel.WheelType;

        WheelParamA = currentLevel.WheelParamA;
        WheelParamB = currentLevel.WheelParamB;
        WheelParamC = currentLevel.WheelParamC;

        mPrizes   = currentLevel.PrizePositions;
        mBarriers = currentLevel.BarrierPositions;

        Server.Report(Server.ReportComands.LevelLoaded);
    }

    public static void LoadPrefabs()
    {
        string wheelName;

        switch (mCurrentWheel)
        {
            case Wheels.Earth:
                wheelName = "Earth";
                break;

            case Wheels.Mars:
                wheelName = "Mars";
                break;

            case Wheels.Planet1:
                wheelName = "Planet1";
                break;

            case Wheels.Planet2:
                wheelName = "Planet2";
                break;

            case Wheels.Planet3:
                wheelName = "Planet3";
                break;

            case Wheels.Coin1:
                wheelName = "Coin1";
                break;

            case Wheels.Coin2:
                wheelName = "Coin2";
                break;

            case Wheels.Coin3:
                wheelName = "Coin3";
                break;

            case Wheels.Coin4:
                wheelName = "Coin4";
                break;

            case Wheels.Coin5:
                wheelName = "Coin5";
                break;

            case Wheels.Baseball:
                wheelName = "Baseball";
                break;

            case Wheels.Basketball:
                wheelName = "Basketball";
                break;

            case Wheels.Bowling:
                wheelName = "Bowling";
                break;

            case Wheels.Football:
                wheelName = "Football";
                break;

            case Wheels.Disk1:
                wheelName = "Disk1";
                break;

            case Wheels.Disk2:
                wheelName = "Disk2";
                break;

            case Wheels.Disk3:
                wheelName = "Disk3";
                break;

            case Wheels.Disk4:
                wheelName = "Disk4";
                break;

            case Wheels.Disk5:
                wheelName = "Disk5";
                break;

            case Wheels.Tennis:
                wheelName = "Tennis";
                break;

            default:
                wheelName = "";
                Debug.LogError("Can't find wheel type");
                break;
        }

        string bulletName;

        switch (Profile.Settings.CurrentBullet)
        {
            case Bullets.Fork:
                bulletName = "fork";
                break;

            case Bullets.Pencil:
                bulletName = "pencil";
                break;

            case Bullets.Sword:
                bulletName = "sword";
                break;

            case Bullets.Knife:
            default:
                bulletName = "knife";
                Debug.LogError("Can't find bullet type");
                break;
        }

        string barrierName;

        switch (mCurrentBarrier)
        {
            case Bullets.Fork:
                barrierName = "fork";
                break;
            case Bullets.Pencil:
                barrierName = "pencil";
                break;
            case Bullets.Sword:
                barrierName = "sword";
                break;
            case Bullets.Knife:
                barrierName = "knife";
                break;
            default:
                barrierName = "";
                Debug.LogError("Can't find barrier type");
                break;
        }

        WheelPrefab       = Resources.Load(PREFABS_WHEELS_PATH   + wheelName, typeof(GameObject)) as GameObject;
        BulletPrefab      = Resources.Load(PREFABS_BULLETS_PATH  + bulletName, typeof(GameObject)) as GameObject;
        BulletScalePrefab = Resources.Load(PREFABS_SCALES_PATH   + bulletName, typeof(GameObject)) as GameObject;
        BarrierPrefab     = Resources.Load(PREFABS_BARRIERS_PATH + barrierName, typeof(GameObject)) as GameObject;

        PrizePrefab = Resources.Load("Prefabs/prize", typeof(GameObject)) as GameObject;
        PointPrefab = Resources.Load("Prefabs/point", typeof(GameObject)) as GameObject;
    }

    public static void CreateWheel()
    {
        mWheel = Instantiate(WheelPrefab);
        mWheel.name = "Wheel";
    }

    public static void CreateBullets()
    {
        CurrentNumberOfBullets = NumberOfBullets;

        BulletsList   = new List<GameObject>();
        BulletsScales = new List<GameObject>();

        for (var i = 0; i < NumberOfBullets; ++i)
        {
            GameObject bullet = Instantiate(BulletPrefab);

            if (i != 0)
                bullet.SetActive(false);

            BulletsList.Add(bullet);

            bullet.name = "bullet";

            GameObject bulletScale = Instantiate(
                BulletScalePrefab,
                GameObject.Find("Canvas").transform);

            bulletScale.transform.Translate(0, i * 0.4f, 0);

            BulletsScales.Add(bulletScale);

            bulletScale.name = "bulletScale";
        }

        ActiveBullet = BulletsList[0];
    }

    public static void CreatePrizes()
    {
        GameObject prize = Instantiate(PrizePrefab);

        var wheelRadius = mWheel.GetComponent<CircleCollider2D>().radius;
        var prizeRadius = prize.GetComponent<CircleCollider2D>().radius;

        var radius = wheelRadius + prizeRadius;

        Destroy(prize);

        for (var i = 0; i < mPrizes.Count; ++i)
        {
            var angle = mPrizes[i] * 0.0174f;

            var wheelPosX = mWheel.transform.position.x;
            var wheelPosY = mWheel.transform.position.y;

            var current = Instantiate(
                PrizePrefab,
                new Vector3(radius * Mathf.Cos(angle) + wheelPosX, radius * Mathf.Sin(angle) + wheelPosY, PrizePrefab.transform.position.z),
                Quaternion.identity,
                mWheel.transform);

            current.name = "prize";
        }
    }

    public static void CreateBarriers()
    {
        GameObject barrier = Instantiate(BarrierPrefab);

        var wheelRadius = mWheel.GetComponent<CircleCollider2D>().radius;

        Destroy(barrier);

        for (var i = 0; i < mBarriers.Count; ++i)
        {
            var angle = mBarriers[i] * Mathf.Deg2Rad;

            var wheelPosX = mWheel.transform.position.x;
            var wheelPosY = mWheel.transform.position.y;

            var current = Instantiate(
                BarrierPrefab,
                new Vector3(wheelRadius * Mathf.Cos(angle) + wheelPosX, wheelRadius * Mathf.Sin(angle) + wheelPosY, BarrierPrefab.transform.position.z),
                Quaternion.Euler(0, 0, mBarriers[i] + 90),
                mWheel.transform);

            current.name = "barrier";
        }
    }

    public static void RenderPoints()
    {
        Points = new List<GameObject>();

        GameObject point = Instantiate(PointPrefab);
        var diameter = point.GetComponent<RectTransform>().rect.width;
        Destroy(point);

        for (var i = 0; i < NumberOfLevels; ++i)
        {
            var current = Instantiate(
                PointPrefab,
                new Vector3(2 * diameter * i - diameter * (NumberOfLevels - 1), 4.5f, 0),
                Quaternion.identity,
                GameObject.Find("Canvas").transform);

            current.name = "point";

            Points.Add(current);
        }

        UpdatePoints();
    }

    public static void OnLevelWin()
    {
        ActiveBulletIndex = 0;
        SetLevelState(LevelState.WinLevel);

        UpLevel();

        if (GetCurrentLevel() == NumberOfLevels)
        {
            UpdatePoints();

            Events.LaunchEvent(Events.Types.BlockWin, Scenes.ActiveScene);
            return;
        }

        var levelScript = GameObject.Find("Level").GetComponent<Level>();
        levelScript.GoOut();
    }

    public static void OnBlockWin()
    {
        UpBloc();

        var levelScript = GameObject.Find("Level").GetComponent<Level>();
        levelScript.GoOut();
    }

    public static void SetNextBulletAsActive()
    {
        ReduceBulletScale();

        if (ActiveBulletIndex == NumberOfBullets - 1)
        {
            OnWin();
            return;
        }

        ++ActiveBulletIndex;
        BulletsList[ActiveBulletIndex].SetActive(true);

        ActiveBullet = BulletsList[ActiveBulletIndex];
    }

    public static void ReduceBulletScale()
    {
        var bulletScale = BulletsScales[CurrentNumberOfBullets - 1];
        BulletsScales.RemoveAt(BulletsScales.Count - 1);
        Destroy(bulletScale);

        --CurrentNumberOfBullets;
    }

    private static void OnWin()
    {
        Events.LaunchEvent(Events.Types.LevelWin, Scenes.ActiveScene);
    }

    public static void UpdatePoints()
    {
        var number = GetCurrentLevel();

        for (var i = 0; i < number; ++i)
        {
            Points[i].GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
}