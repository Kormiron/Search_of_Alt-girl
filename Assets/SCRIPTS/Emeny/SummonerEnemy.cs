using UnityEngine;

public class SumonerEnemy : Enemy
{
    [SerializeField] private GameObject BatPrefab;
    [SerializeField] private float StopDistance = 5f;
    [SerializeField] private float SummonInterval = 4f;
    private float SummonTimer;

    protected void Update()
    {
        if (GameManager.Instance != null && !GameManager.Instance.GameActive)
        {
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            return;
        }

        if (Player.Instance != null && Player.Instance.gameObject.activeSelf)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, Player.Instance.transform.position);

            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sr.flipX = Player.Instance.transform.position.x > transform.position.x;
            }

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (distanceToPlayer > StopDistance)
            {
                Vector2 dir = (Player.Instance.transform.position - transform.position).normalized;
                rb.linearVelocity = dir * 2f;
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
            }

            SummonTimer += Time.deltaTime;
            if (SummonTimer >= SummonInterval)
            {
                SummonTimer = 0f;
                SummonBats();
            }
        }
    }

    private void SummonBats()
    {
        if (BatPrefab == null) return;

        Instantiate(BatPrefab, (Vector2)transform.position + new Vector2(0.5f, 0f), Quaternion.identity);
        Instantiate(BatPrefab, (Vector2)transform.position + new Vector2(-0.5f, 0f), Quaternion.identity);
    }
}
