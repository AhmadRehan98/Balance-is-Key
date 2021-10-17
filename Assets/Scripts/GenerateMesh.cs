// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEditor;
// using UnityEngine;
//
// [RequireComponent(typeof(MeshRenderer))]
// [RequireComponent(typeof(MeshFilter))]
// public class GenerateMesh : MonoBehaviour
// {
//     [SerializeField, HideInInspector] private MeshFilter meshfilter;
//
//     public bool regenerateMesh; // does nothing except call OnValidate() when changed
//
//     public int resolutionX = 10, resolutionZ = 10; // number of verts in x and z directions
//     public float widthX = 5f, widthZ = 5f;
//
//     private float _stepSizeX, _stepSizeZ;
//
//     [Header("Function Offsets")] 
//     public float offsetX;
//     public float offsetY;
//     public float offsetZ;
//     [Header("Function Scales")] 
//     public float scaleX;
//     public float scaleY;
//     public float scaleZ;
//
//     private float _f(float x, float z)
//     {
//         x = scaleX * x - offsetX;
//         z = scaleZ * z - offsetZ;
//         float y = Mathf.Sqrt(x * x + z * z) + offsetY;
//         return y * scaleY;
//     }
//
//     private void Generate()
//     {
//         int numVerts = resolutionX * resolutionZ;
//         int numQuads = (resolutionX - 1) * (resolutionZ - 1);
//         int numTriVerts = numQuads * 6; // 2 tris per quad, 3 verts per tri
//
//         Vector3[] verts = new Vector3[numVerts * 2]; // array of verts in mesh
//         int[] tris = new int[numTriVerts * 2]; // indices into verts[] that make up the tris of mesh
//         Vector2[] uvs = new Vector2[numVerts * 2]; // uv coords of each vert in mesh
//
//         GenerateSurface(verts, tris, uvs, true, 0, 0);
//         GenerateSurface(verts, tris, uvs, false, numVerts, numTriVerts);
//
//         // create mesh with calculated values
//         Mesh mesh = new Mesh();
//         mesh.vertices = verts;
//         mesh.triangles = tris;
//         mesh.uv = uvs;
//
//         // auto generate normals
//         mesh.RecalculateNormals();
//         meshfilter.mesh = mesh;
//     }
//
//     private void GenerateSurface(Vector3[] verts, int[] tris, Vector2[] uvs, bool normalsFacingUp, int vertOffset, int triOffset)
//     {
//         // get index of verts as if it was a 2d array
//         Func<int, int, int> getVert =
//             (i, j) => i + j * resolutionX + vertOffset;
//
//         // get (i,j)th quad, plus 0<=k<6 for each vert in the two tris for that quad
//         Func<int, int, int, int> getTri =
//             (i, j, k) => ((i - 1) + (j - 1) * (resolutionX - 1)) * 6 + k + triOffset;
//
//         // iterate over all the verts, calculate y = f(x,z)
//         for (int xi = 0; xi < resolutionX; xi++)
//         {
//             float x = xi * _stepSizeX;
//             for (int zi = 0; zi < resolutionZ; zi++)
//             {
//                 float z = zi * _stepSizeZ;
//
//                 // create vert at (x, f(x, z), z)
//                 verts[getVert(xi, zi)] = new Vector3(x, _f(x, z), z);
//                 uvs[getVert(xi, zi)] = new Vector2(Mathf.Lerp(0, 1, x / widthX), Mathf.Lerp(0, 1, z / widthZ));
//
//                 // if xIdx > 0 and zIdx > 0 create tris at indexes
//                 if (xi > 0 && zi > 0)
//                 {
//                     int[] order = normalsFacingUp ? new[] {0, 1, 2, 3, 4, 5} : new[] {2, 1, 0, 5, 4, 3};
//                     // upper left tri
//                     tris[getTri(xi, zi, order[0])] = getVert(xi - 1, zi - 1); // top left
//                     tris[getTri(xi, zi, order[1])] = getVert(xi - 1, zi); // top right
//                     tris[getTri(xi, zi, order[2])] = getVert(xi, zi - 1); // bottom left
//
//                     // bottom right tri
//                     tris[getTri(xi, zi, order[3])] = getVert(xi - 1, zi); // top right
//                     tris[getTri(xi, zi, order[4])] = getVert(xi, zi); // bottom right
//                     tris[getTri(xi, zi, order[5])] = getVert(xi, zi - 1); // bottom left
//                 }
//             }
//         }
//     }
//
//     void SaveAsset(string saveName)
//     {
//         if (meshfilter)
//         {
//             var savePath = "Assets/" + saveName + ".asset";
//             Debug.Log("Saved Mesh to:" + savePath);
//             AssetDatabase.CreateAsset(meshfilter.sharedMesh, savePath);
//         }
//     }
//
//     private void OnValidate()
//     {
//         meshfilter = GetComponent<MeshFilter>();
//         _stepSizeX = widthX / (resolutionX - 1);
//         _stepSizeZ = widthZ / (resolutionZ - 1);
//         Generate();
//         SaveAsset("curved_platform");
//     }
// }