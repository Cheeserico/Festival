using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmenController : BaseWeapon
{

    private void Update()
    {
        // ��]
        transform.Rotate(new Vector3(0, 0, -1000 * Time.deltaTime));
    }

    // �����Ɠ����蔻��̏�����ǉ�

    // �g���K�[���Փ˂�����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        attackEnemy(collision);
    }
}
