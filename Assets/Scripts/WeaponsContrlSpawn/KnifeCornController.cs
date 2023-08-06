using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeCornController : BaseWeapon
{
    // 時間差生成で進行方向に角度を向ける

    // Start is called before the first frame update
    void Start()
    {
        // 角度に変換する
        // Atn2関数（アークタンジェント関数）
        float angle = Mathf.Atan2(forwared.y, forwared.x) * Mathf.Rad2Deg;
        // 角度を代入
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    // Update is called once per frame
    void Update()
    {
        rigidbody2d.position += forwared.normalized * stats.MoveSpeed * Time.deltaTime;
    }

    // トリガーが衝突した時
    private void OnTriggerEnter2D(Collider2D collision)
    {
        attackEnemy(collision);
    }




}
