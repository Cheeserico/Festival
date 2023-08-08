using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ベースを継承するように（モノビヘイビア消す）
//  BaseWeponにprotectで定義した関数なので定義してなくても継承先のクラスで呼べる。クラスを継承したら
// 武器のプレハブをインスペクターから選択できるようになる
public class SlashSpawnerController : BaseWeaponSpawner
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
        // 場所
        Vector3 pos = transform.position;
        pos.x += 2f * dir;
        // 生成
        SlashController ctrl = (SlashController)createWeapon(pos, transform);
        // 左右で角度を変える
        ctrl.transform.eulerAngles = ctrl.transform.eulerAngles * dir;
        // 次のタイマー
        spawnTimer = onceSpawnTime;
        onceSpawnCount--;
        // 1回の生成がおわったらリセット
        // 短いタイマーをセットして1回の生成分のカウントが終わったら正規のタイマーをセット
        if(1 > onceSpawnCount)
        {
            spawnTimer = stats.GetRandomSpawnTimer();
            onceSpawnCount = (int)stats.SpawnCount;
        }

        SoundManager.Instance.PlaySE(SE.WindWeapon);

    }

    public override void LevelUp()
    {
        base.LevelUp();
        onceSpawnTime -= 0.2f;
        if (onceSpawnTime <= 0.2f)
        {
            onceSpawnTime = 0.2f;
        }
    }
}
