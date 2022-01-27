using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    

    public enum GameState
    {
        Init,
        Running,
        Finished
    }

    public GameState State { get; private set; } = GameState.Init;

    private void Start()
    {
        playerLocationLog = GameObject.Find("Player").GetComponent<LocationLog>();
    }


    public delegate int GameStartedDelegate();

    public GameStartedDelegate OnGameStarted;

    
    public delegate void GameFinishedDelegate();

    public GameFinishedDelegate OnGameFinished;

    private int score = 0;
    private int maxScore = 0;
    private List<LocationLog> locationLogs = new List<LocationLog>();
    private LocationLog playerLocationLog;


    private void CheckEndGame()
    {
        if (score != maxScore) return;
        Debug.Log("Victoire !");
        State = GameState.Finished;
    }

    public void IncrementScore()
    {
        ++score;
        CheckEndGame();
    }
    public void DecrementScore()
    {
        --score;
    }
    

    public void StartGame()
    {
        maxScore = OnGameStarted();
        State = GameState.Running;
    }

    public void PushToLocationLogs(LocationLog moveBox)
    {
        locationLogs.Add(moveBox);
        playerLocationLog.SavePlayerLocation();
    }
    
    public void Undo()
    {
        if (State != GameState.Running) return;
        int lastIndex = locationLogs.Count - 1;
        if (lastIndex >= 0)
        {
            locationLogs[lastIndex].UndoPosition();
            locationLogs.RemoveAt(lastIndex);
        } 
        playerLocationLog.UndoPositionAndRotation();
    }
}