using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelUpPanel : MonoBehaviour
{
    [SerializeField] LevelUpButton[] levelUpButtons;
    public UnityAction OnClose;
    [SerializeField] GameSceneDirector gameSceneDirector;

    private void Awake()
    {
        foreach (var levelUpButton in levelUpButtons)
        {
            levelUpButton.OnSelect += OnSelectWeapon;
        }
    }
    public void Show() 
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
        // 3つの武器をランダムに選ぶ
        // 武器を3つ選ぶ
        // データを引っ張ってくる
        // 表示する
        List<int> weaponIDs = gameSceneDirector.Player.Stats.UsableWeponIds;
        foreach (var levelUpButton in levelUpButtons)
        {
            int id = Random.Range(0, weaponIDs.Count);
            var weapon = WeaponSpawnerSettings.Instance.Get(id, 1);
            BaseWeaponSpawner weaponSpawner = gameSceneDirector.Player.WeaponSpawners.Find(x => x.stats.Id == id);
            if (weaponSpawner)
            {
                weapon = WeaponSpawnerSettings.Instance.Get(id, weaponSpawner.stats.Lv+1);
                levelUpButton.SetWeapon(weapon);
            }
            else
            {
                levelUpButton.SetWeapon(weapon);
            }
        }
    }

    void OnSelectWeapon(WeaponSpawnerStats weaponSpawnerStats)
    {
        gameSceneDirector.Player.addWeaponSpawner(weaponSpawnerStats.Id);
        Close();
    }

    public void Close()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        OnClose?.Invoke();
    }

}
