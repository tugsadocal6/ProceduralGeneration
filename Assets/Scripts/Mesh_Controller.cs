using UnityEngine;

public class Mesh_Controller : MonoBehaviour
{
    [Range(1.5f, 5f)]
    [SerializeField] private float radius = 2f;

    [Range(0.5f, 5f)]
    [SerializeField] private float deformationStength = 2f;

    private Mesh mesh;

    private Vector3[] verticies;
    private Vector3[] modifiedVerts;

    private void Awake() => mesh = GetComponentInChildren<MeshFilter>().mesh;

    // Start is called before the first frame update
    private void Start()
    {
        verticies = mesh.vertices;
        modifiedVerts = mesh.vertices;
    }

    private void RecalculateMesh()
    {
        mesh.vertices = modifiedVerts;
        GetComponentInChildren<MeshCollider>().sharedMesh = mesh;
        mesh.RecalculateNormals();
    }

    // Update is called once per frame
    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            for (int v = 0; v < modifiedVerts.Length; v++)
            {
                Vector3 distance = modifiedVerts[v] - hit.point;
                float smoothingFactor = 2f;
                float force = deformationStength / (1f + hit.point.sqrMagnitude);

                if (distance.sqrMagnitude < radius)
                {
                    if (Input.GetMouseButton(0))
                        modifiedVerts[v] = modifiedVerts[v] + (Vector3.up * force) / smoothingFactor;
                    else if (Input.GetMouseButton(1))
                        modifiedVerts[v] = modifiedVerts[v] + (Vector3.down * force) / smoothingFactor;
                }
            }
            RecalculateMesh();
    }
}
