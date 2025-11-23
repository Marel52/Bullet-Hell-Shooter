using UnityEngine;


[CreateAssetMenu(menuName = "BulletHell System/Radial Shot Parttern")]
public class RadialShotPattern : ScriptableObject
{
    public int Repetitions;
    [Range(-180f, 180)] public float AngleOffsetBeetweenReps = 0f;
    public float StartWait = 0f;
    public float EndWait = 0f;
    public RadialShotSettings[] PatternSettings;
}
