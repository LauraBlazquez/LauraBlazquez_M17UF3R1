using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpItems : MonoBehaviour
{
    public WeaponManager manager;
    public WeaponData weapon;
    public PlayerControlls controls;
    public bool isPlayerInRange = false;

    private void Awake()
    {
        controls = new PlayerControlls();
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
        controls.Gameplay.Interact.performed += ctx => PickUpItem();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    public void PickUpItem()
    {
        if (isPlayerInRange)
        {
            manager.UnlockWeapon(weapon);
            Destroy(gameObject);
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
