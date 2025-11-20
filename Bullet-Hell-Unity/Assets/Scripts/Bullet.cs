using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 22f;
    [SerializeField] private GameObject particleOnHitPrefabVFX;
    [SerializeField] private bool isEnemyBullet = false;
    [SerializeField] private float bulletRange = 10f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        MoveBullet();
        DetectFireDistance();
    }

    public bool GetIsEnemyBullet()
    {
        return isEnemyBullet;
    }

    public void UpdateBulletRange(float bulletRange)
    {
        this.bulletRange = bulletRange;
    }

    public void UpdateBulletSpeed(float bulletSpeed)
    {
        this.bulletSpeed = bulletSpeed;
    }

    private void OnTriggerEnter2S(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        Indestructible indestructible = 
    }



}
