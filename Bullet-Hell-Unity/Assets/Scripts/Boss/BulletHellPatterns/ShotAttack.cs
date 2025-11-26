using UnityEngine;

public static class ShotAttack
{
    public static void SimpleShot(Vector2 origin, Vector2 speed, string bulletTag = "BossBullet")
    {
        Bullet bullet = bulletTag == "PlayerBullet" 
            ? BulletPool.Instance.RequestPlayerBullet() 
            : BulletPool.Instance.RequestBossBullet();

        if (bullet != null)
        {
            bullet.transform.position = origin;
            bullet.Speed = speed;
            bullet.gameObject.tag = bulletTag;
        }
    }

    public static void RadialShot(Vector2 origin, Vector2 aimDirection, RadialShotSettings settings, string bulletTag = "BossBullet")
    {
        float angleBetweenBullets = 360f / settings.NumberOfBullets;
        float initialRotation = (angleBetweenBullets * settings.PhaseOffset) + settings.AngleOffset;
        aimDirection = aimDirection.Rotate(initialRotation);

        for (int i = 0; i < settings.NumberOfBullets; i++)
        {
            float bulletDirectionAngle = angleBetweenBullets * i;

            if (settings.RadialMask && bulletDirectionAngle > settings.MaskAngle)
                break;

            Vector2 bulletDirection = aimDirection.Rotate(bulletDirectionAngle);
            
            Vector2 spawnPosition = origin + (bulletDirection * settings.SpawnRadius);
            
            SimpleShot(spawnPosition, bulletDirection * settings.BulletSpeed, bulletTag);

            if (settings.MirrorPattern)
            {
                Vector2 mirroredDirection = new Vector2(-bulletDirection.x, bulletDirection.y);
                Vector2 mirroredSpawnPos = origin + (mirroredDirection * settings.SpawnRadius);
                SimpleShot(mirroredSpawnPos, mirroredDirection * settings.BulletSpeed, bulletTag);
            }
        }
    }
}