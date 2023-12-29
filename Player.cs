using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private GameObject[] weapons;
    private int weaponIndex = 0;

    [SerializeField]
    private Transform shootTransform;

    [SerializeField]
    private float shootInterval = 0.1f;
    private float lastShotTime=0f;

    // Update is called once per frame
    void Update()
    {
        // // 키보드 - 방법1
        // float horizontalInput = Input.GetAxisRaw("Horizontal");
        // //float verticalInput = Input.GetAxisRaw("Vertical");
        // Vector3 moveTo = new Vector3(horizontalInput, 0 /*verticalInput*/, 0f);
        // transform.position += moveTo * moveSpeed * Time.deltaTime;


        // 키보드 - 방법 2
        // Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime, 0, 0);
        // if(Input.GetKey(KeyCode.LeftArrow)) {
        //     transform.position -= moveTo;
        // }else if(Input.GetKey(KeyCode.RightArrow)) {
        //     transform.position += moveTo;
        // }

        // 마우스
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float toX = Mathf.Clamp(mousePos.x, -2.35f, 2.35f);
        transform.position = new Vector3(toX, -4, 0);

        if(GameManager.instance.isGameOver == false){
            shoot();
        }
    }

    void shoot() {
        if(Time.time - lastShotTime > shootInterval) {
            Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.identity);
            lastShotTime=Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss"){
            GameManager.instance.SetGameOver();
            Destroy(gameObject);
        }else if(other.gameObject.tag == "Coin"){
            GameManager.instance.IncreaseCoin();
            Destroy(other.gameObject);
        }
    }

    public void Upgrade() {
        weaponIndex++;
        if(weaponIndex >= weapons.Length) {
            weaponIndex = weapons.Length - 1;
        }
    }
}
