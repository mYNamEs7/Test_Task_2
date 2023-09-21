using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private Button startAgainButton;
    [SerializeField] private TMP_Text titleText;

    [SerializeField] private string bullets = "5.45x39";
    [SerializeField] private string makarov = "Makarov";

    private void Awake()
    {
        startAgainButton.onClick.AddListener(() =>
        {
            if(titleText.text == "вы выиграли!")
                SaveData();
            else
                ResetData();

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
    }

    private void Start()
    {
        PlayerHealth.OnPlayerDeath += PlayerHealth_OnPlayerDeath;
        EnemySpawner.Instance.OnAllEnemiesDeath += EnemySpawner_OnAllEnemiesDeath;

        Hide();
    }

    private void PlayerHealth_OnPlayerDeath()
    {
        Show("вы проиграли!");
    }

    private void EnemySpawner_OnAllEnemiesDeath()
    {
        Show("вы выиграли!");
    }

    private void SaveData()
    {
        GameData gameData = GameData.Instance;

        gameData.bullets = BulletsUI.Instance.Bullets;
        gameData.health = PlayerHealth.Instance.GetHeath();
        foreach (CellUI cell in FindObjectsOfType<CellUI>(true))
        {
            for (int i = 0; i < int.Parse(cell.GetComponentInChildren<TMP_Text>().text); i++)
            {
                Sprite sprite = cell.GetIconImage().sprite;
                if(sprite != null)
                {
                    if (i == int.Parse(cell.GetComponentInChildren<TMP_Text>().text) - 1 && (sprite.name == makarov || sprite.name == bullets))
                        continue;

                    gameData.itemIcons.Add(sprite);
                }
            }
        }

        gameData.SaveData();
    }

    private void ResetData()
    {
        GameData gameData = GameData.Instance;

        gameData.bullets = 20;
        gameData.health = 100;
        gameData.itemIcons = gameData.startItemIcons.ToList();

        gameData.SaveData();
    }

    private void Show(string text)
    {
        titleText.text = text;
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        PlayerHealth.OnPlayerDeath -= PlayerHealth_OnPlayerDeath;
    }
}
