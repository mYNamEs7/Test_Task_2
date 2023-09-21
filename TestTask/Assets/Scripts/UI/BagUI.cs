using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BagUI : MonoBehaviour
{
    public static BagUI Instance { get; private set; }

    [SerializeField] private Transform content;
    [SerializeField] private Button bagButton;
    [SerializeField] private Sprite bulletsSprite;
    [SerializeField] private Sprite rifleSprite;
    [SerializeField] private Sprite makarofSprite;

    [SerializeField] private string rifle = "AK-74";
    [SerializeField] private string makarov = "Makarov";

    private void Awake()
    {
        Instance = this;

        bagButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(!gameObject.activeInHierarchy);
        });
    }

    private void Start()
    {
        foreach (Sprite icon in GameData.Instance.itemIcons)
        {
            AddItem(icon);
            AddItemToPlayer(icon);
        }

        Hide();
    }

    private void AddItemToPlayer(Sprite icon)
    {
        var player = PlayerHealth.Instance;

        if (icon == makarofSprite || icon == rifleSprite)
        {
            var children = player.transform.GetComponentsInChildren<Transform>(true);

            string gunName = icon == rifleSprite ? makarov : rifle;

            children.First((child) => child.name.Contains(icon.name)).gameObject.SetActive(true);
            children.First((child) => child.name.Contains(gunName)).gameObject.SetActive(false);
        }
        else if(icon == bulletsSprite)
        {
            BulletsUI.Instance.Init();
        }
        else
        {
            foreach (Transform item in player.transform.GetComponentsInChildren<Transform>(true))
            {
                if (item.name.Contains(icon.name))
                    item.gameObject.SetActive(true);
            }
        }
    }

    public void AddItem(Sprite icon)
    {
        var imageList = content.GetComponentsInChildren<Image>();
        foreach (Image image in imageList)
        {
            if(image.sprite == icon)
            {
                var countText = image.transform.parent.GetComponentInChildren<TMP_Text>();
                countText.color = Color.white;
                int incCount = int.Parse(countText.text) + 1;
                countText.text = incCount.ToString();
                return;
            }
        }

        Image iconImage = content.GetComponentsInChildren<Image>().First((image) => image.color.a == 0f);
        iconImage.sprite = icon;
        iconImage.color = Color.white;
    }

    public void RemoveFirstBullets()
    {
        var imageList = content.GetComponentsInChildren<Image>();
        foreach (Image image in imageList)
        {
            if (image.sprite == bulletsSprite)
            {
                image.sprite = null;
                Color color = image.color;
                color.a = 0f;
                image.color = color;

                return;
            }
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
