using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text bestLap;
    [SerializeField] private TMP_Text bestScore;
    // Start is called before the first frame update
    void Awake()
    {
            bestLap.text = GameSaveManager.Instance.Load(ScoreType.BestLap,0);
            bestScore.text = GameSaveManager.Instance.Load(ScoreType.HighScore, 0);
    }
}
