using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;


public static class StaticClass
{
    public const int MAXPlayers = 2;

    public static bool[] hatEnabled = {true, true};
    public static bool[] beltEnabled = {true, true};

    public static Material[] variants = Resources.LoadAll("Materials/variants", typeof(Material)).Cast<Material>().ToArray();
    public static int[] skin = {0, 1};

    public static int levelsCompleted = 0;
    public static int maxLevels = 3;
}