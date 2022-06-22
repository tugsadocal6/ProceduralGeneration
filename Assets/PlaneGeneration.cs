using System.Collections;
using UnityEngine;

public class PlaneGeneration : MonoBehaviour
{
    [SerializeField] private GameObject plane;
    [SerializeField] private GameObject player;

    private const int radius = 5;
    private const int planeOffset = 10;

    private Vector3 startPos = Vector3.zero;

    private int XPlayerMove => (int)(player.transform.position.x - startPos.x);
    private int ZPlayerMove => (int)(player.transform.position.z - startPos.z);

    private int XPlayerLocation => (int)Mathf.Floor(player.transform.position.x / planeOffset) * planeOffset;
    private int ZPlayerLocation => (int)Mathf.Floor(player.transform.position.z / planeOffset) * planeOffset;

    private Hashtable tilePlane = new Hashtable();

    // Update is called once per frame
    void Update()
    {
        if (startPos == Vector3.zero)
            for (int x = -radius; x < radius; x++)
                for (int z = 0; z < radius; z++)
                {
                    Vector3 pos = new Vector3((x * planeOffset + XPlayerLocation),
                        0,
                        (z * planeOffset + ZPlayerLocation));

                    if (!tilePlane.Contains(pos))
                    {
                        GameObject _plane = Instantiate(plane, pos, Quaternion.identity);
                        tilePlane.Add(pos, _plane);
                    }
                }

        if (HasPlayerMoved())
            for (int x = -radius; x < radius; x++)
                for (int z = 0; z < radius; z++)
                {
                    Vector3 pos = new Vector3((x * planeOffset + XPlayerLocation),
                        0,
                        (z * planeOffset + ZPlayerLocation));

                    if (!tilePlane.Contains(pos))
                    {
                        GameObject _plane = Instantiate(plane, pos, Quaternion.identity);
                        tilePlane.Add(pos, _plane);
                    }
                }
    }

    private bool HasPlayerMoved()
    {
        if (Mathf.Abs(XPlayerMove) >= planeOffset || Mathf.Abs(ZPlayerMove) >= planeOffset)
            return true;
        return false;
    }
}
