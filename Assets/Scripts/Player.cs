using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10.0f;
    private float horizontal;
    private float vertical;
    private float diagonalSpeedLimit = 0.7f;
    public Joystick joystick;
    [SerializeField] private Animator swatAnimator;
    private float reloadCounter;
    private float reloadDuration = 0.8f;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform bulletHolder;
    [SerializeField] private GameManager gameManager;
    [SerializeField] public float range;

    Vector2 moveDirection;
    private void Start()
    {
        reloadCounter = reloadDuration;
        if (IsEnemyInRange())
        {
            var playerDirection = (GetClosestEnemy().transform.position - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
        }
    }
    void Update()
    {
        MovementInputs();
        Movement();

        if (IsEnemyInRange())
        {
            Fire();
        }

    }
    private Enemy GetClosestEnemy()
    {
        var minDistance = Vector3.Distance(gameManager.enemies[0].transform.position, spawnPoint.position);
        Enemy closestEnemy = gameManager.enemies[0];

        for (int i = 1; i < gameManager.enemies.Count; i++)
        {
            var distance = Vector3.Distance(gameManager.enemies[i].transform.position, spawnPoint.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestEnemy = gameManager.enemies[i];
            }
        }
        return closestEnemy;
    }
    private bool IsEnemyInRange()
    {
        return gameManager.enemies.Count > 0 && (GetClosestEnemy().transform.position - spawnPoint.position).magnitude <= range;
    }

    private void Fire()
    {
        reloadCounter += Time.deltaTime;
        if (reloadCounter >= reloadDuration)
        {
            reloadCounter = 0;
            var bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);
            bullet.direction = (GetClosestEnemy().transform.position+Vector3.up - spawnPoint.position).normalized;
        }
    }
    private void MovementInputs()
    {
        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;
    }
    private void Movement()
    {
        float moveFB = vertical * moveSpeed * Time.deltaTime;
        float moveLR = horizontal * moveSpeed * Time.deltaTime;
        moveDirection = new Vector2(horizontal, vertical).normalized;

        if (horizontal != 0 && vertical != 0)
        {
            transform.position += new Vector3(moveLR * diagonalSpeedLimit, 0f, moveFB * diagonalSpeedLimit);
            swatAnimator.SetFloat("move", 1);
            if (IsEnemyInRange())
            {              
                var playerDirection = (GetClosestEnemy().transform.position - transform.position).normalized;
                transform.rotation = Quaternion.LookRotation(playerDirection, Vector3.up);
            }
            else
                SetRotation(moveDirection);
        }
        else
        {
            swatAnimator.SetFloat("move", 0);
        }
    }
    private void SetRotation(Vector3 direction)
    {
        direction.z = direction.y;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }
}