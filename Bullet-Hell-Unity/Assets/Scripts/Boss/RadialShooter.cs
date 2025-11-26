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

    [Header("Pattern 2 Movement")]
    [SerializeField] private float _pattern2MoveSpeed = 50f;
    [SerializeField] private float _pattern2MoveRange = 80f;

    [Header("Animation")]
    [SerializeField] private Animator _animator;
    [SerializeField] private string _idleAnimationName = "boss_frog_idle";
    [SerializeField] private string _pattern2AnimationName = "boss_frog_move";

    private Vector3 _initialPosition;
    private bool _isMoving = false;

    private void Start()
    {
        _initialPosition = transform.position;

        if (_animator == null)
        {
            _animator = GetComponent<Animator>();
        }

        StartCoroutine(ExecuteBossPhases());
    }

    private IEnumerator ExecuteBossPhases()
    {
        if (_pattern1 != null)
        {
            PlayAnimation(_idleAnimationName);
            yield return StartCoroutine(ExecutePattern(_pattern1, _patternDuration, false));
            yield return new WaitForSeconds(_transitionDelay);
        }

        if (_pattern2 != null)
        {
            PlayAnimation(_pattern2AnimationName);
            yield return StartCoroutine(ExecutePattern(_pattern2, _patternDuration, true));
            yield return new WaitForSeconds(_transitionDelay);
        }

        if (_pattern3 != null)
        {
            PlayAnimation(_idleAnimationName);
            yield return StartCoroutine(ExecutePattern(_pattern3, _patternDuration, false));
        }
    }

    private IEnumerator ExecutePattern(RadialShotPattern pattern, float duration, bool enableMovement)
    {
        float elapsed = 0f;
        int lap = 0;
        Vector2 aimDirection = transform.up;

        _isMoving = enableMovement;

        if (enableMovement)
        {
            StartCoroutine(HorizontalMovement(duration));
        }

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

        _isMoving = false;
    }

    private IEnumerator HorizontalMovement(float duration)
    {
        float elapsed = 0f;
        float moveDirection = 1f;
        Vector3 leftBound = _initialPosition + Vector3.left * _pattern2MoveRange;
        Vector3 rightBound = _initialPosition + Vector3.right * _pattern2MoveRange;

        while (elapsed < duration && _isMoving)
        {
            Vector3 currentPos = transform.position;
            float newX = currentPos.x + (moveDirection * _pattern2MoveSpeed * Time.deltaTime);

            if (newX >= rightBound.x)
            {
                newX = rightBound.x;
                moveDirection = -1f;
            }
            else if (newX <= leftBound.x)
            {
                newX = leftBound.x;
                moveDirection = 1f;
            }

            Vector3 newPosition = new Vector3(newX, currentPos.y, currentPos.z);
            transform.position = newPosition;

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = _initialPosition;
    }

    private void PlayAnimation(string animationName)
    {
        if (_animator != null && !string.IsNullOrEmpty(animationName))
        {
            _animator.Play(animationName, 0, 0f);
        }
    }
}