using UnityEngine;
using System.Collections;

public class RadialShooter : MonoBehaviour
{
    [Header("Pattern Configuration")]
    [SerializeField] private RadialShotPattern _pattern1;
    [SerializeField] private RadialShotPattern _pattern2;
    [SerializeField] private RadialShotPattern _pattern3;
    
    [Header("Timing")]
    [SerializeField] private float _patternDuration = 10f;
    [SerializeField] private float _transitionDelay = 0.5f;

    private void Start()
    {
        StartCoroutine(ExecuteBossPhases());
    }

    private IEnumerator ExecuteBossPhases()
    {
        if (_pattern1 != null)
        {
            yield return StartCoroutine(ExecutePattern(_pattern1, _patternDuration));
            yield return new WaitForSeconds(_transitionDelay);
        }

        if (_pattern2 != null)
        {
            yield return StartCoroutine(ExecutePattern(_pattern2, _patternDuration));
            yield return new WaitForSeconds(_transitionDelay);
        }

        if (_pattern3 != null)
        {
            yield return StartCoroutine(ExecutePattern(_pattern3, _patternDuration));
        }
    }

    private IEnumerator ExecutePattern(RadialShotPattern pattern, float duration)
    {
        float elapsed = 0f;
        int lap = 0;
        Vector2 aimDirection = transform.up;

        yield return new WaitForSeconds(pattern.StartWait);

        while (elapsed < duration && lap < pattern.Repetitions)
        {
            if (lap > 0 && pattern.AngleOffsetBeetweenReps != 0f)
            {
                aimDirection = aimDirection.Rotate(pattern.AngleOffsetBeetweenReps);
            }

            for (int i = 0; i < pattern.PatternSettings.Length; i++)
            {
                Vector2 currentPosition = transform.position;
                ShotAttack.RadialShot(currentPosition, aimDirection, pattern.PatternSettings[i]);
                
                float waitTime = pattern.PatternSettings[i].CooldownAfterShot;
                yield return new WaitForSeconds(waitTime);
                elapsed += waitTime;

                if (elapsed >= duration) break;
            }

            yield return new WaitForSeconds(pattern.EndWait);
            elapsed += pattern.EndWait;

            lap++;
        }
    }
}