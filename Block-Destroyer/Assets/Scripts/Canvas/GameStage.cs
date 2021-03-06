using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Robusta;

public static class GameStageEvent
{
    public delegate void Empty();
    public static event Empty StartLevel;
    public static event Empty FailLevel;
    public static void InvokeStartLevel()
    {
        StartLevel?.Invoke();
    }
}
public enum Stage { StartGame, StartLevel, WinGame, LostGame }
public class GameStage : MonoBehaviour
{
    public static GameStage Instance;

    private static bool _isGameStart;
    public static bool IsGameFlowe
    { get; private set; }

    [SerializeField]
    private CanvasManager _canvasManager;
    public Stage StageGame
    { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        ChangeStage(_isGameStart ? Stage.StartLevel : Stage.StartGame);
    }

    void Update()
    {

    }
    public void ChangeStage(Stage stage)
    {
        StageGame = stage;

        switch (stage)
        {
            case Stage.StartGame:
                _canvasManager.GameStageWindow(StageGame);
                _isGameStart = true;
                FacebookManager.Instance.GameStart();
                RobustaAnalytics.SetLevel(PlayerPrefs.GetInt("Level"));

                break;

            case Stage.StartLevel:

                _canvasManager.GameStageWindow(StageGame);
                IsGameFlowe = true;
                GameStageEvent.InvokeStartLevel();
                FacebookManager.Instance.LevelStart(PlayerPrefs.GetInt("Level"));

                break;

            case Stage.WinGame:
                if (IsGameFlowe)
                {
                    _canvasManager.GameStageWindow(StageGame);
                    FacebookManager.Instance.LevelWin(PlayerPrefs.GetInt("Level"));
                    RobustaAnalytics.SetLevel(PlayerPrefs.GetInt("Level"));

                    PlayerPrefs.SetInt("Scenes", PlayerPrefs.GetInt("Scenes") + 1);
                    PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);

                    IsGameFlowe = false;

                }
                break;

            case Stage.LostGame:
                if (IsGameFlowe)
                {
                    _canvasManager.GameStageWindow(StageGame);
                    FacebookManager.Instance.LevelFail(PlayerPrefs.GetInt("Level"));

                    IsGameFlowe = false;

                }
                break;
        }

    }
    public void LevelStart()
    {
        ChangeStage(Stage.StartLevel);
    }
}
