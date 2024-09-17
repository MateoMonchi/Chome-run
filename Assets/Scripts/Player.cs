using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public float Jump;
    public bool isJumping = false;
    public GameObject RestartBO;

    // Referencia al Object Pool
    public ObjectPool enemyPool;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isJumping == true)
        {
            rb.AddForce(Vector2.up * Jump);
            isJumping = false;
            anim.SetBool("IsJumping", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && isJumping == false)
        {
            isJumping = true;
            anim.SetBool("IsJumping", false);
        }
        if (collision.gameObject.tag == "Enemy")
        {
            anim.SetBool("Dead", true);
            Time.timeScale = 0;
            RestartBO.SetActive(true);
        }
    }

    // Método que genera un enemigo desde el pool
    public void SpawnEnemy()
    {
        GameObject enemy = enemyPool.GetPooledObject();
        if (enemy != null)
        {
            // Posicionas el enemigo donde desees que aparezca
            enemy.transform.position = new Vector3(Random.Range(-5, 5), 0, 0);
            enemy.SetActive(true);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Level1");
        Time.timeScale = 1;
        print("Finish!!!!");
    }
}
