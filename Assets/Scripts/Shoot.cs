using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform bulletHolder;
    private List<GameObject> bullets = new List<GameObject>();
    private int index = 0;

    void Start()
    {
        SpawnBullet();
        InvokeRepeating("ShootBullet", 1f, 3f);
    }
    void Update()
    {
    }
    private void SpawnBullet()
    {
        for (int i = 0; i < 20; i++)
        {
            var bullet = Instantiate(bulletPrefab, spawnPoint);
            bullet.SetActive(false);
            bullets.Add(bullet);
        }
    }
    private void ShootBullet()
    {
        if (bullets[index].activeSelf)
        {
            return;
        }
        bullets[index].SetActive(true);
        bullets[index].transform.SetParent(bulletHolder);
        GetNewIndex();
    }
    private void GetNewIndex()
    {
        if (index >= 19)
        {
            index = 0;
        }
        index++;
    }
}