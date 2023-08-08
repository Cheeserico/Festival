using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LevelUpButton : MonoBehaviour
{
    WeaponSpawnerStats weaponSpawnerStats;
    int level;
    [SerializeField] Text weaponName;
    [SerializeField] Image weaponImage;
    [SerializeField] Text weaponLevel;
    [SerializeField] Text weaponHelp;

    public UnityAction<WeaponSpawnerStats> OnSelect;
    public void SetWeapon(WeaponSpawnerStats weaponSpawnerStats)
    {
        weaponName.text = weaponSpawnerStats.Name;
        weaponImage.sprite = weaponSpawnerStats.Icon;
        weaponLevel.text = $"Lv.{weaponSpawnerStats.Lv}";
        weaponHelp.text = weaponSpawnerStats.Description;
        this.weaponSpawnerStats = weaponSpawnerStats;
    }

    public void OnSelectWeapon()
    {
        OnSelect?.Invoke(weaponSpawnerStats);
        SoundManager.Instance.PlaySE(SE.Button);
    }
}
