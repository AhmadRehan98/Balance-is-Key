using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessories : MonoBehaviour
{
    private GameObject _hat, _belt, _body;

    public bool hatEnabled = true, beltEnabled = true;

    [Range(0, 3)] public int variantSkin;

    [Range(0, StaticClass.MAXPlayers - 1)] public int player;

    // Start is called before the first frame update
    void Start()
    {
        AssignRefs();
        beltEnabled = StaticClass.beltEnabled[player];
        hatEnabled = StaticClass.hatEnabled[player];
        variantSkin = StaticClass.skin[player];
        ReloadAccessories();
    }

    private void AssignRefs()
    {
        if (player < 0 || player >= StaticClass.MAXPlayers)
            Debug.LogError(transform.name + " has invalid player number");
        
        Transform child = gameObject.transform.GetChild(0);
        _body = child.GetChild(0).GetChild(0).gameObject;
        child = child.GetChild(1).GetChild(1).GetChild(0);
        _belt = child.transform.GetChild(0).GetChild(2).gameObject;
        _hat = child.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).gameObject;
        print($"body={_body.name} belt={_belt.name} hat={_hat.name}");


        if (_body == null)
            Debug.LogWarning("Could not find body mesh for " + transform.name);
    }

    public void ReloadAccessories()
    {
        // causes warning when called in editor but should be ok
        _belt.SetActive(StaticClass.beltEnabled[player]);
        _hat.SetActive(StaticClass.hatEnabled[player]);
        _body.GetComponent<SkinnedMeshRenderer>().material = StaticClass.variants[StaticClass.skin[player]];
    }

    public void ToggleHat()
    {
        StaticClass.hatEnabled[player] = !StaticClass.hatEnabled[player];
        ReloadAccessories();
    }

    public void ToggleBelt()
    {
        StaticClass.beltEnabled[player] = !StaticClass.beltEnabled[player];
        ReloadAccessories();
    }

    public void NextSkin()
    {
        StaticClass.skin[player] = (StaticClass.skin[player] + 1) % 4; 
        ReloadAccessories();
    }

    // should only ever be called in editor so won't effect runtime
    private void OnValidate()
    {
        if (player < 0 || player >= StaticClass.MAXPlayers)
        {
            Debug.LogError(transform.name + " has invalid player number " + player);
            return;
        }

        StaticClass.hatEnabled[player] = hatEnabled;
        StaticClass.beltEnabled[player] = beltEnabled;
        StaticClass.skin[player] = variantSkin;
        AssignRefs();
        ReloadAccessories();
    }
}