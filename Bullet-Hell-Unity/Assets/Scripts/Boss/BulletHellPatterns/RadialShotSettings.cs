using UnityEngine;

[System.Serializable]
public class RadialShotSettings
{
    [Header("Base Settings")]
    public int NumberOfBullets;
    public float BulletSpeed = 10f;
    public float CooldownAfterShot;

    [Header("Spawn Settings")]
    [Tooltip("Radio desde el centro donde se generan las balas. Mantiene la forma del patrón.")]
    public float SpawnRadius = 0f;

    [Header("Offsets")]
    [Range(-1f, 1f)] public float PhaseOffset = 0f;
    [Range(-180f, 180f)] public float AngleOffset = 0f;

    [Header("Pattern Modifiers")]
    public bool MirrorPattern = false;
    public bool RadialMask = false;
    [Range(0f, 360f)] public float MaskAngle = 360f;
}