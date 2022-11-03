using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] private GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            health -= other.GetComponent<Bullet>().damage;
            if (health < 10)
            {
                gameManager.enemies.Remove(this);
                Destroy(gameObject);
            }
        }
    }
    
}
