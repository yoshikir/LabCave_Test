using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum ScoreType
{
    BestLap,HighScore
}
public class GameSaveManager : MonoBehaviour
{
    [Tooltip("User save states.")]
    [SerializeField] private List<GameScoreScriptable> userSave;

    #region Singleton
    public static GameSaveManager Instance;

    private void Awake() {
        if(Instance == null) {
            Instance = this;
        }else if(Instance != this) {
            Destroy(gameObject);
        }
    }
    #endregion

    public List<GameScoreScriptable> GetUserSave() {
        if (Instance == null) {
            Debug.LogWarning("GameSaveManager not found in the scene.");
            return null;
        }
        return Instance.userSave;
    }

    public void SaveState(ScoreType scoreType, string value) {
        if (userSave.Count < 1) return;
        switch (scoreType) {
            case ScoreType.BestLap:
                if (userSave[0] != null) userSave[0].bestTime = value;
                break;
            case ScoreType.HighScore:
                if (userSave[0] != null) userSave[0].highScore = value;
                break;
        }
    }

    public void SaveUserState(ScoreType scoreType, string value, int user) {
        if (userSave.Count < user) return;
        switch (scoreType) {
            case ScoreType.BestLap:
                if (userSave[user] != null) userSave[user].bestTime = value;
                break;
            case ScoreType.HighScore:
                if (userSave[user] != null) userSave[user].highScore = value;
                break;
        }
    }

    public string Load(ScoreType scoreType, int user) {
        List<GameScoreScriptable> saveData = GetUserSave();
        if (saveData == null) return "";
        switch (scoreType) {
            case ScoreType.BestLap:
                return saveData[user].bestTime;
            case ScoreType.HighScore:
                return saveData[user].highScore;
            default:
                return "";
        }
    }
}
