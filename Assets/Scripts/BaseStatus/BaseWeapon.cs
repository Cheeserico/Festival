using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    // BaseWepon��protect�Œ�`�����֐��Ȃ̂Œ�`���ĂȂ��Ă��p����̃N���X�ŌĂׂ�B�N���X���p��������
    // �e�̐������u
    protected BaseWeaponSpawner spawner;
    // ����X�e�[�^�X
    protected WeaponSpawnerStats stats;
    // ��������
    protected Rigidbody2D rigidbody2d;
    // ����
    protected Vector2 forwared;

    // ������
    // �������ɂ��̊֐����Ăяo���ĕ���ɕK�v�ȃf�[�^���Z�b�g����
    public void Init(BaseWeaponSpawner spawner, Vector2 forward)
    {
        // �e�̐������u
        this.spawner = spawner;
        // ����f�[�^�Z�b�g
        this.stats = (WeaponSpawnerStats)spawner.stats.GetCopy();
        // �i�ޕ���
        this.forwared = forward;
        // ��������
        this.rigidbody2d = GetComponent<Rigidbody2D>();
        // �������Ԃ�����ΐݒ肷��
        if (-1 < stats.AliveTime)
        {
            Destroy(gameObject, stats.AliveTime);
        }
    }
   
    // �G�֍U��
    protected void attackEnemy(Collider2D collider2d, float attack)
    {
        // �G����Ȃ�
        // TryGetComponent���g���ƁAComponent���������ꍇ�̂�Enemy�ϐ��Ƀf�[�^������
        if (!collider2d.gameObject.TryGetComponent<EnemyController>(out var enemy)) return;
        // �U��
        float damage = enemy.Damage(attack);
        // ���_���[�W�v�Z
        spawner.TotalDamage += damage;
        // HP�ݒ肪����Ύ������_���[�W
        if (0 > stats.HP) return;
        stats.HP--;
        if (0 > stats.HP) Destroy(gameObject);
    }

    // �G�ւ̍U���i�f�t�H���g�̍U���́j
    protected void attackEnemy(Collider2D collider2d)
    {
        attackEnemy(collider2d, stats.Attack);
    }





}
