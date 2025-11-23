using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private int _initialPoolSize = 10;

    private List<Bullet> _bulletPool = new List<Bullet>();

    private void Awake()
    {
        // Implementación simple de Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        AddBulletsToPool(_initialPoolSize);
    }

    private void AddBulletsToPool(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Bullet bullet = Instantiate(_bulletPrefab);
            bullet.gameObject.SetActive(false);
            bullet.transform.SetParent(transform);

            _bulletPool.Add(bullet);
        }
    }

    public Bullet RequestBullet()
    {
        // Buscar una bala inactiva
        for (int i = 0; i < _bulletPool.Count; i++)
        {
            if (!_bulletPool[i].gameObject.activeSelf)
            {
                _bulletPool[i].gameObject.SetActive(true);
                return _bulletPool[i];
            }
        }

        // Si no hay balas libres, crear más
        AddBulletsToPool(1);
        Bullet newBullet = _bulletPool[_bulletPool.Count - 1];
        newBullet.gameObject.SetActive(true);

        return newBullet;
    }
}
