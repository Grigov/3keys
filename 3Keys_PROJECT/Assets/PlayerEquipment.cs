using UnityEngine;
using UnityEngine.EventSystems;


public class PlayerEquipment : MonoBehaviour
{
    public static PlayerEquipment Instance;
    public Transform weaponSlot;
    private Weapon currentWeapon;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void EquipWeapon(WeaponItem weaponItem)
    {
        foreach (Transform child in weaponSlot)
            Destroy(child.gameObject);

        GameObject weaponGO = Instantiate(weaponItem.weaponPrefab, weaponSlot.position, Quaternion.identity, weaponSlot);
        currentWeapon = weaponGO.GetComponent<Weapon>();
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (Input.GetMouseButtonDown(0) && currentWeapon != null)
        {
            Vector2 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            currentWeapon.Attack(dir);
        }
    }
}
