using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //�ړ��ƃA�j���[�V����

    Rigidbody2D rigidbody2d;
    Animator animator;
    float moveSpeed = 2;



    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();

    }

    // �v���C���[�̈ړ��Ɋւ��鏈��

    void movePlayer()
    {
        // �ړ��������
        Vector2 dir = Vector2.zero;

        //�@�Đ�����A�j���[�V����
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
        // ���͂��Ȃ���Δ�����
        if (Vector2.zero == dir) return;


        // �v���C���[�ړ�
        rigidbody2d.position += dir.normalized * moveSpeed * Time.deltaTime;

        // �A�j���[�V�������Đ�����(�A�j���[�^�p�ӂ��Ă��Ȃ��j
        //animator.SetTrigger(trigger);
    }







}
