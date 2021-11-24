// CreateMaterialsForTextures.cs
// C#

using System;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Codice.Client.BaseCommands;

public class CreateMaterialsForTextures : ScriptableWizard
{
    public static Shader shader = Shader.Find("Standard");
    public static string ignoreSubstring = "opening door_";
    
    private const int ALBEDO = 0;
    private const int METALLIC = 1;
    private const int NORMAL = 2;

    [MenuItem("Tools/CreateMaterialsForTextures")]
    static void CreateMaterials()
    {
        Dictionary<string, int> suffixes = new Dictionary<string, int>();
        Dictionary<string, Texture[]> maps = new Dictionary<string, Texture[]>();

        suffixes.Add("AlbedoTransparency.png", ALBEDO);
        suffixes.Add("MetallicSmoothness.png", METALLIC);
        suffixes.Add("Normal.png", NORMAL);

        try
        {
            AssetDatabase.StartAssetEditing();
            var textures = Selection.GetFiltered(typeof(Texture), SelectionMode.Assets).Cast<Texture>();
            foreach (var tex in textures)
            {
                string path = AssetDatabase.GetAssetPath(tex);
                string materialPath;
                int suffixSartIdx = path.LastIndexOf("_") + 1;
                string fileSuffix = path.Substring(suffixSartIdx);
                
                int ignoreStartIdx = path.LastIndexOf(ignoreSubstring);
                if (ignoreStartIdx >= 0)
                {
                    int nameStartIdx = ignoreStartIdx + ignoreSubstring.Length;
                    materialPath = path.Substring(0, ignoreStartIdx) + 
                                   path.Substring(nameStartIdx, suffixSartIdx - nameStartIdx - 1);
                }
                else
                {
                    materialPath = path.Substring(0, path.LastIndexOf("_"));
                }
                
                Debug.Log("Found map of type " + fileSuffix + " for " + materialPath);

                int suffixTypeIdx;
                if (suffixes.TryGetValue(fileSuffix, out suffixTypeIdx))
                {
                    Texture[] mapsForFile;
                    if (maps.TryGetValue(materialPath, out mapsForFile))
                    {
                        mapsForFile[suffixTypeIdx] = tex;
                    }
                    else
                    {
                        mapsForFile = new Texture[3];
                        mapsForFile[suffixTypeIdx] = tex;
                        maps.Add(materialPath, mapsForFile);
                        Debug.Log("created map array for " + materialPath);
                    }
                }
                else
                {
                    Debug.LogWarning("map type " + fileSuffix + " not recognized");
                }
            }

            foreach (KeyValuePair<string, Texture[]> pair in maps)
            {
                string newMaterialPath = pair.Key + ".mat";
                Texture[] currMatTextures = pair.Value;
                
                if (AssetDatabase.LoadAssetAtPath(newMaterialPath, typeof(Material)) != null)
                {
                    Debug.LogWarning("Can't create material, it already exists: " + newMaterialPath);
                    continue;
                }

                var mat = new Material(shader);
                if (currMatTextures[ALBEDO] != null)
                    mat.SetTexture("_MainTex", currMatTextures[ALBEDO]);
                else                    
                    Debug.LogWarning(newMaterialPath + " is missing Albedo map");
                
                if (currMatTextures[METALLIC] != null)
                    mat.SetTexture("_MetallicGlossMap", currMatTextures[METALLIC]);
                else
                    Debug.LogWarning(newMaterialPath + " is missing MetallicSmoothness map");
                
                if (currMatTextures[NORMAL] != null)
                    mat.SetTexture("_BumpMap", currMatTextures[NORMAL]);
                else
                    Debug.LogWarning(newMaterialPath + " is missing Normal map");

                AssetDatabase.CreateAsset(mat, newMaterialPath);
            }
        }
        finally
        {
            AssetDatabase.StopAssetEditing();
            AssetDatabase.SaveAssets();
        }
    }
}