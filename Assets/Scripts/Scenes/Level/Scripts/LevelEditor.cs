using System.Collections.Generic;
using UnityEngine;

public class LevelEditor : MonoBehaviour
{
    public LevelPlayground.Wheels WheelType;

    public float WheelParamA;
    public float WheelParamB;
    public float WheelParamC;

    public LevelPlayground.Bullets BarrierType;

    public List<float> BarrierPositions;
    public List<float> PrizePositions;
}
