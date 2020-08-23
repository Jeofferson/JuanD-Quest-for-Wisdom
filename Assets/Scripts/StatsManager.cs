using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour
{


    private const int SCORE_INCREASE_OVER_TIME = 1;
    private const int SCORE_INCREASE_BY_COIN = 50;

    private const int MAX_LIFE_VALUE = 30;

    private PlayerMotor playerMotor;

    private int score;
    public TextMeshProUGUI textScore;

    private int coin;
    public TextMeshProUGUI textCoin;

    private float life;
    public TextMeshProUGUI textLife;
    public Slider sliderLife;

    public Animator animatorStatsManager;


    private void Awake()
    {

        playerMotor = FindObjectOfType<PlayerMotor>();

        ResetStats();

    }


    private void ResetStats()
    {

        score = 0;
        coin = 0;
        life = MAX_LIFE_VALUE;

        UpdateScore();
        UpdateCoin();
        UpdateLife();

    }


    public void StartRunning()
    {

        animatorStatsManager.SetTrigger("Show");

        InvokeRepeating("RegulateScore", .1f, .1f);
        InvokeRepeating("RegulateLife", .1f, .1f);

    }


    private void RegulateScore()
    {

        score += SCORE_INCREASE_OVER_TIME;
        UpdateScore();

    }


    private void RegulateLife()
    {

        life -= .1f;
        UpdateLife();

        if (life <= 0)
        {

            Die();
            playerMotor.Die();

        }

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

        textLife.text = life.ToString("0");
        sliderLife.value = life;

    }


    public void Die()
    {

        CancelInvoke("RegulateScore");
        CancelInvoke("RegulateLife");

    }


}
