using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    // Wastes collected before new 
    public int wastesBeforeNewGeneration;

    // Player Data
    private HealthComponent playerHealth;
    private WasteCollection wasteCollection;
    private WeaponMelee playerWeapon;

    private MapManager mapManager;

    private Vector2Int mapSpawnPoint = Vector2Int.zero;

    // UI Components
    public TextMeshProUGUI wasteText;
    public TextMeshProUGUI scoreCount;
    public Image healthBar;

    private int highScore = 0;
    private int score;

    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");

        mapManager = GetComponent<MapManager>();

        playerHealth = player.GetComponent<HealthComponent>();
        wasteCollection = player.GetComponent<WasteCollection>();        
        playerWeapon = player.GetComponentInChildren<WeaponMelee>();
    }

    private int currentWasteCount = -1;
    void Update()
    {
        score = playerWeapon.killCount + 2 * wasteCollection.wasteCount;

        if (wasteCollection.wasteCount % wastesBeforeNewGeneration == 0 && currentWasteCount != wasteCollection.wasteCount)
        {
            mapManager.GenerateMap(mapSpawnPoint);
            var newMapPosition = new Vector2(Random.Range(-50, 50), Random.Range(-50, 50)) + (Vector2)player.transform.position;
            mapSpawnPoint = new Vector2Int((int)newMapPosition.x, (int)newMapPosition.y);

            currentWasteCount = wasteCollection.wasteCount;
        }

        UpdateUI();

        if (playerHealth.isDead)
        {
            if (score > highScore)
                PlayerPrefs.SetInt("HighScore", score);

            StartCoroutine(GameOver());
        }
    }

    void UpdateUI()
    {
        wasteText.text = wasteCollection.wasteCount.ToString();
        scoreCount.text = "Kill: " + playerWeapon.killCount.ToString();
        healthBar.fillAmount = playerHealth.health / playerHealth.maxHealth;
    }

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainMenu");
    }
}
