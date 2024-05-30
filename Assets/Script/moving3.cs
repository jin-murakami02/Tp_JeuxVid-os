using UnityEngine;

public class moving3 : MonoBehaviour
{
    public float speed = 2f; 
    public float moveDistance = 15f; 
    private Vector3 startPosition;
    private bool moveRight = false; 

    void Start()
    {
        // Stocke la position initiale de la plateforme
        startPosition = transform.position;
    }

    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        // Déplacement vers la gauche
        if (!moveRight)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            // Si la plateforme a atteint la distance de déplacement maximale vers la gauche
            if (transform.position.x <= startPosition.x - moveDistance)
            {
                moveRight = true;
            }
        }
        else
        {
            // Déplacement vers la droite
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            // Si la plateforme a atteint la distance de déplacement maximale vers la droite
            if (transform.position.x >= startPosition.x + moveDistance)
            {
                moveRight = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
