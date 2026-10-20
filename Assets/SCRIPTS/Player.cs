using UnityEngine;
using System.Collections.Generic;


public class Player : MonoBehaviour
{

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float MoveSpeed;
    [SerializeField] private Animator animator;
    public Vector2 playerMoveDirection;
    public static Player Instance;
    public float PlayerMaxHealth;
    public float PlayerHealth;


    private bool IsImmune;
    [SerializeField] private float ImmunityDuration;
    [SerializeField] private float ImmunityTimer;

    public int Experience;
    public int currentLevel;
    public int maxLevel;
    public List<int> PlayerLevels;
    [SerializeField] public Weapon ActiveButton;






    private void Awake()
    {
        if (Instance != null && Instance !=this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        for (int i = PlayerLevels.Count; i < maxLevel; i++)
        {
            PlayerLevels.Add(Mathf.CeilToInt(PlayerLevels[PlayerLevels.Count - 1]+5f));
        }
       

        // Set player Health and connect it with Slider
        PlayerHealth = PlayerMaxHealth;
        UIController.Instance.UpdateHelthSlider();
        UIController.Instance.UpdateExperienceSlider();
    }

    void Update()
    {

        if (GameManager.Instance != null && GameManager.Instance.currentState != GameManager.GameState.Gameplay)
        {
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero; // или rb.velocity = Vector2.zero для более старых версий Unity
            }
            return;
        }


        //moving system + animations
        float InputX = Input.GetAxisRaw("Horizontal");
        float InputY = Input.GetAxisRaw("Vertical");

        animator.SetFloat("moveX", InputX);
        animator.SetFloat("moveY", InputY);

        playerMoveDirection = new Vector2(InputX, InputY).normalized;

        if (playerMoveDirection == Vector2.zero)
        {
            animator.SetBool("moving", false);
        }
        else {
            animator.SetBool("moving", true);
        }

        // create a immunity frame
        if (ImmunityTimer > 0) { 
            ImmunityTimer -= Time.deltaTime;
        }
        else { IsImmune = false;
        }

    }

    private void FixedUpdate()
    {
        rb.linearVelocity = playerMoveDirection * MoveSpeed;
    }


    // Deal a damage to player + add player Immunity Frames after hit
    public void TakeDame(float damage)
    {
        if (!IsImmune) 
        {
            IsImmune = true;
            ImmunityTimer = ImmunityDuration;
            PlayerHealth -= damage;
            UIController.Instance.UpdateHelthSlider();

            if (PlayerHealth <= 0)
            {
                gameObject.SetActive(false);
                GameManager.Instance.GameOver();
            }
        }
    }

    public void GetExperience(int ExperienceToGet)
    {
        Experience += ExperienceToGet;
        UIController.Instance.UpdateExperienceSlider();
        if(currentLevel< PlayerLevels.Count)
        {
            if(Experience >= PlayerLevels[currentLevel - 1])
            {
                LevelUp();
            }
        }
        
    }

    public void LevelUp()
    {
        currentLevel++;
        Experience -= PlayerLevels[currentLevel - 1];
        UIController.Instance.UpdateExperienceSlider();
        UIController.Instance.LevelUpPanelOpen();
        UIController.Instance.LevelUpButtons[0].ActivateButton(ActiveButton);
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SetState(GameManager.GameState.LevelUp);
        }
    }
    
}
