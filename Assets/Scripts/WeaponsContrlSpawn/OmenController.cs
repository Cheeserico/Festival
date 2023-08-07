using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmenController : BaseWeapon
{
    int hitCount;
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
