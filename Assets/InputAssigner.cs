using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class InputAssigner : MonoBehaviour
{
    public PlayerInput[] players;

    void Start()
    {
        if (PlayerInput.all.Count < players.Length)
        {
            Debug.LogError("Fewer available player inputs in scene than player inputs we care about");
        }

        // remove all automatically assigned devices from PlayerInputs in the scene
        foreach (PlayerInput p in PlayerInput.all)
        {
            p.user.UnpairDevices();
        }

        // pair only the connected GamePads to only the PlayerInputs we care about
        int unassignedPlayer = 0;
        foreach (InputDevice d in InputSystem.devices)
        {
            print(d.name);
            if (d is Gamepad)
            {
                InputUser.PerformPairingWithDevice(d, players[unassignedPlayer].user);

                print("assigned " + d.name + " to user id " + players[unassignedPlayer].user.id);
                unassignedPlayer += 1;
                if (unassignedPlayer >= players.Length)
                    break;
            }
        }
    }
}