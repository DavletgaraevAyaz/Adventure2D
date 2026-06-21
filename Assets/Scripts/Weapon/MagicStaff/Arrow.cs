using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float speed = 8f;
    [SerializeField] private float maxDistance = 7f;

    private Vector3 startPosition;
    private Vector2 moveDirection;

    private int damage;

    private bool exploded;

    private Animator animator;
    private Collider2D projectileCollider;

    private void Awake()
    {
        projectileCollider = GetComponent<Collider2D>();
    }

    public void Initialize(Vector2 direction, int projectileDamage)
    {
        moveDirection = direction;
        damage = projectileDamage;

        startPosition = transform.position;

        float angle =
            Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation =
            Quaternion.Euler(0, 0, angle);
    }

    private void Update()
    {
        if (exploded)
            return;

        transform.position +=
            (Vector3)(moveDirection * speed * Time.deltaTime);

        float distance =
            Vector3.Distance(startPosition, transform.position);

        if (distance >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (exploded)
            return;

        if (collision.TryGetComponent(out EnemyEntity enemy))
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        
    }
}
