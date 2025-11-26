using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    [Header("Shooting Settings")]
    [SerializeField] private float _shootCooldown = 0.2f;
    [SerializeField] private float _bulletSpeed = 15f;

    [Header("Spawn Settings")]
    [Tooltip("Punto desde donde salen las balas. Si no se asigna, usa la posición del jugador.")]
    [SerializeField] private Transform _shootPoint;

    private float _shootCooldownTimer = 0f;

    private void Update()
    {
        _shootCooldownTimer += Time.deltaTime;

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.Z)) && _shootCooldownTimer >= _shootCooldown)
        {
            Shoot();
            _shootCooldownTimer = 0f;
        }
    }

    private void Shoot()
    {
        Vector2 shootOrigin = _shootPoint != null ? _shootPoint.position : transform.position;
        Vector2 velocity = Vector2.up * _bulletSpeed;

        ShotAttack.SimpleShot(shootOrigin, velocity, "PlayerBullet");
    }
}