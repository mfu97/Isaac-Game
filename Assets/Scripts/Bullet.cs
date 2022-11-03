using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [SerializeField] private float bulletSpeed;
    public float damage;
    public Vector3 direction;

    void Update()
    {
        transform.position += direction * Time.deltaTime * bulletSpeed;
        
    }
}