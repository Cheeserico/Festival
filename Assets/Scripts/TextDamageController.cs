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
        // テキストをセットして場所を更新して膨らんで消えていくという一連の処理を実行する
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
        //ワールド座標をスクリーン座標に変換
        //　テキスト位置更新
        Vector3 pos = RectTransformUtility.WorldToScreenPoint(Camera.main, target.transform.position);
        transform.position = pos;
    }
    
    // 生成時に呼ばれる関数
    // 初期化
    public void Init(GameObject target, float damage, bool heal = false)
    {
        this.target = target;
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();
        text.text = "" + (int)damage;
        
        // プレイヤーのダメージは赤表示
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
