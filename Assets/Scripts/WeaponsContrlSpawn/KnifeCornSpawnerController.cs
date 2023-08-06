using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeCornSpawnerController : BaseWeaponSpawner
{
    // 一度の生成に時差をつける
    // 0.3秒時間差をつけて、スポーンカウントを消費したら通常のタイマーをセットする
    int onceSpawnCount;
    float onceSpawnTime = 0.3f;
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
    }
}
