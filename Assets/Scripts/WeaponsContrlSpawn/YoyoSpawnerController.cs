using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        List<EnemyController> enemies = enemySpawner.GetEnemies().FindAll(x => Vector2.Distance(transform.position, x.transform.position) < 5);
        if (enemies.Count > 0)
        {
            enemies.Sort((a,b) => (int)(Vector2.Distance(transform.position, a.transform.position) - Vector2.Distance(transform.position, b.transform.position)));
        }

        for (int i = 0; i < (int)stats.SpawnCount; i++)
        {
            // 武器生成
            
            YoyoController ctrl =
                (YoyoController)createWeapon(transform.position);

            // ランダムでターゲットを決定
            EnemyController target = null;
            if (enemies.Count > i)
            {
                target = enemies[i];
            }




            ctrl.Target = target;
        }
        SoundManager.Instance.PlaySE(SE.WindWeapon);

    }
}
