using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BulletsUI : MonoBehaviour
{
    public static BulletsUI Instance { get; private set; }

    public int Bullets {  get; private set; }

    [SerializeField] private TMP_Text bulletCountText;

    private int clip = 20;

    private void Awake()
    {
        Instance = this;
    }

    public void Init()
    {
        bulletCountText.text = "x" + (Bullets = GameData.Instance.bullets).ToString();
    }

    public void PickUpBullets()
    {
        bulletCountText.text = "x" + (Bullets += 20).ToString();
    }

    public void Shoot()
    {
        bulletCountText.text = "x" + (--Bullets).ToString();
        clip--;

        if(clip <= 0)
        {
            clip = 20;
            BagUI.Instance.RemoveFirstBullets();
        }
    }

    public void RemoveBullets()
    {
        bulletCountText.text = "x" + (Bullets -= Bullets - 20 < 0 ? Bullets : 20).ToString();
        clip = 20;
    }
}
