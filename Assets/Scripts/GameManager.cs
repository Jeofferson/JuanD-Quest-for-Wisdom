using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{


    private bool hasStartedRunning;

    private PlayerMotor playerMotor;

    // UI Fields
    private float score;
    public TextMeshProUGUI textScore;

    private float life;
    public TextMeshProUGUI textLife;


    private void Awake()
    {

        Instance = this;
        UpdateStats();

        playerMotor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();

    }


    private void Update()
    {

        if (MobileInput.Instance.Tap && !hasStartedRunning)
        {

            hasStartedRunning = true;
            playerMotor.StartRunning();

        }

    }


    private void UpdateStats()
    {

        textScore.text = score.ToString();
        textLife.text = life.ToString();
        
    }


    public static GameManager Instance { set; get; }


}
