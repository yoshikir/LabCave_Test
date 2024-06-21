using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectObject : TargetObject
{
    [Header("CrashObject")]
    [Tooltip("The VFX prefab spawned when the object is collected")]
    public ParticleSystem CollectVFX;

    [Tooltip("The mesh that will dissappear when the object is picked up.")]
    public GameObject deactivatedObject;

    [Tooltip("Check if you want this object to add to the highscore in the save state.")]
    [SerializeField] private bool saveScore;

    void Start() {
        Register();
    }

    void OnCollect(Collider other) {
        active = false;
        if (CollectVFX)
            StartCoroutine(PlayVFX());

        Objective.OnUnregisterPickup(this);

        if (saveScore) {
            int score = 0;
            if (!GameSaveManager.Instance.Load(ScoreType.HighScore, 0).Equals(""))
                score = int.Parse(GameSaveManager.Instance.Load(ScoreType.HighScore, 0));
            score += 1;
            GameSaveManager.Instance.SaveState(ScoreType.HighScore, score.ToString());
        }

        TimeManager.OnAdjustTime(TimeGained);
    }

    IEnumerator PlayVFX() {
        CollectVFX.Play();
        deactivatedObject.gameObject.SetActive(false);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) {
        if (!active) return;

        if ((layerMask.value & 1 << other.gameObject.layer) > 0 && other.gameObject.CompareTag("Player"))
            OnCollect(other);
    }
}
