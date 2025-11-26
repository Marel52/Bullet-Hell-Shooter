using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    [Header("Boss Bullet Pool")]
    [SerializeField] private Bullet _bossBulletPrefab;
    [SerializeField] private int _initialBossBulletCount = 150;
    [SerializeField] private int _expandBossBulletAmount = 20;

    [Header("Player Bullet Pool")]
    [SerializeField] private Bullet _playerBulletPrefab;
    [SerializeField] private int _initialPlayerBulletCount = 50;
    [SerializeField] private int _expandPlayerBulletAmount = 10;

    private List<Bullet> _bossBulletPool = new List<Bullet>();
    private List<Bullet> _playerBulletPool = new List<Bullet>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        InitializePools();
    }

    private void InitializePools()
    {
        if (_bossBulletPrefab != null)
        {
            AddBulletsToPool(_bossBulletPrefab, _bossBulletPool, _initialBossBulletCount, "BossBullet");
        }

        if (_playerBulletPrefab != null)
        {
            AddBulletsToPool(_playerBulletPrefab, _playerBulletPool, _initialPlayerBulletCount, "PlayerBullet");
        }
    }

    private void AddBulletsToPool(Bullet prefab, List<Bullet> pool, int amount, string bulletTag)
    {
        for (int i = 0; i < amount; i++)
        {
            Bullet bullet = Instantiate(prefab);
            bullet.gameObject.SetActive(false);
            bullet.gameObject.tag = bulletTag;
            bullet.transform.SetParent(transform);
            pool.Add(bullet);
        }
    }

    public Bullet RequestBossBullet()
    {
        return RequestBulletFromPool(_bossBulletPool, _bossBulletPrefab, _expandBossBulletAmount, "BossBullet");
    }

    public Bullet RequestPlayerBullet()
    {
        return RequestBulletFromPool(_playerBulletPool, _playerBulletPrefab, _expandPlayerBulletAmount, "PlayerBullet");
    }

    public Bullet RequestBullet()
    {
        return RequestBossBullet();
    }

    private Bullet RequestBulletFromPool(List<Bullet> pool, Bullet prefab, int expandAmount, string bulletTag)
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeSelf)
            {
                pool[i].gameObject.SetActive(true);
                return pool[i];
            }
        }

        if (prefab != null)
        {
            AddBulletsToPool(prefab, pool, expandAmount, bulletTag);
            Bullet newBullet = pool[pool.Count - 1];
            newBullet.gameObject.SetActive(true);
            return newBullet;
        }

        return null;
    }
}