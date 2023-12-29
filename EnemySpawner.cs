using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;

    [SerializeField]
    private GameObject boss;

    private float[] arrPosX = {-2.2f, -1.1f, 0f, 1.1f, 2.2f};

    [SerializeField]
    private float spawnInterval = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartEnemyRoutine();
    }

    void StartEnemyRoutine() {
        StartCoroutine("EnemyRoutine");
    }

    public void StopEnemyRoutine() {
        StopCoroutine("EnemyRoutine");
    }

    IEnumerator EnemyRoutine() {
        yield return new WaitForSeconds(3f); // 다음 동작 가기 전에 기다림

        float moveSpeed = 5f;
        int enemyIndex = 0;
        int spawnCount = 0;

        while(true) {
            foreach(float posX in arrPosX){
                //int index = Random.Range(0, enemies.Length);
                SpawnEnemy(posX, enemyIndex/*index*/, moveSpeed);
            }

            spawnCount++;

            if(spawnCount % 10 == 0) {
                enemyIndex++;
                moveSpeed+=2f;
            }

            if(enemyIndex >= enemies.Length) {
                SpawnBoss();
                enemyIndex=0;
                moveSpeed = 5f;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemy(float posX, int index, float moveSpeed) {
        Vector3 spawnPos = new Vector3(posX, transform.position.y, transform.position.z);

        if(Random.Range(0,5) == 0){
            index += 1;
        }

        if(index >= enemies.Length){
            index = enemies.Length - 1;
        }

        GameObject enemyObject =  Instantiate(enemies[index], spawnPos, Quaternion.identity);
        Enemy enemy = enemyObject.GetComponent<Enemy>(); // 게임오브젝트로부터 에너미라는 컴포넌트 얻어옴
        enemy.SetMoveSpeed(moveSpeed);
    }

    void SpawnBoss() {
        Instantiate(boss, transform.position, Quaternion.identity);
    }
}
