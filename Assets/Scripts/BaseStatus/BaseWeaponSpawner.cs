using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeaponSpawner : MonoBehaviour
{
    //����̃v���n�u
    [SerializeField] GameObject PrefabWeapon;
    // ����̃f�[�^
    public WeaponSpawnerStats stats;
    // �^�������_���[�W
    public float TotalDamage;
    // �ғ��^�C�}�[
    public float TotalTimer;
    // �����^�C�}�[
    protected float spawnTimer;
    // ������������̃��X�g
    protected List<BaseWeapon> weapons;
    // �G�������u
    protected EnemySpawnerController enemySpawner;
    // ������
    public void Init(EnemySpawnerController enemySpawner, WeaponSpawnerStats stats)
    {
        // �ϐ�������
        weapons = new List<BaseWeapon>();
        this.enemySpawner = enemySpawner;
        this.stats = stats;
    }
    // �ғ��^�C�}�[
    // Update�͕��킲�ƂɌŗL�̏����������čs�������̂ŁA���ʂ�FixUpdate�ő��u�̐����^�C�}�[���񂵂Ă���
    private void FixedUpdate()
    {
        TotalTimer += Time.fixedDeltaTime;
    }

    // ���퐶��
    // ����𐶐�����Ƃ��̈�A�̋��ʂ�������
    // BaseWepon��protect�Œ�`�����֐��Ȃ̂Œ�`���ĂȂ��Ă��p����̃N���X�ŌĂׂ�B�N���X���p��������
    protected BaseWeapon createWeapon(Vector3 position, Vector2 forward, Transform parent = null)
    {
        // ����
        GameObject obj = Instantiate(PrefabWeapon, position, PrefabWeapon.transform.rotation, parent);
        // ���ʃf�[�^�Z�b�g
        BaseWeapon weapon = obj.GetComponent<BaseWeapon>();
        weapon.Init(this, forward);
        // �f�[�^������
        weapons.Add(weapon);
        // ���탊�X�g�֒ǉ�
        return weapon;

    }

    // ���퐶���i�ȈՔ�
    // �����֐����̈�����������o�[�W����������Ă���
    protected BaseWeapon createWeapon(Vector3 position, Transform parent = null)
    {
        return createWeapon(position, Vector2.zero, parent);
    }
    
    // ����̃A�b�v�f�[�g���~����
    //�@�Q�[�����ꎞ��~�������Ƃ��̂��߂Ɏ����Ǝ�����������������̓������~�߂�֐� 
    public void SetEnabled(bool enabled=true)
    {
        this.enabled = enabled;
        // �I�u�W�F�N�g���폜
        weapons.RemoveAll(item => item);
        // ��������������~
        foreach(var item in weapons)
        {
            item.enabled = enabled;
            // Rigidbody��~
            item.GetComponent<Rigidbody2D>().simulated = enabled;
        }
    }
    // �^�C�}�[�����`�F�b�N
    protected bool isSpawnTimerNotElapsed()
    {
        // �^�C�}�[����
        spawnTimer -= Time.deltaTime;
        if (0 < spawnTimer) return true;
        return false;
    }
    
    // TODO�@���x���A�b�v���̃f�[�^��Ԃ�
    public virtual void LevelUp()
    {
        stats.Lv++;
        this.stats = WeaponSpawnerSettings.Instance.Get(stats.Id, stats.Lv);
    }
}
