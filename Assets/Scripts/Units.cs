using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Units
{

    // 秒数を0:00の文字列へ変換
  public static string GetTextTimer(float timer)
    {
        int seconds = (int)timer % 60;
        int minutes = (int)timer / 60;
        return minutes.ToString() + ":" + seconds.ToString("00");
    }


}
