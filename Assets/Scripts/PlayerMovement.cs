using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.Splines;

public class PlayerMovement : MonoBehaviour
{
    public Transform orientation;
    [SerializeField] GameObject player, bulletPrefab;
    [SerializeField] Transform clip, bulletHolder;
    [Header("Inputs")]
    PlayerCamera playerCam;
    Rigidbody playerRb;
    Vector3 moveDirection;
    [SerializeField] Camera cam;
    float vInput, hInput, moveSpeed = 10.0f, maxSpeed = 20.0f;
    Vector3 centerScreen = new Vector3(0.5f, 0.5f, 0f);
    [SerializeField] Image ret;
    int clipSize = 8;
    float reloadTime = 2.0f;
    bool reload;
    public SplineAnimate spline;
    [SerializeField] Transform rObject;
    bool reflect;
    Vector3 pos;

    void Awake()
    {
        spline = this.GetComponent<SplineAnimate>();

        transform.position = Vector3.up;
        transform.localEulerAngles = Vector3.zero;
        pos = rObject.position;
    }

    void Start()
    {
        playerCam = this.GetComponent<PlayerCamera>();
        playerRb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        InputHandler();
        Casting();
        Shoot();
        if(reflect) 
        {
            pos = Vector3.Reflect(transform.position, -transform.position.normalized);
            pos.y = 2;
            rObject.position = pos;
        }

        Debug.Log(rObject.position);
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void InputHandler()
    {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");
    }

    void MovePlayer()
    {
        orientation.localEulerAngles = new Vector3(orientation.localEulerAngles.x, cam.transform.localEulerAngles.y, orientation.localEulerAngles.z);
        Vector3 inputDir = orientation.forward * vInput + orientation.right * hInput;
        inputDir.y = playerRb.linearVelocity.y;

        //print(orientation);
        player.transform.localEulerAngles = orientation.localEulerAngles;


        playerRb.linearVelocity = (inputDir != Vector3.zero) ? inputDir * moveSpeed : Vector3.zero;

        if (playerRb.linearVelocity.magnitude > maxSpeed)
            playerRb.linearVelocity = Vector3.ClampMagnitude(playerRb.linearVelocity, maxSpeed);
    }

    void Casting()
    {
        Ray laser = Camera.main.ViewportPointToRay(centerScreen);
        RaycastHit hit = new RaycastHit();


        if (Physics.Raycast(laser, out hit))
        {
            if (hit.collider.gameObject.tag == "Ground")
            {
                ret.color = Color.green;
            }
            else
            {
                ret.color = Color.red;
            }
        }
        else
        {
            ret.color = Color.red;
        }
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && clipSize > 0)
        {
            Instantiate(bulletPrefab, clip.position, Quaternion.identity, bulletHolder);
            clipSize--;
        }

        if (clipSize == 0 && Input.GetKeyDown(KeyCode.R))
        {
            reload = true;
        }

        if (reload)
        {
            if (reloadTime >= 0f)
            {
                reloadTime -= Time.deltaTime;
            }
            else
            {
                clipSize = 8;
                reloadTime = 2.0f;
                reload = false;
            }
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Rail") spline.Play();

        if (col.gameObject.tag == "Rail Exit") 
        {
            spline.Pause();
        }

        if(col.gameObject.tag == "R Surface")
        {
            reflect = true;
        }
    }
}
