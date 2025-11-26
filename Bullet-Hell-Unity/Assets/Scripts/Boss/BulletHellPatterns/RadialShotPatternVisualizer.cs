using UnityEngine;

public class RadialShotPatternVisualizer : MonoBehaviour
{
    [SerializeField] private RadialShotPattern _pattern;
    [SerializeField] private float _radius = 0.1f;
    [SerializeField] private Color _color = Color.red;
    [SerializeField, Range(0f, 5f)] private float _testTime = 1f;

    private void OnDrawGizmos()
    {
        if (_pattern == null) return;

        Gizmos.color = _color;

        int lap = 0;
        Vector2 aimDirection = transform.up;
        Vector2 center = transform.position;
        float timer = _testTime;

        while (timer > 0f && lap < _pattern.Repetitions)
        {
            if (lap > 0 && _pattern.AngleOffsetBeetweenReps != 0f)
            {
                aimDirection = aimDirection.Rotate(_pattern.AngleOffsetBeetweenReps);
            }

            for (int i = 0; i < _pattern.PatternSettings.Length; i++)
            {
                if (timer < 0f) break;

                DrawRadialShot(_pattern.PatternSettings[i], timer, aimDirection, center);
                timer -= _pattern.PatternSettings[i].CooldownAfterShot;
            }

            lap++;
        }
    }

    private void DrawRadialShot(RadialShotSettings settings, float lifetime, Vector2 aimDirection, Vector2 center)
    {
        float angleBetweenBullets = 360f / settings.NumberOfBullets;

        if (settings.PhaseOffset != 0f || settings.AngleOffset != 0f)
        {
            aimDirection = aimDirection.Rotate((angleBetweenBullets * settings.PhaseOffset) + settings.AngleOffset);
        }

        for (int i = 0; i < settings.NumberOfBullets; i++)
        {
            float bulletDirectionAngle = angleBetweenBullets * i;
            if (settings.RadialMask && bulletDirectionAngle > settings.MaskAngle) break;

            Vector2 bulletDirection = aimDirection.Rotate(bulletDirectionAngle);
            
            Vector2 spawnPos = center + (bulletDirection * settings.SpawnRadius);
            Vector2 bulletPosition = spawnPos + (bulletDirection * settings.BulletSpeed * lifetime);

            Gizmos.DrawSphere(bulletPosition, _radius);
        }
    }
}