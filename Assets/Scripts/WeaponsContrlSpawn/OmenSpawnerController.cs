using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmenSpawnerController : BaseWeaponSpawner
{
    // ��x�̐����Ɏ���������
    // 0.3�b���ԍ������āA�X�|�[���J�E���g���������ʏ�̃^�C�}�[���Z�b�g����

    int onceSpawnCount;
    float onceSpawnTime = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        // �ŗL�̃p�����[�^���Z�b�g
        //SpawnCountt�Ƃ͈�x�ɐ������鐔
        onceSpawnCount = (int)stats.SpawnCount;
    }

    // Update is called once per frame
    void Update()
    {
        // �^�C�}�[����@spawnTimer�F�����^�C�}�[
        if (isSpawnTimerNotElapsed()) return;


        // �����ō��E�ɂ���
        int dir = (onceSpawnCount % 2 == 0) ? 1 : -1;
        
        // ����
        OmenController ctrl = (OmenController)createWeapon(transform.position);
        // �΂ߏ�ɗ͂�������
        ctrl.GetComponent<Rigidbody2D>().AddForce(new Vector2(100 * dir, 350));
        // ���̃^�C�}�[
        spawnTimer = onceSpawnTime;
        onceSpawnCount--;
        // 1��̐�������������烊�Z�b�g
        // �Z���^�C�}�[���Z�b�g����1��̐������̃J�E���g���I������琳�K�̃^�C�}�[���Z�b�g
        if (1 > onceSpawnCount)
        {
            spawnTimer = stats.GetRandomSpawnTimer();
            onceSpawnCount = (int)stats.SpawnCount;
        }



    }
}
