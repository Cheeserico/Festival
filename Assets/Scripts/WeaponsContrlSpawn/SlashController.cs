using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashController : BaseWeapon
{
    // BaseWepon��protect�Œ�`�����֐��Ȃ̂Œ�`���ĂȂ��Ă��p����̃N���X�ŌĂׂ�B�N���X���p��������

    // �g���K�[���Փ˂�����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        attackEnemy(collision);
    }


}
