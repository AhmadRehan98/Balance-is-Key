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
    /* If all your materials are named "door mat1_AlbedoTransparency.png" "door mat2_AlbedoTransparency.png" etc, and you just want
     * the final materials to be called "mat1.mat" "mat2.mat", set the ignoreSubstring to be "door "
     */
    public static string ignoreSubstring = "";


    // Only set up for the unity default metallic shader, you could probably modify this script to have it work with a different one but no promises
    public static Shader shader = Shader.Find("Standard");
    private const int ALBEDO = 0;
    private const int METALLIC = 1;
    private const int NORMAL = 2;

    [MenuItem("Tools/Create Materials From Selected Maps")]
    static void CreateMaterials()
    {
        // mapping each suffix string to a const int
        Dictionary<string, int> suffixes = new Dictionary<string, int>();
        suffixes.Add("AlbedoTransparency.png", ALBEDO);
        suffixes.Add("MetallicSmoothness.png", METALLIC);
        suffixes.Add("Normal.png", NORMAL);

        // create a dict for each material name and all of its mats
        Dictionary<string, Texture[]> maps = new Dictionary<string, Texture[]>();

        try
        {
            AssetDatabase.StartAssetEditing();

            // get all selected maps
            var textures = Selection.GetFiltered(typeof(Texture), SelectionMode.Assets).Cast<Texture>();
            foreach (var tex in textures)
            {
                // do a bunch of string manipulation stuff to get the file path to the material this map belongs to 
                string path = AssetDatabase.GetAssetPath(tex);
                int suffixSartIdx = path.LastIndexOf("_") + 1;
                string fileSuffix = path.Substring(suffixSartIdx);
                string materialPath;
                int ignoreStartIdx = path.LastIndexOf(ignoreSubstring);
                if (ignoreSubstring != "" && ignoreStartIdx >= 0)
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

                // store this map in an array, and assign that array to the key for the appropriate material
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
                        mapsForFile = new Texture[3]; // change this if you want to use a different shader that uses a different number of maps
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

            // create each material
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