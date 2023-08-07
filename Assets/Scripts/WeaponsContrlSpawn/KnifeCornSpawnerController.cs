using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeCornSpawnerController : BaseWeaponSpawner
{
    // 一度の生成に時差をつける
    // 0.3秒時間差をつけて、スポーンカウントを消費したら通常のタイマーをセットする
    int onceSpawnCount;
    float onceSpawnTime = 0.6f;
    PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        onceSpawnCount = (int)stats.SpawnCount;
        player = transform.parent.GetComponent<PlayerController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (isSpawnTimerNotElapsed()) return;
        // 武器生成
        KnifeCornController ctrl = (KnifeCornController)createWeapon(transform.position, player.Forward);
        spawnTimer = onceSpawnTime;
    }

    public override void LevelUp()
    {
        stats.Lv++;
        this.stats = WeaponSpawnerSettings.Instance.Get(stats.Id, stats.Lv);
        onceSpawnTime -= 0.1f;
        if (onceSpawnTime <= 0)
        {
            onceSpawnTime = 0.2f;
        }
    }

}
