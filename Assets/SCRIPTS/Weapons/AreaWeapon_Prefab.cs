using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class AreaWeapon_Prefab : MonoBehaviour
{

    public AreaWeapon Weapon;
    private float Timer;
    private Vector3 TargetSize;
    public List<Enemy> EnemiesInRange;
    private float Counter;
    
    // spawning ability
    void Start()
    {
        Weapon = GameObject.Find("AreaWeapon").GetComponent<AreaWeapon>();
        Timer = Weapon.Stats[Weapon.WeaponLevel].duration;
        TargetSize = Vector3.one * Weapon.Stats[Weapon.WeaponLevel].range;
        transform.localScale = Vector3.zero;

        Counter = Weapon.Stats[Weapon.WeaponLevel].speed;
    }

    
    void Update()
    {
        transform.localScale = Vector3.MoveTowards(transform.localScale,TargetSize, Time.deltaTime * 5);
        Timer -= Time.deltaTime;
        if(Timer <= 0)
        {
            TargetSize = Vector3.zero;
            if(transform.localScale.x == 0f)
            {
                Destroy(gameObject);
            }
        }
        Counter-=Time.deltaTime;
        if (Counter <= 0)
        {
            Counter = Weapon.Stats[Weapon.WeaponLevel].speed;
            for (int i = EnemiesInRange.Count -1; i>=0; i--)
            {
                if (EnemiesInRange[i] == null)
                {
                    EnemiesInRange.RemoveAt(i);
                }
                else
                {
                    EnemiesInRange[i].TakeDamage(Weapon.Stats[Weapon.WeaponLevel].damage);
                }
            }
        }
    }

    // deal a damgae to enemy in the area
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            EnemiesInRange.Add(collider.GetComponent<Enemy>());
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            EnemiesInRange.Remove(collider.GetComponent<Enemy>());
        }
    }
}
