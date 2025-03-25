using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    [SerializeField] private float minX, maxX, minY, maxY;
    // Start is called before the first frame update
    void Start()
    {
        RandomFrogPosition();
    }

    private void RandomFrogPosition()
    {
        transform.position = new Vector2(
            Mathf.Round(Random.Range(minX, maxX)) + 0.5f,
            Mathf.Round(Random.Range(minY, maxY)) + 0.5f
        );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Snake")
        {
            RandomFrogPosition();
        }
    }
}
