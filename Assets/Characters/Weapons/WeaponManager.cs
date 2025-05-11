using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] private WeaponData defaultWeapon;
    [SerializeField] private GenericPool magazinePool;

    public List<WeaponData> unlockedWeapons = new List<WeaponData>();
    public WeaponData currentWeapon;
    public bool hasPistol = true;
    private PlayerInput playerInput;
    public Player player;

    private void Start()
    {
        if (defaultWeapon != null)
        {
            unlockedWeapons.Add(defaultWeapon);
            currentWeapon = defaultWeapon;
            UpdateWeaponPool();
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
            currentWeapon = unlockedWeapons[index];
            UpdateWeaponVisual();
            UpdateWeaponPool();
            UpdateFireOrigin();
        }
        else
        {
            Debug.Log("No weapon unlocked in this slot.");
        }
    }

    private void UpdateWeaponVisual()
    {
        Animator animator = GetComponentInParent<Animator>();
        if (hasPistol)
        {
            animator.SetBool("pistolEquiped", true);
        }
        else
        {
            animator.SetBool("pistolEquiped", false);
        }
    }

    private void UpdateWeaponPool()
    {
        if (currentWeapon == null || currentWeapon.poolSO == null)
        {
            Debug.LogWarning("Faltan referencias para crear el pool.");
        }
        else
        {
            if (magazinePool == null)
            {
                Debug.Log("Esta arma no tiene pool");
            }
            else
            {
                magazinePool.poolConfig = currentWeapon.poolSO;
                if (GenericPool.Pools.ContainsKey(magazinePool.poolConfig.poolID))
                {
                    GenericPool.Pools.Remove(magazinePool.poolConfig.poolID);
                }
                foreach (Transform child in magazinePool.transform)
                {
                    Destroy(child.gameObject);
                }
                magazinePool.InitializePool();

                Debug.Log($"Pool instanciada para: {currentWeapon.poolSO.poolID}");
            }
        }
    }

    private void UpdateFireOrigin()
    {
        player.fireOrigin = currentWeapon.fireOrigin;
    }

    public void UnlockWeapon(WeaponData newWeapon)
    {
        if (!unlockedWeapons.Contains(newWeapon))
        {
            unlockedWeapons.Add(newWeapon);
        }
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        if (currentWeapon != null)
        {
            currentWeapon.UseWeapon(gameObject);
        }
    }
}
