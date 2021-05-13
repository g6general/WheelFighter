using UnityEngine;
using sp;

public class Level : MonoBehaviour
{
    public AudioSource Audio;

    void Start()
    {
        Audio = GetComponent<AudioSource>();
    }

    void OnGUI()
    {
        if (CheatsPanel.NeedShowPanel)
        {
            CheatsPanel.CheatPanel();

            var buttonNextRect = new Rect(CheatsPanel.button11PosX, CheatsPanel.button11PosY + CheatsPanel.Gap1Y,
                CheatsPanel.ButtonWidth, CheatsPanel.ButtonHeight);
            if (GUI.Button(buttonNextRect, "Next Block"))
            {
                ++Profile.Settings.CurrentBlock;

                if (Profile.Settings.CurrentBlock == LevelPlayground.NumberOfBlocs)
                    Profile.Settings.CurrentBlock = 0;

                Profile.Settings.CurrentLevel = 0;

                CheatsPanel.ButtonClicked = true;

                GoOut();
            }

            var buttonPrevRect = new Rect(CheatsPanel.button21PosX, CheatsPanel.button21PosY + CheatsPanel.Gap1Y,
                CheatsPanel.ButtonWidth, CheatsPanel.ButtonHeight);
            if (GUI.Button(buttonPrevRect, "Prev Block"))
            {
                --Profile.Settings.CurrentBlock;

                if (Profile.Settings.CurrentBlock < 0)
                    Profile.Settings.CurrentBlock = 0;

                Profile.Settings.CurrentLevel = 0;

                CheatsPanel.ButtonClicked = true;

                GoOut();
            }

            var buttonWinRect = new Rect(CheatsPanel.button12PosX, CheatsPanel.button12PosY + CheatsPanel.Gap1Y,
                CheatsPanel.ButtonWidth, CheatsPanel.ButtonHeight);
            if (GUI.Button(buttonWinRect, "Win Level"))
            {
                CheatsPanel.ButtonClicked = true;

                Events.LaunchEvent(Events.Types.LevelWin, Scenes.ActiveScene);
            }

            if (CheatsPanel.DesingMode)
                GUI.color = Color.yellow;

            var buttonDesignRect = new Rect(CheatsPanel.button11PosX, CheatsPanel.button11PosY + CheatsPanel.Gap2Y,
                CheatsPanel.ButtonWidth, CheatsPanel.ButtonHeight);
            if (GUI.Button(buttonDesignRect, "Design Mode"))
            {
                CheatsPanel.DesingMode = !CheatsPanel.DesingMode;

                CheatsPanel.ButtonClicked = true;
            }

            if (CheatsPanel.DesingMode)
            {
                var buttonShowRect = new Rect(CheatsPanel.button12PosX, CheatsPanel.button12PosY + CheatsPanel.Gap2Y,
                    CheatsPanel.ButtonWidth, CheatsPanel.ButtonHeight);
                if (GUI.Button(buttonShowRect, "Show Level"))
                {
                    CheatsPanel.ButtonClicked = true;

                    Scenes.GoToScene(Scenes.Types.Level);
                }

                var buttonRecordRect = new Rect(CheatsPanel.button13PosX, CheatsPanel.button13PosY + CheatsPanel.Gap2Y,
                    CheatsPanel.ButtonWidth, CheatsPanel.ButtonHeight);
                if (GUI.Button(buttonRecordRect, "Record Level"))
                {
                    var levelConfig = Configs.Levels;

                    var editorData = GetComponent<LevelEditor>();

                    var levelData = new LevelData();
                    levelData.BarrierType = editorData.BarrierType;
                    levelData.WheelType   = editorData.WheelType;

                    levelData.WheelParamA = editorData.WheelParamA;
                    levelData.WheelParamB = editorData.WheelParamB;
                    levelData.WheelParamC = editorData.WheelParamC;

                    levelData.PrizePositions   = editorData.PrizePositions;
                    levelData.BarrierPositions = editorData.BarrierPositions;

                    var numberOfBlocks = levelConfig.Blocks.Count;

                    bool NeedAddBlock = (numberOfBlocks == 0) ||
                        (levelConfig.Blocks[numberOfBlocks - 1].Levels.Count >= LevelPlayground.NumberOfLevels);

                    var lastBlock = NeedAddBlock ? new BlocData() : levelConfig.Blocks[numberOfBlocks - 1];

                    if (NeedAddBlock)
                        levelConfig.Blocks.Add(lastBlock);

                    lastBlock.Levels.Add(levelData);

                    Configs.RewriteLevelsConfig(levelConfig);

                    CheatsPanel.ButtonClicked = true;
                }
            }
        }
    }

    public void GoOut()
    {
        Audio.Play();

        if (LevelPlayground.GetCurrentLevel() == LevelPlayground.NumberOfLevels)
            Invoke("GoToResult", Audio.clip.length / 3);
        else
            Invoke("RestartLevel", Audio.clip.length / 3);
    }

    private void GoToResult()
    {
        Scenes.GoToScene(Scenes.Types.Result);
    }

    private void RestartLevel()
    {
        Scenes.GoToScene(Scenes.Types.Level);
    }
}
