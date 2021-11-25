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
        int unassignedPlayer = 0;
        if (PlayerInput.all.Count < players.Length)
        {
            Debug.LogError("Fewer available player inputs in scene than player inputs we care about");
        }

        // List<InputDevice> devices = new List<InputDevice>();

        foreach (PlayerInput p in PlayerInput.all)
        {
            p.user.UnpairDevices();
        }

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