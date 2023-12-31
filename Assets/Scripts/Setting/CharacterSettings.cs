using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 右クリックメニューに表示する、filenameはデフォルトのファイル名
[CreateAssetMenu(fileName = "CharacterSettings",menuName ="ScriptableObjects/CharacterSettings")]


public class CharacterSettings : ScriptableObject
{
    // キャラクターデータ
    public List<CharacterStats> datas;

    // 外部からアクセスされるためのプロパティ（Getcomponentが要らない）シングルトン
    static CharacterSettings instance;
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

    // 敵生成
    public EnemyController CreateEnemy(int id, GameSceneDirector sceneDirector,Vector3 position)
    {
        // ステータス取得
        CharacterStats stats = instance.Get(id);
        // オブジェクト
        GameObject obj = Instantiate(stats.Prefab, position, Quaternion.identity);
        //データセット
        EnemyController ctrl = obj.GetComponent<EnemyController>();
        ctrl.Init(sceneDirector, stats);

        return ctrl;
    }

    // プレイヤー生成
    public PlayerController CreatePlayer(int id, GameSceneDirector sceneDirector, EnemySpawnerController enemySpawner, Text textLv, Slider sliderHP, Slider sliderXP)

    {
        // ステータス取得
        CharacterStats stats = Instance.Get(id);
        // オブジェクト作成
        GameObject obj = Instantiate(stats.Prefab, Vector3.zero, Quaternion.identity);

        // データセット
        PlayerController ctrl = obj.GetComponent<PlayerController>();
        ctrl.Init(sceneDirector, enemySpawner, stats, textLv, sliderHP, sliderXP);

        return ctrl;
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
        public GameObject Prefab;
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



    

