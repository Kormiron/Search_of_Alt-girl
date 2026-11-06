using UnityEngine;

public class AreaWeapon : Weapon
{
    //radiance damage for 1 ability
    [SerializeField] private GameObject prefab;
    private float SpawnCounter;


    void Update()
    {
        SpawnCounter -= Time.deltaTime;
        if(SpawnCounter <= 0)
        {
            if (AudioManager.Instance != null && AudioManager.Instance.PlayerAttackSound != null)
            {
                AudioManager.Instance.PlaySFX(AudioManager.Instance.PlayerAttackSound);
            }
            SpawnCounter = Stats[WeaponLevel].cooldown;
            Instantiate(prefab, transform.position, transform.rotation, transform);
        }
    }
}
