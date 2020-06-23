using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{


    private bool hasStartedRunning;

    private PlayerMotor playerMotor;
    private CameraMotor cameraMotor;

    // UI Fields
    private float score;
    public TextMeshProUGUI textScore;

    private float life;
    public TextMeshProUGUI textLife;


    private void Awake()
    {

        Instance = this;

        playerMotor = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMotor>();
        cameraMotor = FindObjectOfType<CameraMotor>();

        UpdateStats();

    }


    private void Update()
    {

        if (MobileInput.Instance.Tap && !hasStartedRunning)
        {

            hasStartedRunning = true;

            playerMotor.StartRunning();
            cameraMotor.hasStartedRunning = true;

        }

    }


    private void UpdateStats()
    {

        textScore.text = score.ToString();
        textLife.text = life.ToString();
        
    }


    public static GameManager Instance { set; get; }


}
