using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    public int[] LVUPStatusHP = new int[] { 20, 15, 10 };


    public int[] LVUPStatusDamage = new int[] {10, 8, 10};

    public int[] skillLev = new int[] { 0, 0, 0, 0};

    public int[] commonSkilLev = new int[] {0, 0, 0, 0, 0};

    public int[] commonSkil1 = new int[] {0, 10, 20, 30, 40, 50};

    public float[] commonSkil2 = new float[] {0f, 0,5f, 1.0f, 1.5f, 2.0f, 2.5f, 3.0f};

    public int[] commonSkil3 = new int[] {0, 200, 400, 600, 800, 1000};

    public float[] commonSkil4 = new float[] { 0f, 0, 5f, 1.0f, 1.5f, 2.0f};

    public int[] commonSkil5 = new int[] { 0, 20, 40, 60, 80, 100};



    public int maxLV = 20;

    public int curLv = 0;

    public float baseHP = 100f;

    public float maxHP = 100f;

    public float curHP = 100f;

    public float maxMP = 50f;

    public float curMP = 50f;

    public float moveSpeed = 4.5f;

    public float criticalRate = 10f;

    public float turnSpeed = 720f;

    public float baseAttackDamage = 1f;

    public float attackDamage = 1f;

    public float attackRate = 1.0f;

    public float attackRange = 1.4f;

    public float detectRaidus = 1.0f;

    public float healDamage = 10f;

    public float arriveRadius = 0.6f;

    public float friendlyRadius = 1.5f;

    public float separationWeight = 4f;

    public float cohesionWeight = 1.0f;

    public float alignmentWeight = 0.5f;

    public float exp = 0f;

    public Team team = Team.Player;

    public Status status = Status.idle;

    public PlayerType playerType = PlayerType.Mele;
    public EnemyType enemyType = EnemyType.mele1;

    public MeleSkill[] meleSkill = new MeleSkill[] { MeleSkill.none, MeleSkill.none, MeleSkill.none, MeleSkill.none };
    public RangeSkill[] rangeSkill = new RangeSkill[] { RangeSkill.none, RangeSkill.none, RangeSkill.none, RangeSkill.none };
    public MagicSkill[] magicSkill = new MagicSkill[] { MagicSkill.none, MagicSkill.none, MagicSkill.none, MagicSkill.none };


    public Vector3[] formOffset = new Vector3[]
    {
        new Vector3(2.0f, 0, 2.0f),
        new Vector3(-2.0f, 0, 2.0f),
        new Vector3(-2.0f, 0, -2.0f)
    };

    public float GetExp(float Damage)
    {
        if (Damage > curHP)
        {
            return exp *curHP / maxHP;
        }
        return exp * Damage / maxHP;
    }

    public float RequireExp()
    {
        return 100f + curLv;
    }

    public void SetInitPlayerStatus(PlayerType _type)
    {
        playerType = _type;

        switch (playerType)
        {
            case PlayerType.Mele:
                baseHP = 100;
                maxHP = baseHP;
                curHP = maxHP;
                maxMP = 50;
                curMP = maxMP;
                baseAttackDamage = 20;
                attackDamage = baseAttackDamage;
                attackRange = 1.0f;
                criticalRate = 0.1f;
                break;


            case PlayerType.Ranged:
                baseHP = 80;
                maxHP = baseHP;
                curHP = maxHP;
                maxMP = 70;
                curMP = maxMP;
                baseAttackDamage = 15;
                attackDamage = baseAttackDamage;
                attackRange = 3.0f;
                criticalRate = 0.2f;
                break;

            case PlayerType.Magic:
                baseHP = 70;
                maxHP = baseHP;
                curHP = maxHP;
                maxMP = 150;
                curMP = maxMP;
                baseAttackDamage = 10;
                attackDamage = baseAttackDamage;
                attackRange = 3.0f;
                criticalRate = 0f;
                break;
        }
        maxHP = baseHP + (curLv * LVUPStatusHP[(int) _type]);
        curHP = maxHP;
        curMP = maxMP;
        attackDamage = baseAttackDamage + (curLv * LVUPStatusDamage[(int) _type]);
    }


    public void SetInitEnemyStatus(EnemyType _type)
    {
        enemyType = _type;

        switch (enemyType)
        {
            case EnemyType.mele1:
                baseHP = 50;
                maxHP = baseHP;
                curHP = maxHP;
                maxMP = 50;
                curMP = maxMP;
                baseAttackDamage = 10;
                attackDamage = baseAttackDamage;
                attackRange = 1.0f;
                criticalRate = 0.1f;
                exp = 100;
                break;

            case EnemyType.mele2:
                baseHP = 100;
                maxHP = baseHP;
                curHP = maxHP;
                maxMP = 50;
                curMP = maxMP;
                baseAttackDamage = 20;
                attackDamage = baseAttackDamage;
                attackRange = 1.0f;
                criticalRate = 0.1f;
                exp = 100;
                break;

            case EnemyType.mele3:
                baseHP = 150;
                maxHP = baseHP;
                curHP = maxHP;
                maxMP = 50;
                curMP = maxMP;
                baseAttackDamage = 30;
                attackDamage = baseAttackDamage;
                attackRange = 1.0f;
                criticalRate = 0.1f;
                exp = 100;
                break;


            case EnemyType.range1:
                baseHP = 80;
                maxHP = baseHP;
                curHP = maxHP;
                maxMP = 70;
                curMP = maxMP;
                baseAttackDamage = 15;
                attackDamage = baseAttackDamage;
                attackRange = 3.0f;
                criticalRate = 0.2f;
                exp = 120;
                break;

            case EnemyType.range2:
                baseHP = 80;
                maxHP = baseHP;
                curHP = maxHP;
                maxMP = 70;
                curMP = maxMP;
                baseAttackDamage = 15;
                attackDamage = baseAttackDamage;
                attackRange = 3.0f;
                criticalRate = 0.2f;
                exp = 120;
                break;

            case EnemyType.range3:
                baseHP = 80;
                maxHP = baseHP;
                curHP = maxHP;
                maxMP = 70;
                curMP = maxMP;
                baseAttackDamage = 15;
                attackDamage = baseAttackDamage;
                attackRange = 3.0f;
                criticalRate = 0.2f;
                exp = 120;
                break;

            case EnemyType.boss1:
                baseHP = 70;
                maxHP = baseHP;
                curHP = maxHP;
                maxMP = 150;
                curMP = maxMP;
                baseAttackDamage = 10;
                attackDamage = baseAttackDamage;
                attackRange = 3.0f;
                criticalRate = 0f;
                break;

            case EnemyType.boos2:
                baseHP = 70;
                maxHP = baseHP;
                curHP = maxHP;
                maxMP = 150;
                curMP = maxMP;
                baseAttackDamage = 10;
                attackDamage = baseAttackDamage;
                attackRange = 3.0f;
                criticalRate = 0f;
                break;

            case EnemyType.boss3:
                baseHP = 70;
                maxHP = baseHP;
                curHP = maxHP;
                maxMP = 150;
                curMP = maxMP;
                baseAttackDamage = 10;
                attackDamage = baseAttackDamage;
                attackRange = 3.0f;
                criticalRate = 0f;
                break;
        }
        maxHP = baseHP + (curLv * LVUPStatusHP[(int)_type]);
        curHP = maxHP;
        curMP = maxMP;
        attackDamage = baseAttackDamage + (curLv * LVUPStatusDamage[(int)_type]);
    }



    public float GetBonusExp()
    {
        return exp / 10;
    }

}
