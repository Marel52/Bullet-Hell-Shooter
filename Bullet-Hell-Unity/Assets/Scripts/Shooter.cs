using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{
    [SerializeField] private Gameobject bulletprefab;
    [SerializeField] private float bulletSpeed;
    
    public void Attack()
    {
        Vector2 targetDirection = PlayerController.Instance.transform.posotion - transform.position;

        GameObject newBullet = Instantiate(bulletprefab, transform.position, Quaternion.identity);

        newBullet.transform.right = targetDirection;
    }

}
