using System;
using System.Linq;
using UnityEditor;
using UnityEngine;


[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
public class GenerateMesh : MonoBehaviour
{
    public string saveName = "Models/curvedPlatform";
    
    [Header("Mesh Parameters")] 
    public int resolutionX = 10;
    public int resolutionZ = 10; // number of verts in x and z directions
    public float widthX = 5f, widthZ = 5f;
    
    [Header("Function Offsets")] 
    public float offsetX;
    public float offsetY;
    public float offsetZ;
   
    [Header("Function Scales")] 
    public float scaleX;
    public float scaleY;
    public float scaleZ;

    [Header("Collider Settings")] public float colliderThickness = 0.001f;
    
    private float _stepSizeX, _stepSizeZ;
    [SerializeField, HideInInspector] private MeshFilter meshfilter;

    private float _f(float x, float z)
    {
        x = scaleX * x - offsetX;
        z = scaleZ * z - offsetZ;
        float y = Mathf.Sqrt(x * x + z * z) + offsetY;
        return y * scaleY;
    }

    [ContextMenu("Generate Mesh")]
    private void Generate()
    {
        meshfilter = GetComponent<MeshFilter>();
        _stepSizeX = widthX / (resolutionX - 1);
        _stepSizeZ = widthZ / (resolutionZ - 1);

        int numVerts = resolutionX * resolutionZ;
        int numQuads = (resolutionX - 1) * (resolutionZ - 1);
        int numTriVerts = numQuads * 6; // 2 tris per quad, 3 verts per tri

        Vector3[] verts = new Vector3[numVerts * 2]; // array of verts in mesh
        int[] tris = new int[numTriVerts * 2]; // indices into verts[] that make up the tris of mesh
        Vector2[] uvs = new Vector2[numVerts * 2]; // uv coords of each vert in mesh

        GenerateSurface(verts, tris, uvs, true, 0, 0);
        GenerateSurface(verts, tris, uvs, false, numVerts, numTriVerts);

        // create mesh with calculated values
        Mesh mesh = new Mesh();
        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.uv = uvs;

        // auto generate normals
        mesh.RecalculateNormals();
        meshfilter.mesh = mesh;
    }

    private void GenerateSurface(Vector3[] verts, int[] tris, Vector2[] uvs, bool normalsFacingUp, int vertOffset, int triOffset)
    {
        // get index of verts as if it was a 2d array
        Func<int, int, int> getVertIdx =
            (i, j) => i + j * resolutionX + vertOffset;

        // get (i,j)th quad, plus 0<=k<6 for each vert in the two tris for that quad
        Func<int, int, int, int> getTriIdx =
            (i, j, k) => ((i - 1) + (j - 1) * (resolutionX - 1)) * 6 + k + triOffset;

        // iterate over all the verts, calculate y = f(x,z)
        for (int xi = 0; xi < resolutionX; xi++)
        {
            float x = xi * _stepSizeX;
            for (int zi = 0; zi < resolutionZ; zi++)
            {
                float z = zi * _stepSizeZ;

                // create vert at (x, f(x, z), z)
                verts[getVertIdx(xi, zi)] = new Vector3(x, _f(x, z), z);
                uvs[getVertIdx(xi, zi)] = new Vector2(Mathf.Lerp(0, 1, x / widthX), Mathf.Lerp(0, 1, z / widthZ));

                // if xIdx > 0 and zIdx > 0 create tris at indexes
                if (xi > 0 && zi > 0)
                {
                    int[] order = normalsFacingUp ? new[] {0, 1, 2, 3, 4, 5} : new[] {2, 1, 0, 5, 4, 3}; // tri's normal depends on order of vertices
                    // upper left tri
                    tris[getTriIdx(xi, zi, order[0])] = getVertIdx(xi - 1, zi - 1); // top left
                    tris[getTriIdx(xi, zi, order[1])] = getVertIdx(xi - 1, zi); // top right
                    tris[getTriIdx(xi, zi, order[2])] = getVertIdx(xi, zi - 1); // bottom left

                    // bottom right tri
                    tris[getTriIdx(xi, zi, order[3])] = getVertIdx(xi - 1, zi); // top right
                    tris[getTriIdx(xi, zi, order[4])] = getVertIdx(xi, zi); // bottom right
                    tris[getTriIdx(xi, zi, order[5])] = getVertIdx(xi, zi - 1); // bottom left
                }
            }
        }
    }

    // generate a collider at center of each quad and align with normal. This should be ok as long as each quad is roughly rectangular
    [ContextMenu("Generate Colliders")]
    void GenerateColliders()
    {
        Clear();
        
        Vector3[] verts = GetComponent<MeshFilter>().sharedMesh.vertices;
        Func<int, int, Vector3> getVert = (i, j) => verts[i + j * resolutionX];

        GameObject boxCollider = new GameObject();
        boxCollider.AddComponent<BoxCollider>();
        
        
        for (int xi = 1; xi < resolutionX; xi++)
        {
            for (int zi = 1; zi < resolutionZ; zi++)
            {
                // i hate this
                GameObject newBox = Instantiate(boxCollider, transform);

                // opposite corners of quad
                Vector3 v1 = getVert(xi, zi), v2 = getVert(xi - 1, zi - 1), v3 = getVert(xi, zi - 1);
                
                // center of square is at midpoint of opposite corners. quad should always be rectangular so this is ok
                newBox.transform.position = Vector3.Lerp(v1 + transform.position, v2 + transform.position, 0.5f);
                newBox.GetComponent<BoxCollider>().size = new Vector3(Mathf.Abs(v1.x - v2.x), colliderThickness, Mathf.Abs(v1.z-v2.z));;
                newBox.transform.rotation = Quaternion.FromToRotation(Vector3.up, Vector3.Cross(v1-v2, v3 - v1));
            }
        }
    }
    
    [ContextMenu("Clear Box Colliders")]
    void Clear()
    {
        foreach (BoxCollider c in gameObject.GetComponents<BoxCollider>())
        {
            DestroyImmediate(c);
        }

        foreach (Transform child in transform)
        {
            DestroyImmediate(child.gameObject);
        }
    }

    [ContextMenu("Save Mesh")]
    void SaveAsset()
    {
        if (meshfilter)
        {
            var savePath = "Assets/" + saveName + ".asset";
            Debug.Log("Saved Mesh to:" + savePath);
            AssetDatabase.CreateAsset(meshfilter.sharedMesh, savePath);
        }
    }
}