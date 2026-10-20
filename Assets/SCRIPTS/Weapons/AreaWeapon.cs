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
            SpawnCounter = Stats[WeaponLevel].cooldown;
            Instantiate(prefab, transform.position, transform.rotation, transform);


        }
    }
}
