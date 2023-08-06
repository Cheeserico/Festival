using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashController : BaseWeapon
{
    // BaseWeponにprotectで定義した関数なので定義してなくても継承先のクラスで呼べる。クラスを継承したら

    // トリガーが衝突した時
    private void OnTriggerEnter2D(Collider2D collision)
    {
        attackEnemy(collision);
    }


}
