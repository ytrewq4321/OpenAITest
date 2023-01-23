using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private float mouseSens;
    [SerializeField] private float speed;
    [SerializeField] private Transform target;
    [SerializeField] private Transform fox;

    private bool mouseDown;
    private bool isChatOpened;
    private bool isChatClosed;

    private Vector3 foxPos;
    private Vector3 origPosition;
    private Quaternion origRotation;

    private void Start()
    {
        origPosition = new Vector3(transform.position.x,transform.position.y,transform.position.z);
        origRotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
        ChatUI.ChatOpened.AddListener(OnChatOpen);
        ChatUI.ChatClosed.AddListener(OnChatClose);
    }

    void Update()
    {
        if (Input.GetMouseButton(2))
            mouseDown = true;
        if (Input.GetMouseButtonUp(2))
            mouseDown = false;

        if (mouseDown)
        {
            float angle = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
            transform.RotateAround(target.position, Vector3.up, angle);

        }

        if(isChatOpened)
        {
            FocusOnFox();
        }
        else if(isChatClosed)
        {
            ReturnTransform();
            if (transform.position.Equals(origPosition))
                isChatClosed = false;
        }
    }

    private void  OnChatOpen()
    {
        isChatOpened = true;
        foxPos = new Vector3(fox.position.x, fox.position.y + 2f, fox.position.z - 10f);
    }

    private void OnChatClose()
    {
        isChatOpened = false;
        isChatClosed = true;
        transform.rotation = origRotation;
    }

    private void FocusOnFox()
    {
        transform.position = Vector3.MoveTowards(transform.position, foxPos, Time.deltaTime * speed);
        transform.LookAt(fox); 
    }

    public void ReturnTransform()
    {
        transform.position = Vector3.MoveTowards(transform.position, origPosition, Time.deltaTime * speed);
    }
}
