using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //�ړ��ƃA�j���[�V����

    Rigidbody2D rigidbody2d;
    Animator animator;
    float moveSpeed = 10;

    // ���Ƃ�Intit�ֈړ�

    [SerializeField] GameSceneDirector sceneDirector;
    [SerializeField] Slider sliderHP;
    [SerializeField] Slider sliderXP;

    public CharacterStats Stats;

    // �U���̃N�[���_�E��
    float attackCoolDownTimer;
    float attackCoolDownTimerMax = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        updateTimer();
        movePlayer();
        moveCamera();
        moveSliderHP();

    }

    // �v���C���[�̈ړ��Ɋւ��鏈��

    void movePlayer()
    {
        // �ړ��������
        Vector2 dir = Vector2.zero;

        //�@�Đ�����A�j���[�V����
        string trigger = "";

        if (Input.GetKey(KeyCode.UpArrow))
        {
            dir += Vector2.up;
            trigger = "isUp";
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            dir -= Vector2.up;
            trigger = "isDown";
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            dir += Vector2.right;
            trigger = "isRight";
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            dir -= Vector2.right;
            trigger = "isLeft";
        }
        // ���͂��Ȃ���Δ�����
        if (Vector2.zero == dir) return;


        // �v���C���[�ړ�
        rigidbody2d.position += dir.normalized * moveSpeed * Time.deltaTime;

        // �A�j���[�V�������Đ�����(�A�j���[�^�p�ӂ��Ă��Ȃ��j
        //animator.SetTrigger(trigger);

        // �@�ړ��͈͐���
        // �n�_
        if (rigidbody2d.position.x < sceneDirector.WorldStart.x)
        {
            Vector2 pos = rigidbody2d.position;
            pos.x = sceneDirector.WorldStart.x;
            rigidbody2d.position = pos;
        }

        if (rigidbody2d.position.y < sceneDirector.WorldStart.y)
        {
            Vector2 pos = rigidbody2d.position;
            pos.y = sceneDirector.WorldStart.y;
            rigidbody2d.position = pos;
        }

        // �I�_
        if (sceneDirector.WorldEnd.x < rigidbody2d.position.x)
        {
            Vector2 pos = rigidbody2d.position;
            pos.x = sceneDirector.WorldEnd.x;
            rigidbody2d.position = pos;
        }

        if (sceneDirector.WorldEnd.y < rigidbody2d.position.y)
        {
            Vector2 pos = rigidbody2d.position;
            pos.y = sceneDirector.WorldEnd.y;
            rigidbody2d.position = pos;
        }




    }

    // �J�����ړ�

    // �J������WorldStar,End���ƃt�B�[���h�̈�Ԓ[�̈ʒu�ɂȂ��Ă��܂��ƁA�ʒu������Ȃ��B
    // TileMap�̍�����Map�̍��W�A�E��̍��W���擾���āA������������ƍ��ɂ݂͂ł��肵�Ȃ��悤�ɐ��䂵�Ă���i�|�W�V������߂��悤�ɂ��Ă���j
    void moveCamera()
    {
        Vector3 pos = transform.position;
        pos.z = Camera.main.transform.position.z;

        // �n�_
        if (pos.x < sceneDirector.TileMapStart.x)
        {
            pos.x = sceneDirector.TileMapStart.x;
        }

        if (pos.y < sceneDirector.TileMapStart.y)
        {
            pos.y = sceneDirector.TileMapStart.y;
        }
        // �I�_
        if (sceneDirector.TileMapEnd.x < pos.x)
        {
            pos.x = sceneDirector.TileMapEnd.x;
        }

        if (sceneDirector.TileMapEnd.y < pos.y)
        {
            pos.y = sceneDirector.TileMapEnd.y;
        }

        // �J�����̈ʒu���X�V����
        Camera.main.transform.position = pos;
    }

    // HP�X���C�_�[�ړ�
    void moveSliderHP()
    {
        //���[���h���W���X�N���[�����W�ɕϊ�

        Vector3 pos = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
        pos.y -= 50;
        sliderHP.transform.position = pos;

    }
    // �_���[�W���󂯂���GameSceneDirector�̃_���[�W�֐����Ăяo������
    // �_���[�W
    public void Damage(float attack)
    {

        //��A�N�e�B�u�Ȃ甲����
        if (!enabled) return;
        //�@�ő�l
        float damage = Mathf.Max(0, attack - Stats.Defense);

        Stats.HP -= damage;

        // �_���[�W�\��
        sceneDirector.DispDamege(gameObject, damage);

        //TODO �Q�[���I�[�o�[
        if(0>Stats.HP)
        {

        }

        if (0 > Stats.HP) Stats.HP = 0;
        setSloderHP();
    }

    // HP�X���C�_�[�̒l���X�V
    void setSloderHP()
    {
        sliderHP.maxValue = Stats.MaxHP;
        sliderHP.value = Stats.HP;

    }

    void setSloderXP()
    {
        sliderXP.maxValue = Stats.MaxXP;
        sliderXP.value = Stats.XP;

    }

    void OnCollitionEnter2D(Collision2D collition)
    {
        attackEnemy(collition);
    }
    // �Փ˂��Ă����
    void OnCollisionStay2D(Collision2D collision)
    {
        attackEnemy(collision);

    }
    // �Փ˂��I������Ƃ�
    void OnCollitionExit2D(Collision2D collision)
    {

    }

    // �v���C���[�֍U������
    void attackEnemy(Collision2D collision)
    {
        // �G�l�~�[�ȊO
        if (!collision.gameObject.TryGetComponent<EnemyController>(out var enemy)) return;
        //�@�^�C�}�[������
        if (0 < attackCoolDownTimer) return;

        enemy.Damage(Stats.Attack);
        attackCoolDownTimer = attackCoolDownTimerMax;
    }

    // �e��^�C�}�[�ݒ�

    void updateTimer()
    {
        if (0 < attackCoolDownTimer)
        {
            attackCoolDownTimer -= Time.deltaTime;
        }
    }




}
