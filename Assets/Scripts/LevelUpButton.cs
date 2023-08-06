using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LevelUpButton : MonoBehaviour
{
    int id;
    [SerializeField] Text weaponName;
    [SerializeField] Image weaponImage;
    [SerializeField] Text weaponLevel;
    [SerializeField] Text weaponHelp;

    public UnityAction<int> OnSelect;
    public void SetWeapon(WeaponSpawnerStats weaponSpawnerStats)
    {
        weaponName.text = weaponSpawnerStats.Name;
        weaponImage.sprite = weaponSpawnerStats.Icon;
        // weaponLevel.text = weaponSpawnerStats.;
        // weaponHelp.text = weaponSpawnerStats.help;
        id = weaponSpawnerStats.Id;
    }

    public void OnSelectWeapon()
    {
        OnSelect?.Invoke(id);
    }
}
