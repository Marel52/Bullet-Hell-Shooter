using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float MAX_LIFE_TIME = 3f;

    private float _lifeTime = 0f;

    public Vector2 Speed;

    private void Update()
    {
        transform.position += (Vector3)Speed * Time.deltaTime;
    }

    private void Disable()
    {
        _lifeTime = 0f;
        gameObject.SetActive(false);
    }
}
