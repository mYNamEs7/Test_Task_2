using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CellUI : MonoBehaviour
{
    [SerializeField] private Button deleteButton;
    [SerializeField] private Image iconImage;
    [SerializeField] private PlayerMovement player;

    [SerializeField] private Sprite rifleTransform;
    [SerializeField] private Sprite makarovTransform;
    [SerializeField] private Sprite bulletsTransform;

    [SerializeField] private string rifle = "AK-74";
    [SerializeField] private string makarov = "Makarov";
    [SerializeField] private string bullets = "5.45x39";

    private void Awake()
    {
        deleteButton.gameObject.SetActive(false);

        GetComponent<Button>().onClick.AddListener(() =>
        {
            deleteButton.gameObject.SetActive(!deleteButton.gameObject.activeInHierarchy);
        });

        deleteButton.onClick.AddListener(() =>
        {
            var children = player.transform.GetComponentsInChildren<Transform>(true);

            if (iconImage.sprite == makarovTransform)
            {
                var contentChildren = BagUI.Instance.transform.GetComponentsInChildren<Image>();
                foreach (var child in contentChildren)
                {
                    if (child.sprite == rifleTransform)
                    {
                        children.First((child) => child.name.Contains(iconImage.sprite.name)).gameObject.SetActive(false);
                        children.First((child) => child.name.Contains(rifle)).gameObject.SetActive(true);

                        RemoveIcon();

                        return;
                    }
                }

                children.First((child) => child.name.Contains(iconImage.sprite.name)).gameObject.SetActive(false);
            }
            else if (iconImage.sprite == rifleTransform)
            {
                var contentChildren = BagUI.Instance.transform.GetComponentsInChildren<Image>();
                foreach (var child in contentChildren)
                {
                    if (child.sprite == makarovTransform)
                    {
                        children.First((child) => child.name.Contains(iconImage.sprite.name)).gameObject.SetActive(false);
                        children.First((child) => child.name.Contains(makarov)).gameObject.SetActive(true);

                        RemoveIcon();

                        return;
                    }
                }

                children.First((child) => child.name.Contains(iconImage.sprite.name)).gameObject.SetActive(false);
            }
            else if (iconImage.sprite == bulletsTransform)
            {
                BulletsUI.Instance.RemoveBullets();
            }
            else
            {
                var items = children.Where((child) => child.name.Contains(iconImage.sprite.name)).ToList();

                foreach (Transform item in items)
                {
                    item.gameObject.SetActive(false);
                }
            }

            RemoveIcon();
        });
    }

    private void RemoveIcon()
    {
        iconImage.sprite = null;
        Color color = iconImage.color;
        color.a = 0f;
        iconImage.color = color;
    }

    public Image GetIconImage() => iconImage;
}
