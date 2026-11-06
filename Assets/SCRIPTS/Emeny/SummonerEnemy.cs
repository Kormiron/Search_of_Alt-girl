using UnityEngine;

public class SummonerEnemy : Enemy
{
    [SerializeField] private GameObject BatPrefab;
    [SerializeField] private float StopDistance ;
    [SerializeField] private float SummonInterval ;
    private float SummonTimer;

    protected void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.currentState != GameManager.GameState.Gameplay)
        {
            if (rb != null) rb.linearVelocity = Vector2.zero;
            return;
        }

        if (Player.Instance != null && Player.Instance.gameObject.activeSelf)
        {
            if (SpriteRender != null)
            {
                SpriteRender.flipX = Player.Instance.transform.position.x > transform.position.x;
            }

            float distanceToPlayer = Vector2.Distance(transform.position, Player.Instance.transform.position);

            if (distanceToPlayer > StopDistance)
            {
                Vector2 dir = (Player.Instance.transform.position - transform.position).normalized;
                rb.linearVelocity = dir * MoveSpeed;
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
        else
        {
            if (rb != null) rb.linearVelocity = Vector2.zero;
        }
    }

    private void SummonBats()
    {
        if (BatPrefab == null) return;

        Instantiate(BatPrefab, (Vector2)transform.position + new Vector2(0.5f, 0f), Quaternion.identity);
        Instantiate(BatPrefab, (Vector2)transform.position + new Vector2(-0.5f, 0f), Quaternion.identity);
    }
}
