using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TextDamageController : MonoBehaviour
{
    float destroyTime = 1;
    GameObject target;
        // Start is called before the first frame update
    void Start()
    {
        // �e�L�X�g���Z�b�g���ďꏊ���X�V���Ėc���ŏ����Ă����Ƃ�����A�̏��������s����
        transform.DOScale(new Vector2(1, 1), destroyTime / 2)
              .SetRelative()
              .OnComplete(() =>
        {
            transform.DOScale(new Vector2(0, 0), destroyTime / 2)
            .OnComplete(() => Destroy(gameObject));
        });
            
    }

    // Update is called once per frame
    void Update()
    {
        if (!target) return;
        //���[���h���W���X�N���[�����W�ɕϊ�
        //�@�e�L�X�g�ʒu�X�V
        Vector3 pos = RectTransformUtility.WorldToScreenPoint(Camera.main, target.transform.position);
        transform.position = pos;
    }
    
    // �������ɌĂ΂��֐�
    // ������
    public void Init(GameObject target, float damage, bool heal = false)
    {
        this.target = target;
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        text.text = "" + (int)damage;
        
        // �v���C���[�̃_���[�W�͐ԕ\��
        if(target.GetComponent<PlayerController>())
        {
            if (heal)
            {
                text.color = Color.green;
            }
            else
            {
                text.color = Color.red;
            }
        }

    }




}
