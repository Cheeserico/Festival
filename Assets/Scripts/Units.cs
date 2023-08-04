using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class Units
{

    // �b����0:00�̕�����֕ϊ�
  public static string GetTextTimer(float timer)
    {
        int seconds = (int)timer % 60;
        int minutes = (int)timer / 60;
        return minutes.ToString() + ":" + seconds.ToString("00");
    }
    // �����蔻��̂���^�C�����ǂ������ׂ�
    public static bool IsCollisionTile(Tilemap tilemapCollision, Vector2 position)
    {
        // �Z���ʒu�ɕϊ�
        Vector3Int cellPosition = tilemapCollision.WorldToCell(position);

        // �����蔻�肠��
        if(tilemapCollision.GetTile(cellPosition))
        {
            return true;
        }

        return false;
    }

}
