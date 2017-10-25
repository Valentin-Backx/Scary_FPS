using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

[RequireComponent(typeof(NavMeshAgent))]
public class Monster : MonoBehaviour {

    Transform _myTransform;


    [SerializeField]
    float damage=10f;

    [SerializeField]
    Transform player;


    [SerializeField]
    float aggroDistance=8f;

    NavMeshAgent _navMeshAgent;


    [SerializeField]
    AudioSource aggroSound;

    Action CurrentBehavior;

    

	// Use this for initialization
	void Start () {

        _myTransform = transform;

        _navMeshAgent = GetComponent<NavMeshAgent>();
        CurrentBehavior = IdleBehavior;

        Player.Instance.GetComponent<Death>().PlayerDeathEvent += Monster_PlayerDeathEvent;
	}

    private void Monster_PlayerDeathEvent()
    {
        CurrentBehavior = DevourBehavior;

    }

    // Update is called once per frame
    void Update () {
        CurrentBehavior();

	}

    public event Action DevourEvent;
    bool _devouring;
    void DevourBehavior()
    {
        if(playerInRange)
        {
            if(!_devouring)
            {
                DevourEvent();
                _devouring = true;
            }
        }else
        {
            bool startMovingThisFrame = _navMeshAgent.isStopped && canMove;
            _navMeshAgent.isStopped = !canMove;
            if (startMovingThisFrame)
            {
                MoveEvent();
            }
            _navMeshAgent.SetDestination(player.position);
        }
    }


    public LayerMask layersToIgnore;

    void IdleBehavior()
    {
        RaycastHit hit;
        if (!_aggro && Physics.Raycast(_myTransform.position, player.position - _myTransform.position, out hit, aggroDistance,layersToIgnore))
        {
            if (hit.collider.CompareTag("Player"))
            {
                Aggro();
            }
        }
    }

    [HideInInspector]
    public bool canMove = true;
    void AggroBehavior()
    {
		bool startMovingThisFrame = _navMeshAgent.isStopped&&canMove;
		_navMeshAgent.isStopped = !canMove;
        
        if(!_attacking &&playerInRange)
        {
            _attacking = true;
            AttackEvent();
        }
        
		if(!_attacking)
		{
			if(startMovingThisFrame)
			{
				MoveEvent();
			}
			_navMeshAgent.SetDestination(player.position);
		}
    }

    public event Action AttackEvent;
    public void AnimationHit()
    {
        //inflict damages and shit
        if(playerInRange)
        {
            Player.Instance.Damage(damage);
        }
    }

    public void AttackAnimationEnded()
    {
        _attacking = false;
    }

    bool _attacking = false;
    public bool attacking
    {
        get
        {
            return _attacking;
        }
    }

    float playerDistance
    {
        get
        {
            return Vector3.Distance(_myTransform.position, player.position);
        }
    }

    /// <summary>
    /// is player in attack range
    /// </summary>
    bool playerInRange
    {
        get
        {
            return _navMeshAgent.stoppingDistance > playerDistance;
        }
    }

    bool _aggro = false;
    public bool aggro
    {
        get
        {
            return _aggro;
        }
    }

    public event Action AggroEvent;

	public event Action MoveEvent;

    void Aggro()
    {
        CurrentBehavior = AggroBehavior;
        _aggro = true;
//        _navMeshAgent.SetDestination(player.position);
//		MoveEvent();
        if(aggroSound)
        {
            aggroSound.Play();
        }
        AggroEvent();
		MySoundManager.AggroMusic();
    }

    //void OnGUI()
    //{
    //    GUILayout.Label("can move: " + this.canMove);
    //    GUILayout.Label("player in range: " + playerInRange);
    //}
}
