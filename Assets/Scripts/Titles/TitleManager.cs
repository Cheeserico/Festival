using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    bool wasStart;
    public void OnStartButton()
    {
        if (wasStart)
        {
            return;
        }
        wasStart = true;
        FadeManager.Instance.LoadScene("GameScene",1);
    }
}
