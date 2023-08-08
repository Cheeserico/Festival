using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �x�[�X���p������悤�Ɂi���m�r�w�C�r�A�����j
//  BaseWepon��protect�Œ�`�����֐��Ȃ̂Œ�`���ĂȂ��Ă��p����̃N���X�ŌĂׂ�B�N���X���p��������
// ����̃v���n�u���C���X�y�N�^�[����I���ł���悤�ɂȂ�
public class SlashSpawnerController : BaseWeaponSpawner
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
        // �ꏊ
        Vector3 pos = transform.position;
        pos.x += 2f * dir;
        // ����
        SlashController ctrl = (SlashController)createWeapon(pos, transform);
        // ���E�Ŋp�x��ς���
        ctrl.transform.eulerAngles = ctrl.transform.eulerAngles * dir;
        // ���̃^�C�}�[
        spawnTimer = onceSpawnTime;
        onceSpawnCount--;
        // 1��̐�������������烊�Z�b�g
        // �Z���^�C�}�[���Z�b�g����1��̐������̃J�E���g���I������琳�K�̃^�C�}�[���Z�b�g
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
