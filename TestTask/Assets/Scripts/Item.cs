using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private Sprite sprite;
    [SerializeField] private ItemTypeEnum itemType;

    [SerializeField] private string rifle = "AK-74";
    [SerializeField] private string makarov = "Makarov";

    private enum ItemTypeEnum { clothes, gun, bullets }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerMovement player))
        {
            BagUI.Instance.AddItem(sprite);
            if(itemType == ItemTypeEnum.clothes)
            {
                foreach (Transform item in player.transform.GetComponentsInChildren<Transform>(true))
                {
                    if (item.name.Contains(sprite.name))
                        item.gameObject.SetActive(true);
                }
            }
            else if(itemType == ItemTypeEnum.gun)
            {
                var children = player.transform.GetComponentsInChildren<Transform>(true);

                string gunName = sprite.name == rifle ? makarov : rifle;

                children.First((child) => child.name.Contains(sprite.name)).gameObject.SetActive(true);
                children.First((child) => child.name.Contains(gunName)).gameObject.SetActive(false);
            }
            else
            {
                BulletsUI.Instance.PickUpBullets();
            }

            Destroy(gameObject);
        }
    }
}
