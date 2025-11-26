using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private int _maxHealth = 4;
    [SerializeField] private float _invulnerabilityDuration = 1.5f;

    [Header("Visual Feedback")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _hitColor = Color.red;
    [SerializeField] private float _hitFlashDuration = 0.1f;
    [SerializeField] private float _invulnerabilityBlinkSpeed = 0.15f;

    private int _currentHealth;
    private bool _isInvulnerable = false;
    private Color _originalColor;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;
    public bool IsInvulnerable => _isInvulnerable;

    private void Start()
    {
        _currentHealth = _maxHealth;

        if (_spriteRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (_spriteRenderer != null)
        {
            _originalColor = _spriteRenderer.color;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isInvulnerable) return;

        if (collision.CompareTag("BossBullet"))
        {
            TakeDamage();
            collision.gameObject.SetActive(false);
        }
    }

    public void TakeDamage()
    {
        if (_isInvulnerable) return;

        _currentHealth--;
        Debug.Log($"Player hit! Health: {_currentHealth}/{_maxHealth}");

        if (_currentHealth <= 0)
        {
            Die();
        }
        else
        {
            StartCoroutine(InvulnerabilityCoroutine());
        }
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        _isInvulnerable = true;

        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = _hitColor;
            yield return new WaitForSeconds(_hitFlashDuration);
        }

        float elapsed = 0f;
        while (elapsed < _invulnerabilityDuration)
        {
            if (_spriteRenderer != null)
            {
                _spriteRenderer.enabled = !_spriteRenderer.enabled;
            }

            yield return new WaitForSeconds(_invulnerabilityBlinkSpeed);
            elapsed += _invulnerabilityBlinkSpeed;
        }

        if (_spriteRenderer != null)
        {
            _spriteRenderer.enabled = true;
            _spriteRenderer.color = _originalColor;
        }

        _isInvulnerable = false;
    }

    private void Die()
    {
        Debug.Log("Player died!");
        gameObject.SetActive(false);
    }

    public void Heal(int amount)
    {
        _currentHealth = Mathf.Min(_currentHealth + amount, _maxHealth);
    }
}