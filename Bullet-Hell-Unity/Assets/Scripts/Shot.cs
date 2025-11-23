using UnityEngine;

public class Shot : MonoBehaviour
{
    [SerializeField] private float _shootCooldown = 0.5f;
    [SerializeField] private float _bulletSpeed = 10f;
    [SerializeField] private RadialShotSettings _shotSettings;

    private float _shootCooldownTimer = 0f;

    private void Update()
    {
        _shootCooldownTimer += Time.deltaTime;

        if (_shootCooldownTimer >= _shootCooldown)
        {
            ShotAttack.RadialShot(transform.position, transform.up, _shotSettings);
            _shootCooldownTimer = 0f;
        }
    }
}
