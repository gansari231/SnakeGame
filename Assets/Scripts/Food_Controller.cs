using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food_Controller : MonoBehaviour
{
    public BoxCollider2D Spawn_Area;

    void Start()
    {
        Random_Position();
    }

    void Random_Position()
    {
        Bounds Spawn_Bounds = this.Spawn_Area.bounds;
        float Xpos = Random.Range(Spawn_Bounds.min.x, Spawn_Bounds.max.x);
        float Ypos = Random.Range(Spawn_Bounds.min.y, Spawn_Bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(Xpos), Mathf.Round(Ypos), 0.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Snake"))
        {
            Random_Position();
        }
    }
}
