using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.SocialPlatforms.Impl;

public class Unit : MonoBehaviour
{
    public UnitInfo unitInfo;

    public Transform Leader;

    public Vector3 formOffset;

    public Vector3 DesireVelocity;

    public float attTimer;

    public Transform currentTarget;

    public List<Unit> tempTeam = new List<Unit>(20);

    public NavMeshAgent navMeshAgent;

    GameManager gameManager;

    public GameObject[] effectPrefab;

    public float[] skillCoolDown = new float[] { 0, 0, 0, 0, 0 };

    public Debuf debuf = Debuf.none;

    public float stunTimer = 2.0f;

    public float stunTiming = 2.0f;

    PotionInfo potionInfo = new PotionInfo();

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        unitInfo = GetComponent<UnitInfo>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }





   


    public void Damage(float dmg)
    {
        if(unitInfo.curHP <= 0f)
        {
            return;
        }
        unitInfo.curHP -= dmg;

        if(unitInfo.curHP <= 0f)
        {
            unitInfo.curHP = 0f;
            GameObject manager = GameObject.Find("GameManager");
            LeaderManager leaderManager = manager.GetComponent<LeaderManager>();
            GameManager gameManager = manager.GetComponent<GameManager>();
            if(leaderManager.currentLeader == this)
            {
                leaderManager.units.Remove(this);
                leaderManager.currentLeaderIndex = 0;
                leaderManager.ChangePlayerLeader(0);
                gameManager.SetPlayerTeam();
            }
            else
            {
                leaderManager.units.Remove(this);
                gameManager.enemys.Remove(this);
            }
            if(unitInfo.team == Team.Enemy)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }



    bool IsValidTarget(Transform tr)
    {
        if (tr == null)
        {
            return false;
        }
        Unit unit = tr.GetComponent<Unit>();

        if(unit == null)
        {
            return false;
        }
        if(unit.unitInfo.curHP<=0f)
        {
            return false;
        }
        if(unit.unitInfo.team == this.unitInfo.team)
        {
            return false;
        }
        return true;
    }



    private void FixedUpdate()
    {
        if(Leader == null)
        {
            return;
        }
        if(navMeshAgent == null)
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            return;
        }
        if(DesireVelocity.sqrMagnitude > 0.01f)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.SetDestination(DesireVelocity);
        }
        else
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero;
        }
    }

    private void Update()
    {
        if (debuf == Debuf.stun)
        {
            stunTiming -= Time.deltaTime;
            if (stunTiming > 0)
            {
                return;
            }
            else
            {
                stunTiming -= stunTimer;
                debuf = Debuf.none;
            }
        }
        if (currentTarget == null || IsValidTarget(currentTarget))
        {
            currentTarget = minDistanceTarget();
        }

        if (currentTarget != null)
        {
            float dist = Vector3.Distance(transform.position, currentTarget.position);
            if (dist <= unitInfo.attackRange)
            {
                unitInfo.status = Status.Attack;
            }
            else
            {
                unitInfo.status = Status.Chase;
            }
        }
        else
        {
            if (Leader != null)
            {
                unitInfo.status = Status.Follow;
            }
            else
            {
                unitInfo.status = Status.idle;
            }
        }
        switch (unitInfo.status)
        {
            case Status.Attack:
                DesireVelocity = Vector3.zero;
                Attack(currentTarget);

                break;

            case Status.Chase:
                Vector3 dir = currentTarget.position - transform.position;
                dir.y = 0;
                if (dir.sqrMagnitude < 0.1f)
                {
                    DesireVelocity = Vector3.zero;
                }
                else
                {
                    DesireVelocity = currentTarget.position;
                }
                break;

            case Status.Follow:
                Vector3 target = Leader.position + formOffset;
                DesireVelocity = target;
                break;

        }

    }


    



   


    public void LvUP()
    {
        if(unitInfo.exp > unitInfo.RequireExp())
        {
            unitInfo.exp -= unitInfo.RequireExp();
            unitInfo.curLv++;
            unitInfo.SetInitPlayerStatus(unitInfo.playerType);

            gameManager.SetUI((int)unitInfo.playerType, unitInfo);
            if (unitInfo.curLv % 2 == 1)
            {
                Time.timeScale = 0;
                gameManager.SetSkillUI((int)unitInfo.playerType, unitInfo);
            }
        }
        gameManager.SetUI((int)unitInfo.playerType, unitInfo);
    }


    void Attack(Transform target)
    {
        attTimer -= Time.deltaTime;
        if (attTimer > 0)
        {
            return;
        }
        if (target == null)
        {
            return;
        }
        if ((!IsValidTarget(target)))
        {
            return;
        }
        Unit targetUnit = target.GetComponent<Unit>();
        if (unitInfo.team == Team.Player)
        {
            //    GameObject effect = Instantiate(effectPrefab[(int)unitInfo.playerType],
            //        target.transform.position + new Vector3(0, 1.0f, 0),
            //        effectPrefab[(int)unitInfo.playerType], transform.rotation);


            if (targetUnit.unitInfo.curHP - unitInfo.attackDamage > 0)
            {
                unitInfo.exp += targetUnit.unitInfo.GetExp(unitInfo.attackDamage);
            }
            else
            {
                unitInfo.exp += targetUnit.unitInfo.GetExp(unitInfo.attackDamage);
                unitInfo.exp += targetUnit.unitInfo.GetBonusExp();
            }

            LvUP();
        }

        targetUnit.Damage(unitInfo.attackDamage);
        attTimer = unitInfo.attackRate;
    }


    Transform minDistanceTarget()
    {
        Collider[] col = Physics.OverlapSphere(transform.position, unitInfo.detectRaidus,
            LayerMask.GetMask("Unit"));

        Transform choice = null;

        float bestSqr = float.MaxValue;
        for (int i = 0; i < col.Length; i++)
        {
            Unit unit = col[i].GetComponent<Unit>();
            if (unit == null || unit.unitInfo.team == this.unitInfo.team)
            {
                continue;
            }
            float sq = (unit.transform.position - transform.position).sqrMagnitude;
            float distance = Vector3.Distance(unit.transform.position, transform.position);
            if (distance < bestSqr)
            {
                bestSqr = distance;
                choice = unit.transform;
            }

        }
        if (choice != null)
        {
            Debug.Log(choice.name);
        }
        return choice;
    }

}
