using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameSceneDirector : MonoBehaviour
{
    // タイルマップ
    [SerializeField] GameObject grid;
    [SerializeField] Tilemap tilemapCollider;

    // マップ全体座標

    public Vector2 TileMapStart;
    public Vector2 TileMapEnd;
    public Vector2 WorldStart;
    public Vector2 WorldEnd;


    // EnemyContllerで呼び出せすため
    // GameSceneDirectorを介してプレイヤーのオブジェクトにアクセスできるようにする


    public PlayerController Player;
    
    [SerializeField] Transform parentTextDamage;
    [SerializeField] GameObject prefabTextDamage;

    // タイマー
    [SerializeField] Text textTimer;
    public float GameTimer;
    public float OldSeconds;

    // 敵生成
    [SerializeField] EnemySpawnerController enemySpawner;


    private void Start()
    {
        // 初期設定
        OldSeconds = -1;
        enemySpawner.Init(this, tilemapCollider);

        //　カメラの移動できる範囲

        foreach (Transform item in grid.GetComponentInChildren<Transform>())
        {
            // 子供のポジションを割り出して、
            // 開始位置（一番左下端のタイルの座標を割り出す）
            if (TileMapStart.x > item.position.x)
            {
                TileMapStart.x = item.position.x;
            }

            if (TileMapStart.y > item.position.y)
            {
                TileMapStart.y = item.position.y;
            }

            // 終了位置（一番右上端のタイルの座標を割り出す）
            if (TileMapEnd.x < item.position.x)
            {
                TileMapEnd.x = item.position.x;
            }

            if (TileMapEnd.y < item.position.y)
            {
                TileMapEnd.y = item.position.y;
            }

            // カメラのサイズとアスペクト比から画面のサイズを割り出す
            // 画面縦半分の描画範囲（デフォルトで５タイル）
            float cameraSize = Camera.main.orthographicSize;

            // 画面縦横比（16：9想定） 
            float aspect = (float)Screen.width / (float)Screen.height;

            // プレイヤーの移動できる範囲（アスペクト比率とカメラのサイズを取得して、WorldStart：左下端　WorldEnd：右上端を取得する）
            // 
            WorldStart = new Vector2(TileMapStart.x - cameraSize * aspect, TileMapStart.y - cameraSize);
            WorldEnd = new Vector2(TileMapEnd.x + cameraSize * aspect, TileMapEnd.y + cameraSize);
        }
    }

    private void Update()
    {
        // ゲームタイマー更新
        UpdateGameTimer();
    }

    // プレハブからTextDamageControllerで作ったコントローラの関数を呼び出す
    // ダメージ表示
    public void DispDamege(GameObject target, float damege)
    {
        //　プレハブを生成する
        GameObject obj = Instantiate(prefabTextDamage, parentTextDamage);
        // プレハブのテキストを書き換えている
        obj.GetComponent<TextDamageController>().Init(target, damege);
    }

    //　ゲームタイマー
    void UpdateGameTimer()
    {
        GameTimer += Time.deltaTime;

        // 前回と秒数が同じなら処理をしない
        int seconds = (int)GameTimer % 60;
        if (seconds == OldSeconds) return;

        textTimer.text = Units.GetTextTimer(GameTimer);
        OldSeconds = seconds;
    }



}
