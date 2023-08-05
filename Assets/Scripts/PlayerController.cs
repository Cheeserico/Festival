using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //移動とアニメーション

    Rigidbody2D rigidbody2d;
    Animator animator;
    /// <summary>
    
    /// </summary>
    
    ////////////////////////////////////////コメントアウトした
    // float moveSpeed = 10;

    // あとでIntitでセットされる
    ////////////////////////////////////////////３つともシリアライザブルを消した
    GameSceneDirector sceneDirector;
    Slider sliderHP;
    Slider sliderXP;

    public CharacterStats Stats;

    // 攻撃のクールダウン
    float attackCoolDownTimer;
    float attackCoolDownTimerMax = 0.5f;


    // 必要XP
    List<int> levelRequiements;
    // 敵生成装置
    EnemySpawnerController enemySpawner;
    // 向き
    public Vector2 Forward;
    // レベルテキスト
    Text TextLv;


    // Start is called before the first frame update
    void Start()
    {
        ////////////////////////////////////////////ふたつとも消す
        // rigidbody2d = GetComponent<Rigidbody2D>();
        // animator = GetComponent<Animator>();

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
    // 初期化
    ////////////////////////////////////////////////////////////////新しく関数作った
    // プレイヤーが使うデーターだけを取得するように引数を取得する
    public void Init(GameSceneDirector sceneDirector,EnemySpawnerController enemySpawner,CharacterStats characterStats,Text textLv,Slider sliderHP,Slider sliderXP)
    {
        // 変数初期化

        levelRequiements = new List<int>();

        this.sceneDirector = sceneDirector;
        this.enemySpawner = enemySpawner;
        this.Stats = characterStats;
        this.TextLv = textLv;
        this.sliderHP = sliderHP;
        this.sliderXP = sliderXP;

        // プレイヤーの向き 
        Forward = Vector2.right;

        // 経験値の閾値リスト作成
        levelRequiements.Add(0);
        for(int i = 1; 1< 1000; i++)
        {
            // 一つ前の閾値
            int prevxp = levelRequiements[i - 1];
            // 41以降はレベル毎に16ずつ
            int addxp = 16;

            // レベル
            if(i==1)
            {
                addxp = 5;
            }
            else if(20 >= i)
            {
                addxp = 10;
            }
            else if(40 >= i)
            {
                addxp = 13;
            }

            levelRequiements.Add(prevxp);
        }

        // Lv2の必要経験値
        Stats.MaxXP = levelRequiements[1];

        // UI初期化
        setTextLv();
   
        setSliderHP();
        
        setSliderXP();
        // スライダーの座標
        moveSliderHP();

        // TODO 武器データセット
    }
    
    // プレイヤーの移動に関する処理

    void movePlayer()
    {
        // 移動する方向
        Vector2 dir = Vector2.zero;

        //　再生するアニメーション
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
        // 入力がなければ抜ける
        if (Vector2.zero == dir) return;

        ////////////////////////////////////////////////////////////////コメントアウトした所下のコードに変更
        // プレイヤー移動
        // rigidbody2d.position += dir.normalized * moveSpeed * Time.deltaTime;
        rigidbody2d.position += dir.normalized * Stats.MoveSpeed * Time.deltaTime;


        // アニメーションを再生する(アニメータ用意していない）
        //animator.SetTrigger(trigger);

        // 　移動範囲制御
        // 例：もし、WorldStartよりX座標が小さくなったら、X座標をWorldStartのXの所に移動させる
        // 始点
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

        // 終点
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

    // カメラ移動

    // カメラはWorldStar,Endだとフィールドの一番端の位置になってしまうと、位置があわない。
    // TileMapの左下のMapの座標、右上の座標を取得して、そこからもっと左にはみでたりしないように制御している（ポジションを戻すようにしている）
    void moveCamera()
    {
        Vector3 pos = transform.position;
        pos.z = Camera.main.transform.position.z;

        // 始点
        // 画面の左下端のタイルの座標よりもカメラの座標が小さければ、画面左端のタイルの座標へ戻す
        if (pos.x < sceneDirector.TileMapStart.x)
        {
            pos.x = sceneDirector.TileMapStart.x;
        }

        if (pos.y < sceneDirector.TileMapStart.y)
        {
            pos.y = sceneDirector.TileMapStart.y;
        }
        // 終点
        if (sceneDirector.TileMapEnd.x < pos.x)
        {
            pos.x = sceneDirector.TileMapEnd.x;
        }

        if (sceneDirector.TileMapEnd.y < pos.y)
        {
            pos.y = sceneDirector.TileMapEnd.y;
        }

        // カメラの位置を更新する
        Camera.main.transform.position = pos;
    }

    // HPスライダー移動
    void moveSliderHP()
    {
        //ワールド座標をスクリーン座標に変換
        Vector3 pos = RectTransformUtility.WorldToScreenPoint(Camera.main, transform.position);
        pos.y -= 50;
        sliderHP.transform.position = pos;

    }
    // ダメージを受けたらGameSceneDirectorのダメージ関数を呼び出す処理
    // ダメージ
    public void Damage(float attack)
    {
        //非アクティブなら抜ける
        if (!enabled) return;
        //　最大値
        float damage = Mathf.Max(0, attack - Stats.Defense);

        Stats.HP -= damage;

        // ダメージ表示
        sceneDirector.DispDamege(gameObject, damage);

        //TODO ゲームオーバー
        if(0>Stats.HP)
        {

        }

        if (0 > Stats.HP) Stats.HP = 0;
        setSliderHP();
    }

    // HPスライダーの値を更新
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

    // 衝突したとき
    void OnCollitionEnter2D(Collision2D collition)
    {
        attackEnemy(collition);
    }
    // 衝突している間
    void OnCollisionStay2D(Collision2D collision)
    {
        attackEnemy(collision);

    }
    // 衝突が終わったとき
    void OnCollitionExit2D(Collision2D collision)
    {

    }

    // プレイヤーへ攻撃する
    void attackEnemy(Collision2D collision)
    {
        // エネミー以外
        if (!collision.gameObject.TryGetComponent<EnemyController>(out var enemy)) return;
        //　タイマー未消化だったら終了
        if (0 < attackCoolDownTimer) return;

        // ダメージを与える
        enemy.Damage(Stats.Attack);
        attackCoolDownTimer = attackCoolDownTimerMax;
    }

    // 各種タイマー設定
    // 攻撃のクールタイムのタイマー開始
    void updateTimer()
    {
        if (0 < attackCoolDownTimer)
        {
            attackCoolDownTimer -= Time.deltaTime;
        }
    }
    ///////////////////////////////////////////////////// 追加
    // レベルテキスト更新
    void setTextLv()
    {
        TextLv.text = "LV" + Stats.Lv;
    }



}
