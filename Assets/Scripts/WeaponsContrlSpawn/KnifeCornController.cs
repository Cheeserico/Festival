using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeCornController : BaseWeapon
{
    // ���ԍ������Ői�s�����Ɋp�x��������

    // Start is called before the first frame update
    void Start()
    {
        // �p�x�ɕϊ�����
        // Atn2�֐��i�A�[�N�^���W�F���g�֐��j
        float angle = Mathf.Atan2(forwared.y, forwared.x) * Mathf.Rad2Deg;
        // �p�x����
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
    // Update is called once per frame
    void Update()
    {
        rigidbody2d.position += forwared.normalized * stats.MoveSpeed * Time.deltaTime;
    }

    // �g���K�[���Փ˂�����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        attackEnemy(collision);
    }




}
