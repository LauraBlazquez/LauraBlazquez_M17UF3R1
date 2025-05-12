using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private WeaponData defaultWeapon;

    public List<WeaponData> unlockedWeapons = new List<WeaponData>();
    public WeaponDatabase weaponDatabase;
    public WeaponData currentWeapon;
    public bool hasPistol = true;
    private PlayerInput playerInput;
    public Player player;

    private Dictionary<string, GenericPool> weaponPools = new Dictionary<string, GenericPool>();

    private void Start()
    {
        if (defaultWeapon != null)
        {
            unlockedWeapons.Add(defaultWeapon);
            SetCurrentWeapon(defaultWeapon);
        }
    }

    private void OnEnable()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        var actions = playerInput.actions;

        actions["Select Item 1"].performed += ctx => SelectWeaponByIndex(0);
        actions["Select Item 2"].performed += ctx => SelectWeaponByIndex(1);
    }

    private void OnDisable()
    {
        var actions = playerInput.actions;

        actions["Select Item 1"].performed -= ctx => SelectWeaponByIndex(0);
        actions["Select Item 2"].performed -= ctx => SelectWeaponByIndex(1);
    }

    void SelectWeaponByIndex(int index)
    {
        if (index < unlockedWeapons.Count)
        {
            SetCurrentWeapon(unlockedWeapons[index]);
        }
        else
        {
            Debug.Log("No weapon unlocked in this slot.");
        }
    }

    private void SetCurrentWeapon(WeaponData weapon)
    {
        currentWeapon = weapon;
        player.currentWeapon = currentWeapon;
        player.fireOrigin = weapon.fireOrigin;
        player.pool = weapon.pool;
        if (weapon.name == "Pistol") hasPistol = true;
        else hasPistol = false;

        UpdateWeaponVisual();

        if (weapon.pool != null)
        {
            if (!weaponPools.ContainsKey(weapon.name))
            {
                weapon.pool.poolConfig = weapon.poolSO;
                weapon.pool.InitializePool();
                weaponPools.Add(weapon.name, weapon.pool);
            }
            else
            {
                Debug.Log("La pool ya está inicializada para esta arma.");
            }
        }
    }

    private void UpdateWeaponVisual()
    {
        Animator animator = GetComponentInParent<Animator>();
        animator.SetBool("pistolEquiped", hasPistol);
    }

    public void UnlockWeapon(WeaponData newWeapon)
    {
        if (!unlockedWeapons.Contains(newWeapon))
        {
            unlockedWeapons.Add(newWeapon);
        }
    }

    public void LoadInventory(List<string> weaponNames)
    {
        unlockedWeapons.Clear();

        foreach (string name in weaponNames)
        {
            WeaponData weapon = weaponDatabase.allWeapons.Find(w => w.name == name);
            if (weapon != null)
            {
                unlockedWeapons.Add(weapon);
            }
            else
            {
                Debug.LogWarning($"No se encontró el arma con nombre: {name}");
            }
        }
        if (unlockedWeapons.Count > 0)
        {
            SetCurrentWeapon(unlockedWeapons[0]);
        }
    }
}
