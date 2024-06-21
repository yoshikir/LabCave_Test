using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugMenuController : MonoBehaviour
{
    //Labels
    [SerializeField] TMP_Text LapsLabel;
    [SerializeField] TMP_Text SpeedLabel;
    [SerializeField] TMP_Text AccelerationLabel;
    [SerializeField] TMP_Text ReverseSpeedLabel;
    [SerializeField] TMP_Text ReverseAccelerationLabel;

    //Input Fields
    [SerializeField] TMP_InputField LapsField;
    [SerializeField] TMP_InputField SpeedField;
    [SerializeField] TMP_InputField AccelerationField;
    [SerializeField] TMP_InputField ReverseSpeedField;
    [SerializeField] TMP_InputField ReverseAccelerationField;

    private ArcadeKart kart;
    private ObjectiveCompleteLaps objectiveLaps;

    private void Start() {
        kart = FindObjectOfType<ArcadeKart>();
        objectiveLaps = FindObjectOfType<ObjectiveCompleteLaps>();

        LapsLabel.text = objectiveLaps.lapsToComplete.ToString();
        SpeedLabel.text = kart.baseStats.TopSpeed.ToString();
        AccelerationLabel.text = kart.baseStats.Acceleration.ToString();
        ReverseSpeedLabel.text = kart.baseStats.ReverseSpeed.ToString();
        ReverseAccelerationLabel.text = kart.baseStats.ReverseAcceleration.ToString();

        LapsField.text = objectiveLaps.lapsToComplete.ToString();
        SpeedField.text = kart.baseStats.TopSpeed.ToString();
        AccelerationField.text = kart.baseStats.Acceleration.ToString();
        ReverseSpeedField.text = kart.baseStats.ReverseSpeed.ToString();
        ReverseAccelerationField.text = kart.baseStats.ReverseAcceleration.ToString();
    }

    public void UpdateValue() {
        LapsLabel.text = LapsField.text;
        objectiveLaps.lapsToComplete = int.Parse(LapsLabel.text);
        objectiveLaps.UpdateObjective("","","");
        SpeedLabel.text = SpeedField.text;
        kart.baseStats.TopSpeed = int.Parse(SpeedLabel.text);
        AccelerationLabel.text = AccelerationField.text;
        kart.baseStats.Acceleration = int.Parse(AccelerationLabel.text);
        ReverseSpeedLabel.text = ReverseSpeedField.text;
        kart.baseStats.ReverseSpeed = int.Parse(ReverseSpeedLabel.text);
        ReverseAccelerationLabel.text = ReverseAccelerationField.text;
        kart.baseStats.ReverseAcceleration = int.Parse(ReverseAccelerationLabel.text);
    }
}
