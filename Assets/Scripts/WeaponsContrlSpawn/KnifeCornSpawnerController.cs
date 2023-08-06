using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeCornSpawnerController : BaseWeaponSpawner
{
    // ��x�̐����Ɏ���������
    // 0.3�b���ԍ������āA�X�|�[���J�E���g���������ʏ�̃^�C�}�[���Z�b�g����
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
        // ���퐶��
        KnifeCornController ctrl = (KnifeCornController)createWeapon(transform.position, player.Forward);
    }
}
