using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private GameObject gameOverPanel;

    private int coin = 0;

    [HideInInspector]
    public bool isGameOver = false;

    void Awake() { // start 메소드보다 한단계 더 먼저 불러지는 메소드
        if(instance == null){
            instance = this;
        }
    }

    public void IncreaseCoin() {
        coin++;
        text.SetText(coin.ToString());

        if(coin % 30 == 0){
            Player player = FindObjectOfType<Player>();
            if(player != null) {
                player.Upgrade();
            }
        }
    }

    public void SetGameOver() {
        isGameOver = true;

        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();
        if(enemySpawner != null){
            enemySpawner.StopEnemyRoutine();
        }

        Invoke("ShowGameOverPanel", 0.5f);
    }
    
    void ShowGameOverPanel() {
        gameOverPanel.SetActive(true);
    }

    public void PlayAgain() {
        SceneManager.LoadScene("SampleScene");
    }
}
