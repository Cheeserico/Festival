using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �E�N���b�N���j���[�ɕ\������Afilename�̓f�t�H���g�̃t�@�C����
[CreateAssetMenu(fileName = "CharacterSettings",menuName = "ScriptableObjects/CharacterSettings")]


public class CharacterSettings : ScriptableObject
{
    // �L�����N�^�[�f�[�^
    public List<CharacterStats> datas;

    // �O������A�N�Z�X����邽�߂̃v���p�e�B�iGetcomponent���v��Ȃ��j�V���O���g��
    static CharacterSettings instance;
    public static CharacterSettings Instance
    {
        get
        {
            // �ŏ��͂��ׂ�instance����Ă��Ȃ��B�C���X�^���X���Ȃ����
            if(!instance)
            {
                // CharacterSettingst�Ƃ������O�̃A�Z�b�g������Ă���
                // Resources.Lode������v���W�F�N�g�ɑ��݂���uResources�Ƃ������O�̃t�H���_�[�̒��g�v����F�X�Ǝ擾�ł���
                instance = Resources.Load<CharacterSettings>(nameof(CharacterSettings));
            }
            // �l��Ԃ�
            return instance;
        }
    }

    // ���X�g��ID����f�[�^����������
    public CharacterStats Get(int id)
    {
        Debug.Log("tesu");

        return (CharacterStats)datas.Find(item => item.Id == id).GetCopy();
    }

    // �G����
    public EnemyController CreateEnemy(int id, GameSceneDirector sceneDirector,Vector3 position)
    {
        // �X�e�[�^�X�擾
        CharacterStats stats = instance.Get(id);
        // �I�u�W�F�N�g
        GameObject obj = Instantiate(stats.prefab, position, Quaternion.identity);
        //�f�[�^�Z�b�g
        EnemyController ctrl = obj.GetComponent<EnemyController>();
        ctrl.Init(sceneDirector, stats);

        return ctrl;


    }


}
   
    //�@�G�̓���
    
public enum MoveType
    
{
        //�@�v���C���[�Ɍ������Đi��
        TargetPlayer,

        //�@������ɐi��
        TargetDirection
}
    [Serializable]  
public class CharacterStats : BaseStats
    
{
        // �L�����N�^�[�̃v���n�u
        public GameObject prefab;
        // ������������ID
        public List<int> DefaultWeaponIds;
        // �����\����ID
        public List<int> UsableWeponIds;
        // �����\��
        public int UsableWeponMax;
        // �ړ��^�C�v
        public MoveType MoveType;

        // [ToDo] �A�C�e���ǉ�    
}



    

