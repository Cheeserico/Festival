using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �E�N���b�N���j���[�ɕ\������Afilename�̓f�t�H���g�̃t�@�C����
[CreateAssetMenu(fileName = "WeaponSpawnerSettings", menuName = "ScriptableObjects/WeaponSpawnerSettings")]


public class WeaponSpawnerSettings : ScriptableObject
{

    // �L�����N�^�[�f�[�^
    // �L�����N�^�[�Z�b�e�B���O�̃R�[�h�𗬗p
    
    public List<WeaponSpawnerStats> datas;

    // �O������A�N�Z�X����邽�߂̃v���p�e�B�iGetcomponent���v��Ȃ��j�V���O���g��
    static WeaponSpawnerSettings instance;
    public static WeaponSpawnerSettings Instance
    {
        get
        {
            // �ŏ��͂��ׂ�instance����Ă��Ȃ��B�C���X�^���X���Ȃ����
            if (!instance)
            {
                // CharacterSettingst�Ƃ������O�̃A�Z�b�g������Ă���
                // Resources.Lode������v���W�F�N�g�ɑ��݂���uResources�Ƃ������O�̃t�H���_�[�̒��g�v����F�X�Ǝ擾�ł���
                instance = Resources.Load<WeaponSpawnerSettings>(nameof(WeaponSpawnerSettings));
            }
            // �l��Ԃ�
            return instance;
        }
    }

    // ���X�g��ID����f�[�^����������
    public WeaponSpawnerStats Get(int id, int lv)
    {
        // ����̐ݒ�͓���ID�̂̈Ⴄ���x����ݒ�o����悤�ɂ������̂�
        // �f�[�^��T��Get�֐��͈����Ŏw�肳�ꂽ���x���ɍł��߂��f�[�^��T���ĕԂ��悤�ȋ@�\������
        // �w�肳�ꂽ���x���̃f�[�^���Ȃ���Έ�ԍ������x���̃f�[�^��Ԃ�
        WeaponSpawnerStats ret = null;

        foreach(var item in datas)
        {
            if (id != item.Id) continue;

            // �w�背�x���ƈ�v
            if(lv == item.Lv)
            {
                return (WeaponSpawnerStats)item.GetCopy();
            }
            // ���̃f�[�^���Z�b�g����Ă��Ȃ����A����𒴂��郌�x���������������ւ���
            if(null == ret)
            {
                ret = item;
            }
            // �T���Ă��郌�x����艺�ŁA�b��f�[�^���傫��

            else if(item.Lv < lv && ret.Lv < item.Lv)
            {
                ret = item;
            }
        }

        return (WeaponSpawnerStats)ret.GetCopy();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    // �쐬
    // ���퐶�����u�𐶐�����֐�
    public BaseWeaponSpawner CreateWeaponSpawner(int id, EnemySpawnerController enemySpawner, Transform parent = null)
    {

        // �f�[�^�擾
        WeaponSpawnerStats stats = instance.Get(id, 1);
        // �I�u�W�F�N�g�쐬
        ///////////////////////////////////////
        GameObject obj�@= Instantiate(stats.PrefabSpawner, parent);
        // �f�[�^�Z�b�g
        BaseWeaponSpawner spawner = obj.GetComponent<BaseWeaponSpawner>();

        spawner.Init(enemySpawner, stats);
        return spawner;
    }
    

}


// ���퐶�����u
[System.Serializable]
public class WeaponSpawnerStats : BaseStats
{
    // �������u�̃v���n�u
    public GameObject PrefabSpawner;
    // ����̃A�C�R��
    public Sprite Icon;
    // ���x���A�b�v���ɒǉ������A�C�e��ID
    public int LevelUpItemId;
    // ��x�ɐ������鐔
    public float SpawnCount;
    // �����^�C�}�[
    public float SpawnTimerMin;
    public float SpawnTimerMax;

    // �������Ԏ擾
    public float GetRandomSpawnTimer()
    {
        return Random.Range(SpawnTimerMin, SpawnTimerMax);
    }
    
    
    // TODO�A�C�e����ǉ�

}