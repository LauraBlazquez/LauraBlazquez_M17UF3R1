using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fogata : MonoBehaviour
{
    public WeaponManager manager;
    public Player player;
    public PlayerControlls controls;
    public bool isPlayerInRange = false;

    private void Awake()
    {
        controls = new PlayerControlls();
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
        controls.Gameplay.Interact.performed += ctx => Interact();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    public void Interact()
    {
        if (isPlayerInRange)
        {
            SaveSystem.SavePlayer(player, manager);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
