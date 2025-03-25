using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SnakeController : MonoBehaviour
{
    private Vector2 direction;
    [SerializeField] private GameObject snakeTailPrefab;
    [SerializeField] private TextMeshProUGUI scoreText;
    private List<GameObject> snakeList = new List<GameObject>();
    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        SetDefault();
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();
    }

    private void FixedUpdate()
    {
        SnakeMove();
        SnakeHeadRotation();
    }

    private void GetPlayerInput()
    {
        if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && direction != Vector2.down)
        {
            direction = Vector2.up;
        }
        else if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) && direction != Vector2.right)
        {
            direction = Vector2.left;
        }
        else if ((Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) && direction != Vector2.up)
        {
            direction = Vector2.down;
        }
        else if ((Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) && direction != Vector2.left)
        {
            direction = Vector2.right;
        }
    }

    private void SetDefault()
    {
        direction = Vector2.right;
        Time.timeScale = 0.18f;

        CreateSnakeTail();
        CreateSnakeTail();
    }

    private void SnakeMove()
    {
        float x, y;
        x = transform.position.x + direction.x;
        y = transform.position.y + direction.y;

        Vector2 prevPos = transform.position;
        transform.position = new Vector2(x, y);

        // Kuyruklarý takip ettir
        for (int i = 0; i < snakeList.Count; i++)
        {
            Vector2 tempPos = snakeList[i].transform.position;
            snakeList[i].transform.position = prevPos;
            prevPos = tempPos;
        }
    }

    private void SnakeHeadRotation()
    {
        if(direction == Vector2.up)
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (direction == Vector2.down)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction == Vector2.left)
        {
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }
        else if (direction == Vector2.right)
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Snake")
        {
            RestartGame();
        }
        else if(collision.gameObject.tag == "Frog")
        {
            CreateSnakeTail();
            score++;
            scoreText.text = score.ToString();
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void CreateSnakeTail()
    {
        Vector2 tailPosition;
        if (snakeList.Count == 0)
        {
            tailPosition = (Vector2)transform.position - direction; // Ýlk kuyruk yýlan baþýnýn arkasýnda
        }
        else
        {
            tailPosition = (Vector2)snakeList[snakeList.Count - 1].transform.position - direction; // Yeni kuyruk en son eklenen kuyruðun arkasýna eklenir
        }

        GameObject newTail = Instantiate(snakeTailPrefab, tailPosition, Quaternion.identity);
        snakeList.Add(newTail);
    }
}
