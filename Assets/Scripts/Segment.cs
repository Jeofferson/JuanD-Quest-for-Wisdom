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

    private Piece[] pieces;


    private void Awake()
    {

        pieces = gameObject.GetComponentsInChildren<Piece>();
        
    }


    public void Spawn()
    {

        gameObject.SetActive(true);

    }


    public void Despawn()
    {

        gameObject.SetActive(false);

    }


    public int SegmentIndex { get; set; }


}
