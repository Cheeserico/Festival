using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoyoController : BaseWeapon
{
    // ターゲット
    public EnemyController Target;



    // 生成時にあらかじめセットされているはずのターゲット方向から
    // 弓の先端がターゲットへ向くように角度を設定する
    void Start()
    {
        if (!Target)
        {
            return;
        }
        // 進行方向
        Vector2 forward = Target.transform.position - transform.position;
        // 角度に変換する
        // Atn2関数（アークタンジェント関数）
        float angle = Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg;
        // 角度を代入
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Update is called once per frame
   
    // ターゲットの生存チェックと移動の処理
    void Update()
    {
        if(!Target)
        {
            Destroy(gameObject);
            return;
                       
        }
        // 移動
        Vector2 forward = Target.transform.position - transform.position;
        rigidbody2d.position += forward.normalized * stats.MoveSpeed * Time.deltaTime;
    }

    // トリガーが衝突した時
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 敵以外
        if(!collision.gameObject.TryGetComponent<EnemyController>(out var enemy)) return;

        // 通常ダメージ
        float attack = stats.Attack;

        attackEnemy(collision, attack);
        Target = null;

    }


}
