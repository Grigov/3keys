using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public Item item;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bool added = Inventory.Instance.AddItem(item);
            if (added)
            {
                Destroy(gameObject);
            }
        }
    }

}
