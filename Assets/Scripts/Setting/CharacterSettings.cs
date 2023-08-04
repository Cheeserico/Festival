using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 右クリックメニューに表示する、filenameはデフォルトのファイル名
[CreateAssetMenu(fileName = "CharacterSettings",menuName = "ScriptableObjects/CharacterSettings")]


public class CharacterSettings : ScriptableObject
{
    // キャラクターデータ
    public List<CharacterStats> datas;

    static CharacterSettings instance;


    // 外部からアクセスされるためのプロパティ（Getcomponentが要らない）シングルトン
    public static CharacterSettings Instance
    {
        get
        {
            // 最初はすべてinstanceされていない。インスタンスがなければ
            if(!instance)
            {
                // CharacterSettingstという名前のアセットを取ってくる
                // Resources.Lodeしたらプロジェクトに存在する「Resourcesという名前のフォルダーの中身」から色々と取得できる
                instance = Resources.Load<CharacterSettings>(nameof(CharacterSettings));
            }

            // 値を返す
            return instance;
        }
    }

    // リストのIDからデータを検索する
    public CharacterStats Get(int id)
    {
        return (CharacterStats)datas.Find(item => item.Id == id).GetCopy();
    }


}
   
    //　敵の動き
    
public enum MoveType
    
{
        //　プレイヤーに向かって進む
        TargetPlayer,

        //　一方向に進む
        TargetDirection
}
    [Serializable]  
public class CharacterStats : BaseStats
    
{
        // キャラクターのプレハブ
        public GameObject prefab;
        // 初期装備武器ID
        public List<int> DefaultWeaponIds;
        // 装備可能武器ID
        public List<int> UsableWeponIds;
        // 装備可能数
        public int UsableWeponMax;
        // 移動タイプ
        public MoveType MoveType;

        // [ToDo] アイテム追加    
}



    

