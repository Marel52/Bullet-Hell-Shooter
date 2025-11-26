using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float MAX_LIFE_TIME = 10f;

    private float _lifeTime = 0f;
    public Vector2 Speed;

    private void OnEnable()
    {
        _lifeTime = 0f;
        BulletManager.RegisterBullet(gameObject.tag);
    }

    private void OnDisable()
    {
        BulletManager.UnregisterBullet(gameObject.tag);
    }

    private void Update()
    {
        transform.position += (Vector3)Speed * Time.deltaTime;

        _lifeTime += Time.deltaTime;

        if (_lifeTime >= MAX_LIFE_TIME)
        {
            Disable();
        }
    }

    private void Disable()
    {
        _lifeTime = 0f;
        gameObject.SetActive(false);
    }

    public void DeactivateBullet()
    {
        Disable();
    }
}