using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource MusicSource;
    [SerializeField] private AudioSource SfxSource;

    [Header("Audio Clips")]
    public AudioClip BackgroundMusic;
    public AudioClip LevelUpSound;
    public AudioClip PlayerDeathSound;
    public AudioClip EnemyDeathSound;
    public AudioClip PlayerAttackSound;
    public AudioClip EnemyAttackSound;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (BackgroundMusic != null && MusicSource != null)
        {
            MusicSource.clip = BackgroundMusic;
            MusicSource.loop = true;
            MusicSource.Play();
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && SfxSource != null)
        {
            SfxSource.PlayOneShot(clip);
        }
    }
}