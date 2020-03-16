using UnityEngine;


public class PlayerControler : MonoBehaviour
{
    public float MoveSpeed;
    public float RotateAngel, Time, T;
    public bool MyControl;
    
    public Transform AmmoPoss;

    public GameObject AmmoObject;

    Rigidbody _body;

    public void FixedUpdate()
    {
        if (MyControl)
        {
            MyMove();
            LookAtCursor();
        }
        else if (Input.anyKey)
        {
            Move();
        }
        if (Input.GetButton("Fire1")||(Input.GetButton("Jump")))
        {
           Fire();
        }
        if (Input.GetButton("Fire2"))
            MyControl = !MyControl;
        Time += UnityEngine.Time.deltaTime;
    }
    private void MyMove()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");
        var moveDirection = new Vector3(moveHorizontal, 0, moveVertical);
        _body.AddForce(moveDirection.normalized * MoveSpeed);
    }
    private void Move()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            var angelRotation = Quaternion.AngleAxis(RotateAngel, Vector3.up);
            transform.rotation *= angelRotation;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            var angelRotation = Quaternion.AngleAxis(RotateAngel, -Vector3.up);
            transform.rotation *= angelRotation;
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            _body.AddForce(transform.forward * MoveSpeed);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            _body.AddForce(-transform.forward * MoveSpeed);
        }
    }
    private void LookAtCursor()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x,Input.mousePosition.y,Camera.main.transform.position.y));
        transform.LookAt(mousePos+Vector3.up*transform.position.y);
    }
    private void Fire()
    {
        if (Time > T)
        {        
           GameObject.Instantiate(AmmoObject, AmmoPoss.transform.position, transform.rotation.normalized);
           Time = 0f;
        }
    }
    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
    }
}


