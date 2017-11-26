using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Monster))]
[RequireComponent(typeof(NavMeshAgent))]
public class THC1_Anim : MonoBehaviour {

    Monster _monster;

    Animator _anim;

    NavMeshAgent _navMeshAgent;

	// Use this for initialization
	void Start () {
        _monster = GetComponent<Monster>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _anim = GetComponent<Animator>();

        _monster.AggroEvent += _monster_AggroEvent;
        _monster.AttackEvent += _monster_AttackEvent;
		_monster.MoveEvent += OnMove;

        ToIdle();

        _monster.DevourEvent += _monster_DevourEvent;

        _monster.ToIdleEvent += ToIdle;
    }

    public void ToIdle()
    {

        _anim.SetInteger("moving", 0);
        _anim.SetInteger("battle", 0);
    }

    private void _monster_DevourEvent()
    {
        _monster.canMove = false;


        float preWait = _anim.GetCurrentAnimatorStateInfo(0).length;

        StartCoroutine(CrouchingRoutine(preWait));

    }

    IEnumerator CrouchingRoutine(float preWait)
    {
        yield return new WaitForSeconds(preWait);
        _anim.SetInteger("moving", 0);
        _anim.SetInteger("battle", 2);
        
        float duration = _anim.GetCurrentAnimatorStateInfo(0).length;

        yield return new WaitForSeconds(duration);
        _anim.SetInteger("moving", 17);


    }

    private void _monster_AttackEvent()
    {
        _anim.SetInteger("moving", 0);
        _monster.canMove = false;

        _attacking = true;


        float duration = _anim.GetCurrentAnimatorStateInfo(0).length;
        
        StartCoroutine(AttackEnded(duration));
    }

	void OnMove()
	{
		_anim.SetInteger("moving", 2);
	}

    [SerializeField]
    float attackFrameHit=0.5f;
    IEnumerator AttackEnded(float duration)
    {
        yield return null;
        int val = (int)(Random.value * 4) + 4;


        _anim.SetInteger("moving", val);

        float timer = 0f;

        bool hit = false;

        while (timer < duration)
        {
            yield return null;
            timer += Time.deltaTime;
            if(timer>=attackFrameHit&&!hit)
            {
                hit=true;
                _monster.AnimationHit();
            }
        }
        _monster.AttackAnimationEnded();
        _attacking = false;
		_monster.canMove = true;
		_anim.SetInteger("moving", 2);
    }

    private void _monster_AggroEvent()
    {
        _monster.canMove = false;

        _anim.SetInteger("moving", 8);
        _anim.SetInteger("battle", 1);

        float duration= Mathf.Max( _anim.GetCurrentAnimatorStateInfo(0).length, _anim.GetCurrentAnimatorStateInfo(1).length);

        StartCoroutine(ToRunRoutine(duration));



    }

    IEnumerator ToRunRoutine(float d)
    {
        yield return null;
        _anim.SetInteger("moving", 2);

        yield return new WaitForSecondsRealtime(d);

        _roaring = false;
        _monster.canMove = true;
    }

    bool _attacking = false;
    public bool attacking
    {
        get
        {
            return _attacking;
        }
    }
    
    bool _roaring = false;
    public bool roaring
    {
        get
        {
            return _roaring;
        }
    }
    
    //private void OnGUI()
    //{

    //    GUILayout.Label("moving: " + _anim.GetInteger("moving"));
    //    GUILayout.Label("battle: " + _anim.GetInteger("battle"));
    //}

}
