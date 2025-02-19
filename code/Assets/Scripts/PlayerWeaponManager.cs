using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWeaponManager : MonoBehaviour
{
    public List<WeaponController> startingWeapons = new List<WeaponController>();

    public Camera weaponCamera;

    public Transform weaponParentSocket;

    public UnityAction<WeaponController> onSwtichedToWeapon;

    private WeaponController[] _weaponSlots = new WeaponController[9];

    private void Start()
    {
        onSwtichedToWeapon += OnWeaponSwitched;

        for (int i = 0; i < startingWeapons.Count; i++)
        {
            AddWeapon(startingWeapons[i], i);
        }

        SwitchWeapon();
    }

    private void Update()
    {
        WeaponController activeWeapon = _weaponSlots[0];
        
        if(activeWeapon)
        {
            activeWeapon.HandleShootInputs(PlayerInputHandler.Instance.GetFireInputHeld());
        }
    }

    public bool AddWeapon(WeaponController weaponPrefab, int position)
    {
        if (position >= 0 && position < _weaponSlots.Length && _weaponSlots[position] == null)
        {
            WeaponController weaponInstance = Instantiate(weaponPrefab, weaponParentSocket);
            weaponInstance.transform.localPosition = Vector3.zero;
            weaponInstance.transform.localRotation = Quaternion.identity;

            weaponInstance.owner = gameObject;
            weaponInstance.sourcePrefab = weaponPrefab.gameObject;
            weaponInstance.ShowWeapon(false);

            _weaponSlots[position] = weaponInstance;

            return true;
        }
        else
        {
            return false;
        }
    }
    //public bool AddWeapon(WeaponController weaponPrefab)
    //{
    //    for (int i = 0; i < _weaponSlots.Length; i++)
    //    {
    //        WeaponController weaponInstance = Instantiate(weaponPrefab, weaponParentSocket);
    //        weaponInstance.transform.localPosition = Vector3.zero;
    //        weaponInstance.transform.localRotation = Quaternion.identity;

    //        weaponInstance.owner = gameObject;
    //        weaponInstance.sourcePrefab = weaponPrefab.gameObject;
    //        weaponInstance.ShowWeapon(false);

    //        _weaponSlots[i] = weaponInstance;

    //        return true;
    //    }

    //    return false;
    //}

    public void SwitchWeapon()
    {
        SwitchWeaponToIndex(0);
    }

    public void SwitchWeaponToIndex(int newWeaponIndex)
    {
        if (newWeaponIndex >= 0)
        {
            WeaponController newWeapon = GetWeaponAtSlotIndex(newWeaponIndex);

            if(onSwtichedToWeapon != null)
            {
                onSwtichedToWeapon.Invoke(newWeapon);
            }
        }
    }

    public WeaponController GetWeaponAtSlotIndex(int index)
    {
        if(index >=0 && index < _weaponSlots.Length)
        {
            return _weaponSlots[index];
        }
        return null;
    }

    private void OnWeaponSwitched(WeaponController newWeapon)
    {
        if (newWeapon != null)
        {
            newWeapon.ShowWeapon(true);
        }
    }
}
