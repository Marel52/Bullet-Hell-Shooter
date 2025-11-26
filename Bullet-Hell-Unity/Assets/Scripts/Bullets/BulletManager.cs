using UnityEngine;
using System;

public class BulletManager : MonoBehaviour
{
    public static BulletManager Instance;

    public static Action OnBossBulletCountChanged;
    public static Action OnPlayerBulletCountChanged;

    public static int BossBulletCount { get; private set; }
    public static int PlayerBulletCount { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        BossBulletCount = 0;
        PlayerBulletCount = 0;
    }

    public static void RegisterBullet(string bulletTag)
    {
        if (bulletTag == "BossBullet")
        {
            BossBulletCount++;
            OnBossBulletCountChanged?.Invoke();
        }
        else if (bulletTag == "PlayerBullet")
        {
            PlayerBulletCount++;
            OnPlayerBulletCountChanged?.Invoke();
        }
    }

    public static void UnregisterBullet(string bulletTag)
    {
        if (bulletTag == "BossBullet")
        {
            BossBulletCount--;
            if (BossBulletCount < 0) BossBulletCount = 0;
            OnBossBulletCountChanged?.Invoke();
        }
        else if (bulletTag == "PlayerBullet")
        {
            PlayerBulletCount--;
            if (PlayerBulletCount < 0) PlayerBulletCount = 0;
            OnPlayerBulletCountChanged?.Invoke();
        }
    }
}