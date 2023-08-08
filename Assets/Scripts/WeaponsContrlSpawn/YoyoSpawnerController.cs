using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
        List<EnemyController> enemies = enemySpawner.GetEnemies().FindAll(x => Vector2.Distance(transform.position, x.transform.position) < 5);
        if (enemies.Count > 0)
        {
            enemies.Sort((a,b) => (int)(Vector2.Distance(transform.position, a.transform.position) - Vector2.Distance(transform.position, b.transform.position)));
        }

        for (int i = 0; i < (int)stats.SpawnCount; i++)
        {
            // ���퐶��
            
            YoyoController ctrl =
                (YoyoController)createWeapon(transform.position);

            // �����_���Ń^�[�Q�b�g������
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
