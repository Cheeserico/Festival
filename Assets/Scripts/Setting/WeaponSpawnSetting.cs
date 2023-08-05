using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 右クリックメニューに表示する、filenameはデフォルトのファイル名
[CreateAssetMenu(fileName = "WeaponSpawnerSettings", menuName = "ScriptableObjects/WeaponSpawnerSettings")]


public class WeaponSpawnerSettings : ScriptableObject
{

    // キャラクターデータ
    // キャラクターセッティングのコードを流用
    
    public List<WeaponSpawnerStats> datas;

    // 外部からアクセスされるためのプロパティ（Getcomponentが要らない）シングルトン
    static WeaponSpawnerSettings instance;
    public static WeaponSpawnerSettings Instance
    {
        get
        {
            // 最初はすべてinstanceされていない。インスタンスがなければ
            if (!instance)
            {
                // CharacterSettingstという名前のアセットを取ってくる
                // Resources.Lodeしたらプロジェクトに存在する「Resourcesという名前のフォルダーの中身」から色々と取得できる
                instance = Resources.Load<WeaponSpawnerSettings>(nameof(WeaponSpawnerSettings));
            }
            // 値を返す
            return instance;
        }
    }

    // リストのIDからデータを検索する
    public WeaponSpawnerStats Get(int id, int lv)
    {
        // 武器の設定は同じIDのの違うレベルを設定出来るようにしたいので
        // データを探すGet関数は引数で指定されたレベルに最も近いデータを探して返すような機能を実装
        // 指定されたレベルのデータがなければ一番高いレベルのデータを返す
        WeaponSpawnerStats ret = null;

        foreach(var item in datas)
        {
            if (id != item.Id) continue;

            // 指定レベルと一致
            if(lv == item.Lv)
            {
                return (WeaponSpawnerStats)item.GetCopy();
            }
            // 狩りのデータがセットされていないか、それを超えるレベルがあったら入れ替える
            if(null == ret)
            {
                ret = item;
            }
            // 探しているレベルより下で、暫定データより大きい

            else if(item.Lv < lv && ret.Lv < item.Lv)
            {
                ret = item;
            }
        }

        return (WeaponSpawnerStats)ret.GetCopy();
    }

    ///////////////////////////////////////////////////////////////////////////////////////////////////////////
    
    // 作成
    // 武器生成装置を生成する関数
    public BaseWeaponSpawner CreateWeaponSpawner(int id, EnemySpawnerController enemySpawner, Transform parent = null)
    {

        // データ取得
        WeaponSpawnerStats stats = instance.Get(id, 1);
        // オブジェクト作成
        ///////////////////////////////////////
        GameObject obj　= Instantiate(stats.PrefabSpawner, parent);
        // データセット
        BaseWeaponSpawner spawner = obj.GetComponent<BaseWeaponSpawner>();

        spawner.Init(enemySpawner, stats);
        return spawner;
    }
    

}


// 武器生成装置
[System.Serializable]
public class WeaponSpawnerStats : BaseStats
{
    // 生成装置のプレハブ
    public GameObject PrefabSpawner;
    // 武器のアイコン
    public Sprite Icon;
    // レベルアップ時に追加されるアイテムID
    public int LevelUpItemId;
    // 一度に生成する数
    public float SpawnCount;
    // 生成タイマー
    public float SpawnTimerMin;
    public float SpawnTimerMax;

    // 生成時間取得
    public float GetRandomSpawnTimer()
    {
        return Random.Range(SpawnTimerMin, SpawnTimerMax);
    }
    
    
    // TODOアイテムを追加

}