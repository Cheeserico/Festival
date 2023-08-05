using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public CharacterStats Stats;

    [SerializeField] GameSceneDirector sceneDirector;
    Rigidbody2D rigidbody2d;

    // �U���̃N�[���_�E��
    float attackCoolDownTimer;
    float attackCoolDownTimerMax = 0.5f;

    Vector2 forward;
    // ���
    enum State
    {
        Alive,
        Dead,
    }
    State state;



    // Start is called before the first frame update
    void Start()
    {
        // ����Ă������V���O���g���̃L�����N�^�[�Z�b�e�B���O����P�O�O�Ԗڂ̃f�[�^��ǂݍ���
        Init(this.sceneDirector, CharacterSettings.Instance.Get(100));
    }

    // Update is called once per frame
    void Update()
    {
        updateTimer();
        moveEnemy();
    }

    //�@������

    public void Init(GameSceneDirector sceneDirector, CharacterStats characterStats)
    {

        this.sceneDirector = sceneDirector;
        this.Stats = characterStats;

        rigidbody2d = GetComponent<Rigidbody2D>();

        // �A�j���[�V����
        // �����_���Ŋɋ}������

        float random = Random.Range(0.8f, 1.2f);
        float speed = 1 / Stats.MoveSpeed * random;

        // using DG.Tweening;
        // �T�C�Y
        float addx = 0.3f;
        float x = addx * random;
        transform.DOScale(x, speed)
            .SetRelative()
            .SetLoops(-1, LoopType.Yoyo);

        //�@��]
        float addz = 10f;
        float z = Random.Range(-addz, addz) * random;

        // �����l
        // Rotate�֐��͎w�肵���p�x�����Z����̂ɑ΂��AeulerAngles�ϐ��͎w�肵�����W�ɕϊ�
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.z = z;

        //�@�ڕW�l
        transform.eulerAngles = rotation;
        transform.DORotate(new Vector3(0, 0, -z), speed)
            .SetLoops(-1, LoopType.Yoyo);

        // �i�ޕ���
        PlayerController player = sceneDirector.Player;
        Vector2 dir = player.transform.position - transform.position;
        forward = dir;


        state = State.Alive;
    }

    // �v���C���[��ǂ�������
    void moveEnemy()
    {
        if (State.Alive != state) return;
        // �ړI���v���C���[�Ȃ�i�ޕ������X�V����
        if (MoveType.TargetPlayer == Stats.MoveType)
        {
            PlayerController player = sceneDirector.Player;
            Vector2 dir = player.transform.position - transform.position;
            forward = dir;
        }

        // �ړ�
        // normalized(�x�N�g���𐳋K������H�j
        rigidbody2d.position += forward.normalized * Stats.MoveSpeed * Time.deltaTime;
    }
    
    // �e��^�C�}�[�ݒ�
            
    void updateTimer()
    {
        if(0 < attackCoolDownTimer)
        {
            attackCoolDownTimer -= Time.deltaTime;
        }
                
        // �������Ԃ��ݒ肳��Ă�����^�C�}�[����        
        if(0 < Stats.AliveTime)
        {
            Stats.AliveTime -= Time.deltaTime;
           
            if (0 > Stats.AliveTime) setDead(false);
        }
    }

            
    // �G�����񂾂Ƃ��ɌĂяo�����        
    void setDead(bool createXP = true)
            
    {
        if (State.Alive != state) return;
        // �����������~
        rigidbody2d.simulated = false;
        // �A�j���[�V�������~
        transform.DOKill();
        // �c�ɒׂ��A�j���[�V����
        transform.DOScaleY(0, 0.5f).OnComplete(() => Destroy(gameObject));
        // �o���l���쐬
        if(createXP)
        {
            // TODO �o���l����
        }
        state = State.Dead;
    }
    // �Փ˂����Ƃ�
    void OnCollitionEnter2D(Collision2D collition)
    {
        attackPlayer(collition);
    }
    // �Փ˂��Ă����
    void OnCollisionStay2D(Collision2D collision)
    {
        attackPlayer(collision);

    }
    // �Փ˂��I������Ƃ�
    void OnCollitionExit2D(Collision2D collision)
    {

    }

    // �v���C���[�֍U������
    void attackPlayer(Collision2D collision)
    {
        // �v���C���[�ȊO
        if(!collision.gameObject.TryGetComponent<PlayerController>(out var player)) return;
        //�@�^�C�}�[������
        if (0 < attackCoolDownTimer) return;
        // ��A�N�e�B�u
        if (State.Alive != state) return;

        player.Damage(Stats.Attack);
        attackCoolDownTimer = attackCoolDownTimerMax;
    }

    //�@�_���[�W
    public float Damage(float attack)
    {
        // ��A�N�e�B�u
        if (State.Alive != state) return 0;

        float damage = Mathf.Max(0, attack - Stats.Defense);
        Stats.HP -= damage;

        // �_���[�W�\��
        sceneDirector.DispDamege(gameObject, damage);

        //TODO ����
        if(0>Stats.HP)
        {
            setDead();

        }
        // �v�Z��̃_���[�W��Ԃ�
        return damage;
    }



}
