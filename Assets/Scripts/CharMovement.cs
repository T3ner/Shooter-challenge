using UnityEngine;
using UnityEngine.UI;

public class CharMovement : MonoBehaviour
{
    Rigidbody playerRb;
    float xMove;
    float yMove;
    float speed = 10f;

    int health = 100;

    public bool grounded = true;

    Camera PlayerCamera;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject gameScr;
    [SerializeField] GameObject hand;
    [SerializeField] GameObject door;

    OpenDoor openDoor;
    private void Start()
    {
        PlayerCamera = GetComponentInChildren<Camera>();
        playerRb = GetComponent<Rigidbody>();
        openDoor = door.GetComponent<OpenDoor>();
    }
    void Update()
    {
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
            health = 100;
            gameScr.SetActive(false);
            gameOver.SetActive(true);
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
                openDoor.enemies.Remove(enemy);
                enemy.Hurt(10);
            }
        }
    }

    //Function to Pickup the Gun
    void PickUp()
    {
        RaycastHit hit;
        if (Physics.Raycast(PlayerCamera.transform.position, PlayerCamera.transform.forward, out hit, 5)){
            Gun gun = hit.transform.GetComponent<Gun>();
            Transform cam = PlayerCamera.transform;

            //Picking up the gun by setting it as a child of the player 
            if (gun != null && Input.GetKeyDown(KeyCode.E))
            {
                gun.transform.position = cam.transform.position + new Vector3(1,-0.5f,-1.5f);
                gun.transform.rotation = Quaternion.Euler(90f,0f,-30f);
                gun.transform.parent = transform;
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
