using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

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

    void Start()
    {
        mapManager = GetComponent<MapManager>();

        playerHealth = player.GetComponent<HealthComponent>();
        wasteCollection = player.GetComponent<WasteCollection>();        
        playerWeapon = player.GetComponentInChildren<WeaponMelee>();
    }

    private int currentWasteCount = -1;
    void Update()
    {
        if (wasteCollection.wasteCount % wastesBeforeNewGeneration == 0 && currentWasteCount != wasteCollection.wasteCount)
        {
            mapManager.GenerateMap(mapSpawnPoint);
            var newMapPosition = new Vector2(Random.Range(-50, 50), Random.Range(-50, 50)) + (Vector2)player.transform.position;
            mapSpawnPoint = new Vector2Int((int)newMapPosition.x, (int)newMapPosition.y);

            currentWasteCount = wasteCollection.wasteCount;

            playerHealth.Heal(50);
        }

        UpdateUI();
    }

    void UpdateUI()
    {
        wasteText.text = wasteCollection.wasteCount.ToString();
        scoreCount.text = "Kill: " + playerWeapon.killCount.ToString();
        healthBar.fillAmount = playerHealth.health / playerHealth.maxHealth;
    }
}
