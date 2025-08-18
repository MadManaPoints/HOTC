using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private float xRotation;
    private float yRotation;
    public float sens = 500f;
    public Transform orientation;
    //private GameManager gameManager;

    void Start()
    {
        //gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        Cursor.lockState = CursorLockMode.Locked; 
    }

    void Update()
    {
        CameraControls();
    }

    private void CameraControls(){
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sens;

        yRotation += mouseX;
        xRotation -= mouseY;

        //xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        //transform.rotation = Quaternion.Euler(0, yRotation, 0);
        //orientation.rotation = Quaternion.Euler(0, yRotation, 0); 
    }
}
