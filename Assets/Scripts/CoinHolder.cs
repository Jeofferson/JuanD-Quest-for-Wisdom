using UnityEngine;

public class CoinHolder : MonoBehaviour
{

    private StatsManager statsManager;

    private void Awake()
    {
        statsManager = FindObjectOfType<StatsManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Player":
                statsManager.AddCoin();
                Destroy(this.gameObject);
                break;
        }
    }

}
