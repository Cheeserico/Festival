using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class Units
{

    // 秒数を0:00の文字列へ変換
  public static string GetTextTimer(float timer)
    {
        int seconds = (int)timer % 60;
        int minutes = (int)timer / 60;
        return minutes.ToString() + ":" + seconds.ToString("00");
    }
    // 当たり判定のあるタイルかどうか調べる
    public static bool IsCollisionTile(Tilemap tilemapCollision, Vector2 position)
    {
        // セル位置に変換
        Vector3Int cellPosition = tilemapCollision.WorldToCell(position);

        // 当たり判定あり
        if(tilemapCollision.GetTile(cellPosition))
        {
            return true;
        }

        return false;
    }

}
