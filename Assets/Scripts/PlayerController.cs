using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    //�ړ��ƃA�j���[�V����

    Rigidbody2D rigidbody2d;
    Animator animator;
    /// <summary>
    
    /// </summary>
    
    ////////////////////////////////////////�R�����g�A�E�g����
    // float moveSpeed = 10;

    // ���Ƃ�Intit�ŃZ�b�g�����
    ////////////////////////////////////////////�R�Ƃ��V���A���C�U�u����������
    GameSceneDirector sceneDirector;
    Slider sliderHP;
    Slider sliderXP;

    public CharacterStats Stats;

    // �U���̃N�[���_�E��
    float attackCoolDownTimer;
    float attackCoolDownTimerMax = 0.5f;


    // �K�vXP
    [SerializeField]List<int> levelRequiements;
    // �G�������u
    EnemySpawnerController enemySpawner;
    // ����
    public Vector2 Forward;
    // ���x���e�L�X�g
    Text TextLv;

    // ���ݑ������̕���
    public List<BaseWeaponSpawner> WeaponSpawners;


    public UnityAction OnLevelUp;
    public UnityAction OnGameOver;


    // Start is called before the first frame update
    void Start()
    {
        ////////////////////////////////////////////�ӂ��Ƃ�����

    }

    // Update is called once per frame
    void Update()
    {
        updateTimer();
        movePlayer();
        moveCamera();
        moveSliderHP();
    }

    ///
    // ������
    ////////////////////////////////////////////////////////////////�V�����֐������
    // �v���C���[���g���f�[�^�[�������擾����悤�Ɉ������擾����
    public void Init(GameSceneDirector sceneDirector,EnemySpawnerController enemySpawner,CharacterStats characterStats,Text textLv,Slider sliderHP,Slider sliderXP)
    {

        rigidbody2d = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();


        // �ϐ�������

        levelRequiements = new List<int>();
        WeaponSpawners = new List<BaseWeaponSpawner>();

        this.sceneDirector = sceneDirector;
        this.enemySpawner = enemySpawner;
        this.Stats = characterStats;
        this.TextLv = textLv;
        this.sliderHP = sliderHP;
        this.sliderXP = sliderXP;

        // �v���C���[�̌��� 
        Forward = Vector2.right;

        // �o���l��臒l���X�g�쐬
        levelRequiements.Add(0);
        for (int i = 1; i < 1000; i++) 
        {
            // ��O��臒l
            int prevxp = levelRequiements[i - 1];
            // 41�ȍ~�̓��x������16����
            int addxp = 12;

            // ���x��
            if(i==1)
            {
                addxp = 5;
            }
            else if(10 >= i)
            {
                addxp = 7;
            }
            else if(20 >= i)
            {
                addxp = 10;
            }

            levelRequiements.Add(prevxp+addxp);
        }

        // Lv2�̕K�v�o���l
        Stats.MaxXP = levelRequiements[1];

        // UI������
        setTextLv();
   
        setSliderHP();
        
        setSliderXP();
        // �X���C�_�[�̍��W
        moveSliderHP();

        // ����f�[�^�Z�b�g
        foreach(var item in Stats.DefaultWeaponIds)
        {
            addWeaponSpawner(item);
        }
    }
    
    // �v���C���[�̈ړ��Ɋւ��鏈��

    void movePlayer()
    {
        // �ړ��������
        Vector2 dir = Vector2.zero;

        //�@�Đ�����A�j���[�V����

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            dir += Vector2.up;
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            dir -= Vector2.up;
  
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            dir += Vector2.right;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            dir -= Vector2.right;
        }
        // ���͂��Ȃ���Δ�����
        if (Vector2.zero == dir) return;

        ////////////////////////////////////////////////////////////////�R�����g�A�E�g���������̃R�[�h�ɕύX
        // �v���C���[�ړ�
        // rigidbody2d.position += dir.normalized * moveSpeed * Time.deltaTime;
        rigidbody2d.position += dir.normalized * Stats.MoveSpeed * Time.deltaTime;


        // �A�j���[�V�������Đ�����(�A�j���[�^�p�ӂ��Ă��Ȃ��j
        //animator.SetTrigger(trigger);

        // �@�ړ��͈͐���
        // ��F�����AWorldStart���X���W���������Ȃ�����AX���W��WorldStart��X�̏��Ɉړ�������
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

        Forward = dir;
    }

    // �J�����ړ�

    // �J������WorldStar,End���ƃt�B�[���h�̈�Ԓ[�̈ʒu�ɂȂ��Ă��܂��ƁA�ʒu������Ȃ��B
    // TileMap�̍�����Map�̍��W�A�E��̍��W���擾���āA������������ƍ��ɂ݂͂ł��肵�Ȃ��悤�ɐ��䂵�Ă���i�|�W�V������߂��悤�ɂ��Ă���j
    void moveCamera()
    {
        Vector3 pos = transform.position;
        pos.z = Camera.main.transform.position.z;

        // �n�_
        // ��ʂ̍����[�̃^�C���̍��W�����J�����̍��W����������΁A��ʍ��[�̃^�C���̍��W�֖߂�
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
            OnGameOver?.Invoke();
        }

        if (0 > Stats.HP) Stats.HP = 0;
        setSliderHP();
    }

    // HP�X���C�_�[�̒l���X�V
    void setSliderHP()
    {
        sliderHP.maxValue = Stats.MaxHP;
        sliderHP.value = Stats.HP;
    }

    void setSliderXP()
    {
        sliderXP.maxValue = Stats.MaxXP;
        sliderXP.value = Stats.XP;

    }

    // �Փ˂����Ƃ�
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

    public void AddEx()
    {
        Stats.XP += 1;
        setSliderXP();
        if (levelRequiements[Stats.Lv] == Stats.XP)
        {
            Stats.XP = 0;
            Stats.Lv++;
            Stats.MaxXP = levelRequiements[Stats.Lv];
            OnLevelUp?.Invoke();
        }

        // UI������
        setTextLv();

        setSliderHP();

        setSliderXP();
    }

    // �v���C���[�֍U������
    void attackEnemy(Collision2D collision)
    {
        // �G�l�~�[�ȊO
        
        if (!collision.gameObject.TryGetComponent<EnemyController>(out var enemy)) return;
        //�@�^�C�}�[��������������I��
        if (0 < attackCoolDownTimer) return;

        // �_���[�W��^����
        enemy.Damage(Stats.Attack);
        attackCoolDownTimer = attackCoolDownTimerMax;
    }

    // �e��^�C�}�[�ݒ�
    // �U���̃N�[���^�C���̃^�C�}�[�J�n
    void updateTimer()
    {
        if (0 < attackCoolDownTimer)
        {
            attackCoolDownTimer -= Time.deltaTime;
        }
    }
    ///////////////////////////////////////////////////// �ǉ�
    // ���x���e�L�X�g�X�V
    void setTextLv()
    {
        TextLv.text = "LV" + Stats.Lv;
    }

    // �����ǉ�
    // ����̃��x���A�b�v�ƐV�K�����̗����Ŏg��
    public void addWeaponSpawner(int id)
    {
        // TODO �����ς݂Ȃ烌�x���A�b�v
        BaseWeaponSpawner spawner = WeaponSpawners.Find(item => item.stats.Id == id);

        if(spawner)
        {
            spawner.LevelUp();
            // ���x��UP
            return;
        }
        // �V�K�ǉ�
        spawner = WeaponSpawnerSettings.Instance.CreateWeaponSpawner(id, enemySpawner, transform);
        if(null ==spawner)
        {
            Debug.LogError("����f�[�^������܂���");
            return;
        }
        // �����ς݃��X�g�֒ǉ�
        WeaponSpawners.Add(spawner);

    }


}
