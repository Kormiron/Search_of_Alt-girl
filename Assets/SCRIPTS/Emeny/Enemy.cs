using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] protected SpriteRenderer SpriteRender;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected float MoveSpeed;

    [SerializeField] private float Damage;
    [SerializeField] private float Health;
    [SerializeField] private GameObject DestroyEffect;
    [SerializeField] private int ExperienceToGive;
    [SerializeField] private float PushTime;
    private float PushCounter;
    [SerializeField] private int ScoreToGive;

    public Vector2 direction;
    void Update()
    {
        if(GameManager.Instance != null && GameManager.Instance.currentState != GameManager.GameState.Gameplay)
        {
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }
            return;
        }
        //terning enemy face to player
        if (Player.Instance != null && Player.Instance.gameObject.activeSelf)
        {
            if (Player.Instance.transform.position.x > transform.position.x)
            {
                SpriteRender.flipX = true;
            }
            else
            {
                SpriteRender.flipX = false;
            }

            if(PushCounter > 0)
            {
                PushCounter -= Time.deltaTime;
                if (MoveSpeed > 0)
                {
                    MoveSpeed = -MoveSpeed;
                }
                if (PushCounter <= 0)
                {
                    MoveSpeed = Mathf.Abs(MoveSpeed);
                }
            }

            direction = (Player.Instance.transform.position - transform.position).normalized;

            rb.linearVelocity = new Vector2(direction.x * MoveSpeed, direction.y * MoveSpeed);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    //hit a player
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.Instance.TakeDame(Damage);
            
        }
    }

    // take a damage from player abilities
    public void TakeDamage(float damage)
    {
        Health -= damage;
        PushCounter = PushTime;
        if (Health <= 0)
        {
            if (AudioManager.Instance != null && AudioManager.Instance.EnemyDeathSound != null)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.EnemyDeathSound);
            }
            Destroy(gameObject);
            Instantiate(DestroyEffect, transform.position, transform.rotation);
            Player.Instance.GetExperience(ExperienceToGive);
            ScoreManager.Instance.AddScore(ScoreToGive);
        }
    }
}
