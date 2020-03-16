using UnityEngine;
using UnityEngine.AI;
public class BotController : MonoBehaviour
{
    [SerializeField]
    Transform _target;
    NavMeshAgent _navMeshAgent;

    public GameObject Ammo;
    public GameObject ShotArea;
    public LayerMask Wall;
    public LayerMask Player;

    public float SpeedAngel;
    public float ShootTime = 1f;
    float _timeAmmo;


    void Start()
    {   
        _navMeshAgent = gameObject.GetComponent<NavMeshAgent>();

    }
    private void FixedUpdate()
    {
        while (_target == null)
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
            _navMeshAgent.SetDestination(_target.position);
        }
        BotAim();
        _timeAmmo += Time.deltaTime;
    }
    void BotAim()
    {
        Vector3 dir = (_target.position - transform.position);
        var distTarget = Vector3.Distance(transform.position, _target.position);
        if (!Physics.Raycast(transform.position, dir.normalized, distTarget, Wall))
        {
            Quaternion rotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, SpeedAngel * Time.fixedDeltaTime);
            if (Physics.Raycast(transform.position, transform.forward, 1000f, Player))
            {
                BotFire(); 
            }
          
            _navMeshAgent.SetDestination(_target.position);
        }
        else if ((_navMeshAgent.remainingDistance < 5f) && Physics.Raycast(transform.position, dir.normalized, distTarget, Wall))
            _navMeshAgent.SetDestination(_target.position);
    }
    
   
    void BotFire()
    {
        if (_timeAmmo > ShootTime)
        {
            GameObject.Instantiate(Ammo, ShotArea.transform.position, transform.rotation);
            _timeAmmo = 0;
        }
    }
}
