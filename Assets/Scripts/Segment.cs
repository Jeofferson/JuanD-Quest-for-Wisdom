using UnityEngine;

public class Segment : MonoBehaviour
{

    public bool isTransitionSegment;

    public int length;

    public int beginY1;
    public int beginY2;
    public int beginY3;

    public int endY1;
    public int endY2;
    public int endY3;

    private PieceSpawner[] pieceSpawners;

    private void Awake()
    {
        pieceSpawners = GetComponentsInChildren<PieceSpawner>();

        for (int i = 0; i < pieceSpawners.Length; i++)
        {
            foreach (MeshRenderer meshRenderer in pieceSpawners[i].GetComponentsInChildren<MeshRenderer>())
            {
                meshRenderer.enabled = LevelManager.Instance.showCollider;
            }
        }
    }

    public void Spawn()
    {
        gameObject.SetActive(true);

        if (LevelManager.Instance.showVisual)
        {
            for (int i = 0; i < pieceSpawners.Length; i++)
            {
                pieceSpawners[i].Spawn();
            }
        }
    }

    public void Despawn()
    {
        gameObject.SetActive(false);

        if (LevelManager.Instance.showVisual)
        {
            for (int i = 0; i < pieceSpawners.Length; i++)
            {
                pieceSpawners[i].Despawn();
            }
        }
    }

    public int SegmentIndex { get; set; }

}
