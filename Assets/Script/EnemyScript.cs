using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;


public class EnemyScript : MonoBehaviour
{
    [SerializeField] private Transform _playerPos;
    [SerializeField] private ragdollController _ragdollController;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _mapSprite;
    [SerializeField] private EnemySoundManager _enemySoundManager;
    [SerializeField] private GameObject _bloodSample;
    [SerializeField] private EnemyManager _enemyManager;
    [SerializeField] private float _respawnTime;

    public NavMeshAgent agent;
    public bool _dead = false;

    private Collider _collider;
    [SerializeField]private float _enemyHealth = 100;
    bool _hit = false;
    float distance;
    private GameObject _player;
    private PlayerHealth _hp;
    private bool _walking;
    private bool _isAttacking;
    private float _walkTimer;

    private void Start()
    {
        _enemyManager = transform.parent.GetComponent<EnemyManager>();
        _player = GameObject.FindWithTag("Player");
        _playerPos = _player.GetComponent<Transform>();
        _hp = _player.GetComponent<PlayerHealth>();
        _collider = GetComponent<Collider>();
        agent.SetDestination(_enemyManager.RandomPosition());
    }


    private void Update()
    {
        if (_dead)
        {
            return;
        }
        checkDestination();
        checkSpeed();
    }

    public void bulletHit(float _damage)
    {
        DecreaseHealth(_damage);
        _hit = true;

        if (_enemyHealth <= 0)
        {
            OnDead();
        }
    }

    private void checkDestination()
    {
        distance = Vector3.Distance(transform.position, _playerPos.position);
        if ((distance < 30f || _hit) && _enemyHealth > 0)
        {
            agent.speed = 7f;
            agent.SetDestination(_playerPos.position);
        }
        else
        {
            FreeRoaming();
        }

        if (distance < 1.2f)
        {
            _animator.SetBool("IsAttacking", true);
        }
        else
        {
            _animator.SetBool("IsAttacking", false);
        }
    }
    private void checkSpeed()
    {
        _animator.SetFloat("Movement", agent.velocity.magnitude/7f);
    }
    public void DecreaseHealth(float _damage)
    {
        _enemyHealth = _enemyHealth - _damage;
    }

    public void DealDamage()
    {
        if (distance < 1.5f)
        {
            _hp.changeHealth(-10);
        }
    }

    private void OnDead()
    {
        agent.isStopped = true;
        _dead = true;
        _mapSprite.color = Color.yellow;
        _animator.enabled = false;
        _collider.enabled = false;
        _ragdollController.activeRagDoll();
        _enemySoundManager.StopAllSound();
        Invoke("ResetAndRespawn", _respawnTime);
        DropItem();
        Invoke("DeactivatingEnemy", 5f);
    }
    private void DeactivatingEnemy()
    {
        gameObject.SetActive(false);
    }

    private void ResetAndRespawn()
    {
        gameObject.SetActive(true);
        _dead = false;
        _collider.enabled = true;
        _enemyHealth = 100;
        _hit = false;
        distance = 0f;
        _walking = false;
        _isAttacking = false;
        _ragdollController.DeactiveRagDoll();
        _animator.enabled = true;
        _mapSprite.color = Color.red;
        transform.position = _enemyManager.RandomPosition();
        agent.SetDestination(_enemyManager.RandomPosition());
        agent.isStopped = false;
        _walkTimer = 0;
    }

    private void FreeRoaming()
    {
        agent.speed = 1f;
        _walkTimer += Time.deltaTime;
        if (_walkTimer > 20f)
        {
            _walkTimer = 0;
            agent.SetDestination(_enemyManager.RandomPosition());
        }
    }

    private void DropItem()
    {
        Instantiate(_bloodSample, transform.position, Quaternion.identity);
    }
}


