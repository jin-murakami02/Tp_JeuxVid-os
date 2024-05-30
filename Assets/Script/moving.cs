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
        // Si le joueur est sur la plateforme, déplacer la plateforme
        if (playerOnPlatform)
        {
            MovePlatform();
        }
    }

    void MovePlatform()
    {
        // Déplacement vers le haut
        if (moveUp)
        {
            transform.Translate(Vector2.up * speed * Time.deltaTime);
            // Si la plateforme a atteint la distance de déplacement maximale vers le haut
            if (transform.position.y >= startPosition.y + moveDistance)
            {
                moveUp = false;
            }
        }
        else
        {
            // Déplacement vers le bas
            transform.Translate(Vector2.down * speed * Time.deltaTime);
            // Si la plateforme est descendue jusqu'à la position initiale moins la distance de déplacement
            if (transform.position.y <= startPosition.y - moveDistance)
            {
                moveUp = true;
            }
        }
    }

    // Détecte quand un objet entre en collision avec le déclencheur
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Vérifie si l'objet en collision a le tag "Player"
        if (collision.CompareTag("Player"))
        {
            playerOnPlatform = true;
        }
    }

    // Détecte quand un objet quitte la collision avec le déclencheur
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Vérifie si l'objet en collision a le tag "Player"
        if (collision.CompareTag("Player"))
        {
            playerOnPlatform = false;
        }
    }
}
