using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpButton : MonoBehaviour
{
    public TMP_Text WeaponName;
    public TMP_Text WeaponDescription;
    public Image WeaponIcon;

    private Weapon AssignedWeapon;

    public void ActivateButton(Weapon Weapon)
    {
        WeaponName.text = Weapon.name;
        WeaponDescription.text = Weapon.Stats[Weapon.WeaponLevel].description;
        WeaponIcon.sprite = Weapon.WeaponImage;

        AssignedWeapon = Weapon;
    }

    public void SelectSpeedUpgrade()
    {
        if (Player.Instance != null)
        {
            Player.Instance.AddSpeedLvlUp(0.25f);
        }

        if (UIController.Instance != null)
        {
            UIController.Instance.LevelUpPanelClose();
        }
    }

    public void SelectHealthUpgrade()
    {
        if (Player.Instance != null)
        {
            Player.Instance.AddHealthLvlUp(5f);
        }

        if (UIController.Instance != null)
        {
            UIController.Instance.LevelUpPanelClose();
        }
    }
    public void SelectUpgrade()
    {
        AssignedWeapon.LevelUp();
        UIController.Instance.LevelUpPanelClose();
    }
}
