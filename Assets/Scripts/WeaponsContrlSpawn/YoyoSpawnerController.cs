using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoyoSpawnerController : BaseWeaponSpawner
{   
    void Update()
    {
        // 一定間隔でターゲットとなる敵をランダムで探して、貫通しながら必中で飛んでいく仕様

        if (isSpawnTimerNotElapsed()) return;

        // 次のタイマー
        spawnTimer = stats.GetRandomSpawnTimer();
        // 敵がいない
        if (1 > enemySpawner.GetEnemies().Count) return;

        for(int i = 0; i < (int)stats.SpawnCount; i++)
        {
            // 武器生成
            
            YoyoController ctrl =
                (YoyoController)createWeapon(transform.position);

            // ランダムでターゲットを決定
            List<EnemyController> enemies = enemySpawner.GetEnemies();
            int rnd = Random.Range(0, enemies.Count);
            EnemyController target = enemies[rnd];

            ctrl.Target = target;


        }
    }
}
