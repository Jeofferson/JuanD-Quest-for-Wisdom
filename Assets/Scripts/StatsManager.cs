﻿using TMPro;
using UnityEngine;

public class StatsManager : MonoBehaviour
{


    private const int SCORE_INCREASE_OVER_TIME = 1;
    private const int SCORE_INCREASE_BY_COIN = 50;

    private PlayerMotor playerMotor;

    private int score;
    public TextMeshProUGUI textScore;

    private int coin;
    public TextMeshProUGUI textCoin;

    private int life;
    public TextMeshProUGUI textLife;


    private void Awake()
    {

        playerMotor = FindObjectOfType<PlayerMotor>();

        ResetStats();

    }


    private void ResetStats()
    {

        score = 0;
        coin = 0;
        life = 30;

        UpdateScore();
        UpdateCoin();
        UpdateLife();

    }


    public void StartRunning()
    {

        InvokeRepeating("RegulateScore", .1f, .1f);
        InvokeRepeating("RegulateLife", 1f, 1f);

    }


    private void RegulateScore()
    {

        score += SCORE_INCREASE_OVER_TIME;
        UpdateScore();

    }


    private void RegulateLife()
    {

        life--;

        if (life <= 0)
        {

            playerMotor.Die();

        }

        UpdateLife();

    }


    public void AddCoin()
    {

        coin++;
        score += SCORE_INCREASE_BY_COIN;
        UpdateCoin();

    }


    private void UpdateScore()
    {

        textScore.text = score.ToString();

    }


    private void UpdateCoin()
    {

        textCoin.text = coin.ToString();

    }


    private void UpdateLife()
    {

        textLife.text = life.ToString();

    }


}