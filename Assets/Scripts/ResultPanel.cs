using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] Text timeText;
    [SerializeField] Text levelText;
    [SerializeField] Text moneyText;
    PlayerController player;

    public void Show(PlayerController playerController, GameSceneDirector gameSceneDirector)
    {
        gameObject.SetActive(true);
        timeText.text = Units.GetTextTimer(gameSceneDirector.GameTimer);
        levelText.text = playerController.Stats.Lv.ToString();
        player = playerController;
        moneyText.text = playerController.sumXp.ToString();
    }

    public void OnRanking()
    {
        naichilab.RankingLoader.Instance.SendScoreAndShowRanking(player.sumXp);
    }
}
