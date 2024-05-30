using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Deplacer_Perso : MonoBehaviour
{
   
    public float vitesse = 100.0f;
    
    
    public float reculForce = 300f;
   
   
    private Rigidbody2D rb;
    
    
    private SpriteRenderer spriteRenderer;
    
    // Clips audio pour les bonus et les dégâts
    public AudioClip bonus;
    public AudioClip damage;
    public AudioClip jump;
    public AudioClip running;

    public Transform groundCheck;
    public LayerMask groundLayer;
    bool isGrounded;
    
    // Source audio pour jouer les sons
    AudioSource sourceAudio;

  

    // Start is called before the first frame update 
    void Start()
    {
        // Récupération des composants nécessaires
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        sourceAudio = GetComponent<AudioSource>();
        // Lancement de l'audio et ajustement du volume
        sourceAudio.Play();
        sourceAudio.volume = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = 0f;

        // Contrôle du déplacement horizontal basé sur les entrées du clavier
        if (Input.GetKey("a"))
        {
            spriteRenderer.flipX = true; 
            moveX = -vitesse * 1.5f; 
            GetComponent<Animator>().SetBool("course", true);
            
        }
        else if (Input.GetKey("d"))
        {
            spriteRenderer.flipX = false; 
            moveX = vitesse * 1.5f; 
            GetComponent<Animator>().SetBool("course", true);
            
        }
        else
        {
            GetComponent<Animator>().SetBool("course", false);
            
        }


        // Vérification si le personnage est au sol
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        

        // Maintien de la composante verticale de la vitesse
        float moveY = rb.velocity.y;

        // Permettre le saut si 'w' est pressé et que le personnage est au sol
        if (Input.GetKeyDown("w") && isGrounded)
        {
            moveY = 25f; 
            GetComponent<AudioSource>().PlayOneShot(jump);

        }

        // Application de la nouvelle vélocité
        rb.velocity = new Vector2(moveX, moveY);
    }

   

    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
        }
    }

    // Gestion des collisions avec d'autres objets
    void OnCollisionEnter2D(Collision2D infoObjetToucher)
    {
        // Collision avec des objets utiles
        if (infoObjetToucher.gameObject.CompareTag ("Heal1"))
        {
            healthMnagaer.health++;
            Destroy(infoObjetToucher.gameObject); 
            GetComponent<AudioSource>().PlayOneShot(bonus); 
        }

        // Collision avec des ennemies
        if (infoObjetToucher.gameObject.CompareTag("Ennemy"))
        {   
            
            healthMnagaer.health--;
            if(healthMnagaer.health <= 0)
            {
                playerManager.isGameOver = true;
            }
            else
            {
                StartCoroutine(Gethurt());
            }
            
            GetComponent<AudioSource>().PlayOneShot(damage); 

           
            Vector2 reculDirection = (rb.transform.position - infoObjetToucher.transform.position).normalized;
            rb.AddForce(reculDirection * reculForce);
        }

        if (infoObjetToucher.gameObject.name =="door")
        {
            playerManager.isGameWin=true;
        }

    }
    // Action apres la collision avec un ennemi
    IEnumerator Gethurt()
    {
        Physics2D.IgnoreLayerCollision(6,8, true);
        GetComponent<Animator>().SetLayerWeight(1, 1);
       
        yield return new WaitForSeconds(3);
        GetComponent<Animator>().SetLayerWeight(1, 0);

        Physics2D.IgnoreLayerCollision(6,8, false);
    }

 
}
