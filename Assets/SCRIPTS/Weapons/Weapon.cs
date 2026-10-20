using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public int WeaponLevel;
    public List<WeaponStats> Stats;
    public Sprite WeaponImage;

    public void LevelUp()
    {
        if(WeaponLevel<Stats.Count - 1)
        {
            WeaponLevel++;
        }
    }

}


[System.Serializable]
public class WeaponStats
{
    public float damage;
    public float cooldown;
    public float duration;
    public float range;
    public float speed;
    public string description;

}
