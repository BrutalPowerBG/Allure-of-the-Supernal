using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private bool doMovement = true;

    public float panSpeed = 30f;
    public float panBorderThickness = 20f;
    public float scrollSpeed = 1f;
    public float scrollAmount = 200f;

    private Vector3 offset;
    private Rigidbody cameraRigidbody;

    private float minY = 0f;
    private float maxY = 40f;
    private float minZ = -50f;
    private float maxZ = 50f;
    private float minX = -20f; 
    private float maxX = 65f;

    private void Start()
    {
        offset = GameObject.FindWithTag("Grid").GetComponent<BoardGrid>().transform.position;
        cameraRigidbody=GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            doMovement = !doMovement;
        }

        if (!doMovement)
            return;
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * panSpeed, Space.World);
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= panBorderThickness)
        {
            transform.Translate(Vector3.back * Time.deltaTime * panSpeed, Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            transform.Translate(Vector3.right * Time.deltaTime * panSpeed, Space.World);
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= panBorderThickness)
        {
            transform.Translate(Vector3.left * Time.deltaTime * panSpeed, Space.World);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float scrollSum = 0;
        scrollSum += scroll * 1000 * Time.deltaTime * scrollAmount;
        transform.Translate(Vector3.down * (scrollSum % scrollSpeed));
        scrollSum -= (scrollSum % scrollSpeed);

        Vector3 pos = transform.position;

        //pos.y -= scrollSum;
        pos.x = Mathf.Clamp(pos.x, minX + offset.x, maxX + offset.x);
        pos.y = Mathf.Clamp(pos.y, minY + offset.y, maxY + offset.y);
        pos.z = Mathf.Clamp(pos.z, minZ + offset.z, maxZ + offset.z);
        transform.position = new Vector3(pos.x, pos.y, pos.z);
    }
}
