using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Bullet Counter UI")]
    [SerializeField] private TextMeshProUGUI _bossBulletCountText;
    [SerializeField] private TextMeshProUGUI _playerBulletCountText;

    private void OnEnable()
    {
        BulletManager.OnBossBulletCountChanged += UpdateBossBulletCount;
        BulletManager.OnPlayerBulletCountChanged += UpdatePlayerBulletCount;
    }

    private void OnDisable()
    {
        BulletManager.OnBossBulletCountChanged -= UpdateBossBulletCount;
        BulletManager.OnPlayerBulletCountChanged -= UpdatePlayerBulletCount;
    }

    private void Start()
    {
        UpdateBossBulletCount();
        UpdatePlayerBulletCount();
    }

    private void UpdateBossBulletCount()
    {
        if (_bossBulletCountText != null)
        {
            _bossBulletCountText.text = $"Boss Bullets: {BulletManager.BossBulletCount}";
        }
    }

    private void UpdatePlayerBulletCount()
    {
        if (_playerBulletCountText != null)
        {
            _playerBulletCountText.text = $"Player Bullets: {BulletManager.PlayerBulletCount}";
        }
    }
}