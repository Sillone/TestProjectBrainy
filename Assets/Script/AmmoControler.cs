using UnityEngine;

public class AmmoControler : MonoBehaviour
{
    private int _live = 3;

    private Vector3 _direction;

    public float Speed = 0.2f;

    MessageBus _bus;
    void Start()
    {
        _bus = MessageBus.GetBus();
    }
    void Update()
    {
        if (_live <= 0)
            Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        //gameObject.GetComponent<Rigidbody>().AddForce(direction*speed);
        gameObject.transform.position += _direction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            _live--;
            var hitDirection = collision.GetContact(0).normal;
            _direction = Vector3.Reflect(_direction, hitDirection);
        }
        else if ((collision.gameObject.tag == "Bot") || (collision.gameObject.tag == "Player"))
        {
            var message = new MessageModel(collision.gameObject, MessageType.OnDead);
            _bus.SendMessage(message);
            Destroy(gameObject);
        }
        else
            Destroy(gameObject);

    }
    private void Awake()
    {
        _direction = gameObject.transform.forward;
    }
}
