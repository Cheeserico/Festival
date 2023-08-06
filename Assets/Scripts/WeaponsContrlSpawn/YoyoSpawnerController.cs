using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoyoSpawnerController : BaseWeaponSpawner
{   
    void Update()
    {
        // ���Ԋu�Ń^�[�Q�b�g�ƂȂ�G�������_���ŒT���āA�ђʂ��Ȃ���K���Ŕ��ł����d�l

        if (isSpawnTimerNotElapsed()) return;

        // ���̃^�C�}�[
        spawnTimer = stats.GetRandomSpawnTimer();
        // �G�����Ȃ�
        if (1 > enemySpawner.GetEnemies().Count) return;

        for(int i = 0; i < (int)stats.SpawnCount; i++)
        {
            // ���퐶��
            
            YoyoController ctrl =
                (YoyoController)createWeapon(transform.position);

            // �����_���Ń^�[�Q�b�g������
            List<EnemyController> enemies = enemySpawner.GetEnemies();
            int rnd = Random.Range(0, enemies.Count);
            EnemyController target = enemies[rnd];

            ctrl.Target = target;


        }
    }
}
