using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake_Movement : MonoBehaviour
{
    Vector2 Direction = Vector2.right;
    List<Transform> Snake_Body;
    [SerializeField]
    Transform Snake_Body_Prefab;
    float Xpos = 24;
    float Ypos = 12;
    float initialSize = 7;

    void Start()
    {
        Snake_Body = new List<Transform>();
        ResetState();
    }

    void Update()
    {
        Movement();
        Snake_Bounds();
    }

    void Movement()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Direction = Vector2.right;
        }
    }

    void FixedUpdate()
    {
        for(int i = Snake_Body.Count - 1; i > 0; i--)
        {
            Snake_Body[i].position = Snake_Body[i - 1].position;
        }
        this.transform.position = new Vector3(Mathf.Round(transform.position.x) + Direction.x, Mathf.Round(transform.position.y) + Direction.y, 0.0f);
    }

    void Grow()
    {
        Transform New_Body = Instantiate(this.Snake_Body_Prefab);
        New_Body.position = Snake_Body[Snake_Body.Count - 1].position;

        Snake_Body.Add(New_Body);
    }

    void Snake_Bounds()
    {
        if(transform.position.x < -Xpos)
        {
            this.transform.position = new Vector3(Xpos, transform.position.y, 0.0f);
        }
        else if(transform.position.x > Xpos)
        {
            this.transform.position = new Vector3(-Xpos, transform.position.y, 0.0f);
        }
        else if (transform.position.y < -Ypos)
        {
            this.transform.position = new Vector3(transform.position.x, Ypos, 0.0f);
        }
        else if (transform.position.y > Ypos)
        {
            this.transform.position = new Vector3(transform.position.x, -Ypos, 0.0f);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Food"))
        {
            Grow();
        }
        else if(collision.gameObject.CompareTag("Body"))
        {
            ResetState();
        }
    }

    void ResetState()
    {
        for(int i = 1; i < Snake_Body.Count; i++)
        {
            Destroy(Snake_Body[i].gameObject);
        }

        Snake_Body.Clear();
        Snake_Body.Add(this.transform);

        for(int i = 1; i < initialSize; i++)
        {
            Snake_Body.Add(Instantiate(Snake_Body_Prefab));
        }
        this.transform.position = Vector3.zero;
    }
}
