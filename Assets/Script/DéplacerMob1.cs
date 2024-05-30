using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DéplacerMob1 : MonoBehaviour
{
    public GameObject PointA;
    public GameObject PointB; 
    private Rigidbody2D rb; 
    private Animator anime; 
    private Transform currentPoint; 
    public float speed; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        anime = GetComponent<Animator>(); 
        currentPoint = PointB.transform; 
        
    }

    // Update is called once per frame
    void Update()
    {
        // Détermine la direction vers le point courant
        Vector2 point = currentPoint.position - transform.position;
        // Définit la vitesse vers le point courant en fonction de la vitesse et de la direction
        if (currentPoint == PointB.transform)
        {
            rb.velocity = new Vector2(speed, 0); 
        }
        else
        {
            rb.velocity = new Vector2(-speed, 0); 
        }

        // Vérifie si l'objet est suffisamment proche du point cible pour envisager de changer de cible
        if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f)
        {
            if (currentPoint == PointB.transform)
            {
                currentPoint = PointA.transform; 
                flip(); 
            }
            else if (currentPoint == PointA.transform)
            {
                flip(); 
                currentPoint = PointB.transform; 
            }
        }
    }

    // Retourne l'échelle de l'objet horizontalement
    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    // Visualise les points et la ligne entre eux dans l'éditeur
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(PointA.transform.position, 0.5f); 
        Gizmos.DrawSphere(PointB.transform.position, 0.5f); 
        Gizmos.DrawLine(PointA.transform.position, PointB.transform.position); 
    }
}

