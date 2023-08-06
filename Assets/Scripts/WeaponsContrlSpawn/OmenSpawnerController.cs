using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmenSpawnerController : BaseWeaponSpawner
{
    // 一度の生成に時差をつける
    // 0.3秒時間差をつけて、スポーンカウントを消費したら通常のタイマーをセットする

    int onceSpawnCount;
    float onceSpawnTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        // 固有のパラメータをセット
        //SpawnCounttとは一度に生成する数
        onceSpawnCount = (int)stats.SpawnCount;
    }

    // Update is called once per frame
    void Update()
    {
        // タイマー消費　spawnTimer：生成タイマー
        if (isSpawnTimerNotElapsed()) return;


        // 偶数で左右にだす
        int dir = (onceSpawnCount % 2 == 0) ? 1 : -1;
        
        // 生成
        OmenController ctrl = (OmenController)createWeapon(transform.position);
        // 斜め上に力を加える
        ctrl.GetComponent<Rigidbody2D>().AddForce(new Vector2(100 * dir, 350));
        // 次のタイマー
        spawnTimer = onceSpawnTime;
        onceSpawnCount--;
        // 1回の生成がおわったらリセット
        // 短いタイマーをセットして1回の生成分のカウントが終わったら正規のタイマーをセット
        if (1 > onceSpawnCount)
        {
            spawnTimer = stats.GetRandomSpawnTimer();
            onceSpawnCount = (int)stats.SpawnCount;
        }



    }
}
