using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f; 
    public float moveDistance = 15f; 
    private Vector3 startPosition;
    private bool moveUp = true;
    private bool playerOnPlatform = false;

    void Start()
    {
        // Stocke la position initiale de la plateforme
        startPosition = transform.position;
    }

    void Update()
    {
        // Si le joueur est sur la plateforme, d�placer la plateforme
        if (playerOnPlatform)
        {
            MovePlatform();
        }
    }

    void MovePlatform()
    {
        // D�placement vers le haut
        if (moveUp)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            // Si la plateforme a atteint la distance de d�placement maximale vers le haut
            if (transform.position.y >= startPosition.y + moveDistance)
            {
                moveUp = false;
            }
        }
        else
        {
            // D�placement vers le bas
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            // Si la plateforme est descendue jusqu'� la position initiale moins la distance de d�placement
            if (transform.position.y <= startPosition.y - moveDistance)
            {
                moveUp = true;
            }
        }
    }

    // D�tecte quand un objet entre en collision avec le d�clencheur
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // V�rifie si l'objet en collision a le tag "Player"
        if (collision.CompareTag("Player"))
        {
            playerOnPlatform = true;
        }
    }

    // D�tecte quand un objet quitte la collision avec le d�clencheur
    private void OnTriggerExit2D(Collider2D collision)
    {
        // V�rifie si l'objet en collision a le tag "Player"
        if (collision.CompareTag("Player"))
        {
            playerOnPlatform = false;
        }
    }
}
