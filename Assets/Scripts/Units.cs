using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Units
{

    // �b����0:00�̕�����֕ϊ�
  public static string GetTextTimer(float timer)
    {
        int seconds = (int)timer % 60;
        int minutes = (int)timer / 60;
        return minutes.ToString() + ":" + seconds.ToString("00");
    }


}
