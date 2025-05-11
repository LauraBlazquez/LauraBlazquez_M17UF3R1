using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    [HideInInspector] public float speed;
    [HideInInspector] public float damage;
    private Vector3 direction;
    private Coroutine lifeCoroutine;
    [HideInInspector] public float lifetime = 2f;
    [HideInInspector] public string targetTag;
    [HideInInspector] public string poolID;

    public void Fire(Vector3 dir, float bulletDamage, string targetTag, string poolID, float bulletSpeed = 3f, float bulletLifetime = 2f)
    {
        direction = dir.normalized;
        speed = bulletSpeed;
        damage = bulletDamage;
        this.targetTag = targetTag;
        this.poolID = poolID;
        this.lifetime = bulletLifetime;

        gameObject.SetActive(true);

        if (lifeCoroutine != null)
            StopCoroutine(lifeCoroutine);

        lifeCoroutine = StartCoroutine(AutoDisableAfterTime());
    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private IEnumerator AutoDisableAfterTime()
    {
        yield return new WaitForSeconds(lifetime);
        ReturnToPool();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            if (targetTag == "Enemy")
            {
                var enemy = collision.GetComponent<Enemy>();
                if (enemy != null) enemy.TakeDamage(damage);
            }
            else if (targetTag == "Player")
            {
                var player = collision.GetComponent<Player>();
                if (player != null) player.TakeDamage(damage);
            }
            ReturnToPool();
        }
    }

    public void ReturnToPool()
    {
        if (lifeCoroutine != null)
        {
            StopCoroutine(lifeCoroutine);
            lifeCoroutine = null;
        }

        var pool = GenericPool.GetPool(poolID);
        if (pool != null)
            pool.ReturnBullet(gameObject);
        else
            gameObject.SetActive(false);
    }
}
