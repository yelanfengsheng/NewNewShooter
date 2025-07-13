using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    private Animator anim;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

    }
    private void FixedUpdate()
    {
        if (rb != null)
        {
            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");

            Move(h, v);

            Rotation();

            Animing(h, v);
        }
    }
    public void Move(float h, float v)
    {
        Vector3 movements = new Vector3(h, 0, v);
        movements = movements.normalized * speed * Time.deltaTime;
        rb.MovePosition(this.transform.position + movements);
    }
    public void Rotation()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layMask = LayerMask.GetMask("Plane");
        RaycastHit hit;
        bool isPlane = Physics.Raycast(ray, out hit, 100, layMask);
        Debug.DrawLine(ray.origin, hit.point, Color.red);
        if (isPlane)
        {
            Vector3 off = hit.point - this.transform.position;
            off.y = 0;
            Quaternion quaternion = Quaternion.LookRotation(off);
            rb.MoveRotation(quaternion);
        }

    }
    public void Animing(float h, float v)
    {
        bool isW = false;
        if (Mathf.Abs(h) > 0 || Mathf.Abs(v) > 0)
        {
            isW = true;
            anim.SetBool("IsWalking", isW);
        }
        else
        {
            isW = false;
            anim.SetBool("IsWalking", isW);
        }
    }
        
}
