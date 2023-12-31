using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmenController : BaseWeapon
{
    int hitCount;
    private void Update()
    {
        // 回転
        transform.Rotate(new Vector3(0, 0, -1000 * Time.deltaTime));
    }

    // 動きと当たり判定の処理を追加

    // トリガーが衝突した時
    private void OnTriggerEnter2D(Collider2D collision)
    {
        attackEnemy(collision);
        if (collision.GetComponent<EnemyController>())
        {
            hitCount++;
            if (hitCount >= 3)
            {
                Destroy(gameObject);
            }
        }

    }
}
