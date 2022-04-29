using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;// touch Input

public class moveCharacter : MonoBehaviour//All compnent in Mono
{

    public float maxHealth = 100;
    public float currentHealth = 100;

    //public clear in window 
    Rigidbody2D rb;
    float move;
    public float moveSpeed = 5f;
    public float jumpPower = 200;
    public bool jump = false;
    public bool isGround = true;

    //Animator لربط مع القيم داخل 
    public Animator animator;
    
    bool isFaceLeft = false;

    // حتى نعيد اللاعب لمكانه
    private Vector3 currentPosition;


    // Start is called before the first frame update
    void Start()//first move character ,once
    {
        
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()//change frames
    {
        animator.SetFloat("Yvolcity",rb.velocity.y);
     
        //read any action between game,character
        // deal window of engine
        //input manager

       // move = Input.GetAxis("Horizontal");
        move = CrossPlatformInputManager.GetAxis("Horizontal");// input touch

        //Input.GetButtonUp("Jump");//Boolean
        animator.SetFloat("speed",Mathf.Abs(move));
        if (CrossPlatformInputManager.GetButtonUp("Jump") && isGround) {
            jump = true;
            isGround = false;
            animator.SetBool("jump", jump);
        }

        if (move > 0 && isFaceLeft) {
            Flip();
        } else if (move < 0 && !isFaceLeft) {
            Flip();
        }
    }

    void Flip() {

        isFaceLeft = !isFaceLeft;
        GetComponent<SpriteRenderer>().flipX = isFaceLeft;
       /* Vector3 rotate = transform.localScale; //(transform)
        rotate.x *= -1;
        transform.localScale = rotate;*/
    }
    void FixedUpdate()
    {//Apply action in engine
        rb.velocity = new Vector2(move * moveSpeed,rb.velocity.y);

        if(jump){
               jump = false;// simple jump , not long jump
               isGround = false;
               rb.AddForce(new Vector2(0,jumpPower));
        }

        if(transform.position.y < -7){
            GameManager.instance.PlayerGotDamged((int)maxHealth);
            transform.position  = currentPosition;
        }
    }
 
    //OnCollisionEnter:Collision in ground
    //OnCollisionStay: standUp in ground
    //OnCollisionExit: Exit ground


     private void OnCollisionEnter2D(Collision2D collision2D ){

         if(collision2D.gameObject.CompareTag("Ground") && collision2D.contacts[0].normal.y > 0.5){
                isGround = true;
                animator.SetBool("jump", !isGround); 
         }
        //Debug.Log("OnCollisionEnter2D");
    }
    private void OnCollisionExit2D(Collision2D collision2D){
        Debug.Log("OnCollisionExit2D");
    }
    private void OnCollisionStay2D(Collision2D collision2D){
        Debug.Log("OnCollisionStay2D");
    }


    // private void OnTriggerEnter2D(Collider2D collider2D)
    // {
    //     if (collider2D.gameObject.CompareTag("Coin")) {
    //       //  Destroy(collider2D.gameObject); 
    //     }
    // }

    // private void OnTriggerExit2D(Collider2D collider2D)
    // {

    // }

    // private void OnTriggerStay2D(Collider2D collider2D)
    // {

    // }
}



























