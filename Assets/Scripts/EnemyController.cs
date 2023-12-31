using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public CharacterStats Stats;

    [SerializeField] GameSceneDirector sceneDirector;
    [SerializeField] Xp xp;
    [SerializeField] HealItem healPrefab;
    Rigidbody2D rigidbody2d;

    // 攻撃のクールダウン
    float attackCoolDownTimer;
    float attackCoolDownTimerMax = 0.5f;

    Vector2 forward;
    // 状態
    enum State
    {
        Alive,
        Dead,
    }
    State state;



    // Start is called before the first frame update
    void Start()
    {
        // 作ってあったシングルトンのキャラクターセッティングから１００番目のデータを読み込む
        // Init(this.sceneDirector, CharacterSettings.Instance.Get(100));
    }

    // Update is called once per frame
    void Update()
    {
        updateTimer();
        moveEnemy();
    }

    //　初期化

    public void Init(GameSceneDirector sceneDirector, CharacterStats characterStats)
    {

        this.sceneDirector = sceneDirector;
        this.Stats = characterStats;

        rigidbody2d = GetComponent<Rigidbody2D>();

        // アニメーション
        // ランダムで緩急をつける

        float random = Random.Range(0.8f, 1.2f);
        float speed = 1 / Stats.MoveSpeed * random;

        // using DG.Tweening;
        // サイズ
        float addx = 0.3f;
        float x = addx * random;
        transform.DOScale(x, speed)
            .SetRelative()
            .SetLoops(-1, LoopType.Yoyo).SetLink(gameObject);

        //　回転
        float addz = 10f;
        float z = Random.Range(-addz, addz) * random;

        // 初期値
        // Rotate関数は指定した角度を加算するのに対し、eulerAngles変数は指定した座標に変換
        //Vector3 rotation = transform.rotation.eulerAngles;
        // rotation.z = z;

        //　目標値
        // transform.eulerAngles = rotation;
        //transform.DORotate(new Vector3(0, 0, -z), speed).SetLoops(-1, LoopType.Yoyo);

        // 進む方向
        PlayerController player = sceneDirector.Player;
        Vector2 dir = player.transform.position - transform.position;
        forward = dir;


        state = State.Alive;
    }

    // プレイヤーを追いかける
    void moveEnemy()
    {
        if (State.Alive != state) return;
        // 目的がプレイヤーなら進む方向を更新する
        if (MoveType.TargetPlayer == Stats.MoveType)
        {
            PlayerController player = sceneDirector.Player;
            Vector2 dir = player.transform.position - transform.position;
            forward = dir;
        }

        // 移動
        // normalized(ベクトルを正規化する？）
        rigidbody2d.position += forward.normalized * Stats.MoveSpeed * Time.deltaTime;
    }
    
    // 各種タイマー設定
            
    void updateTimer()
    {
        if(0 < attackCoolDownTimer)
        {
            attackCoolDownTimer -= Time.deltaTime;
        }
                
        // 生存時間が設定されていたらタイマー消化        
        if(0 < Stats.AliveTime)
        {
            Stats.AliveTime -= Time.deltaTime;
           
            if (0 > Stats.AliveTime) setDead(false);
        }
    }

            
    // 敵が死んだときに呼び出される        
    void setDead(bool createXP = true)
            
    {
        if (State.Alive != state) return;
        // 物理挙動を停止
        rigidbody2d.simulated = false;
        // アニメーションを停止
        transform.DOKill();
        // 縦に潰れるアニメーション
        transform.DOScaleY(0, 0.5f).SetLink(gameObject).OnComplete(() => Destroy(gameObject));
        // 経験値を作成
        if(createXP)
        {
            // TODO 経験値生成
            if (Random.Range(0,100) < 4)
            {
                Instantiate(healPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(xp, transform.position, Quaternion.identity);
            }
        }
        state = State.Dead;
    }
    // 衝突したとき
    void OnCollitionEnter2D(Collision2D collition)
    {
        attackPlayer(collition);
    }
    // 衝突している間
    void OnCollisionStay2D(Collision2D collision)
    {
        attackPlayer(collision);

    }
    // 衝突が終わったとき
    void OnCollitionExit2D(Collision2D collision)
    {

    }

    // プレイヤーへ攻撃する
    void attackPlayer(Collision2D collision)
    {
        // プレイヤー以外
        if(!collision.gameObject.TryGetComponent<PlayerController>(out var player)) return;
        //　タイマー未消化
        if (0 < attackCoolDownTimer) return;
        // 非アクティブ
        if (State.Alive != state) return;

        player.Damage(Stats.Attack);
        attackCoolDownTimer = attackCoolDownTimerMax;
    }

    //　ダメージ
    public float Damage(float attack)
    {
        // 非アクティブ
        if (State.Alive != state) return 0;

        float damage = Mathf.Max(0, attack - Stats.Defense);
        Stats.HP -= damage;

        // ダメージ表示
        sceneDirector.DispDamege(gameObject, damage);

        //TODO 消滅
        if(0>Stats.HP)
        {
            setDead();
        }
        SoundManager.Instance.PlaySE(SE.DonWeapon);

        // 計算後のダメージを返す
        return damage;
    }



}
