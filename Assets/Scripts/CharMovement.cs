using UnityEngine;
using UnityEngine.SceneManagement;

public class CharMovement : MonoBehaviour
{
    Rigidbody playerRb;
    float xMove;
    float yMove;
    float speed = 10f;

    public int health = 100;
    public int killCount = 0;

    public bool grounded = true;

    Camera PlayerCamera;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject gameScr;
    [SerializeField] GameObject hand;
    [SerializeField] GameObject door;
    private void Start()
    {
        Time.timeScale = 1;
        PlayerCamera = GetComponentInChildren<Camera>();
        playerRb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        //Exit to main menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
            Cursor.lockState= CursorLockMode.None;
        }

        if(Input.GetMouseButtonDown(0) && !transform.GetComponentInChildren<Gun>())
        {
            Punch();
        }

        PickUp();

        //Movement 
        if(grounded && Input.GetKeyDown(KeyCode.Space)){
            playerRb.AddForce(Vector3.up * 30);
            grounded = false;
        }

        xMove = Input.GetAxis("Horizontal");
        yMove = Input.GetAxis("Vertical");

        transform.Translate(Vector3.right * xMove * speed * Time.deltaTime);
        transform.Translate(Vector3.forward * yMove * speed * Time.deltaTime);

        //Stops the game, because Player's health is 0
        if (health == 0)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            gameScr.SetActive(false);
            gameOver.SetActive(true);
        }
        else
        {
            gameScr.SetActive(true);
            gameOver.SetActive(false);
        }

        if(door.transform.position.y < 4.5f)
        {
            if (killCount == 4)
            {
                door.transform.Translate(Vector3.up * 5 * Time.deltaTime);
            }
        }
    }

    //Code for Punch
    void Punch()
    {
        RaycastHit hit;

        if(Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, 5))
        {
            Enemy enemy = hit.transform.GetComponent<Enemy>();
            if(enemy != null)
            {
                enemy.Hurt(10);
            }
        }
    }

    //Function to Pickup the Gun
    void PickUp()
    {
        RaycastHit hit;
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, 20)){
            Gun gun = hit.transform.GetComponent<Gun>();
            Transform cam = PlayerCamera.transform;

            //Picking up the gun by setting it as a child of the player 
            if (gun != null && Input.GetKeyDown(KeyCode.E))
            {
                gun.transform.position = hand.transform.position;
                gun.transform.localRotation = cam.transform.rotation;
                gun.transform.parent = cam.transform;
                hand.SetActive(false);
            }
        }
    }

    //Detects Collisons with ground and enemies
    void OnCollisionEnter(Collision collision)
    {
        //To avoid Double-jump
        if (collision.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }

        //Damages the enemy by 10 hit points
        if (collision.gameObject.CompareTag("Enemy"))
        {
            health -= 10;
        }
    }
}