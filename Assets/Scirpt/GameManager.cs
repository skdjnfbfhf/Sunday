using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject prefabUnit;
    public LeaderManager leaderManager;
    public List<Unit> enemys = new List<Unit>();
    public int enemyLv;
    public Transform[] Board;
    /// <summary>
    /// 0 : 근거리, 1: 원거리, 2 : 마법사
    /// </summary>
    public Text[] boardJob;
    public Text[] boardLv;
    public Text[] boardExp;
    public Text[] boardHp;
    public Text[] boardMP;
    public Text[] boardAtk;
    public Text[] Skill1;
    public Text[] Skill2;
    public Text[] Skill3;
    public Text[] Inv;
    public Text[] leader;

    enum eBoard
    {
        Leader,
        Job,
        Lv,
        Exp,
        HP,
        MP,
        Atk,
        Skill,
        Inv
    }
    UnitInfo lvUnitInfo;
    public GameObject BackLvBox;

    private void Start()
    {
        SetPlayerTeam();
        if (leaderManager == null)
        {
            leaderManager = GetComponent<LeaderManager>();
        }
        if (leaderManager != null)
        {
            leaderManager.SetPlayerLeader();
        }
        
        enemyLv = 0;
        Invoke("SetEnemyTeam", 5.0f);

    }

    public void SetPlayerTeam()
    {
        for (int i = 0; i<3; i++)
        {
            Unit unit = Instantiate(prefabUnit).GetComponent<Unit>();
            unit.unitInfo.team = Team.Player;
            unit.unitInfo.playerType = (PlayerType)i;
            unit.unitInfo.SetInitPlayerStatus(unit.unitInfo.playerType);
            unit.gameObject.name = ((PlayerType)i).ToString();
            SetUI(i, unit.unitInfo);
        }
    }

    public void SetUI(int num, UnitInfo unitInfo)
    {
            unitInfo.playerType.ToString();

            boardJob[num].text = unitInfo.playerType.ToString();
            boardLv[num].text = unitInfo.curLv + "/" + unitInfo.maxLV;
            boardExp[num].text = unitInfo.exp + "/" + unitInfo.RequireExp();
            boardHp[num].text = unitInfo.curHP  + "/" + unitInfo.curHP;
            boardMP[num].text = unitInfo.maxMP + "/" + unitInfo.maxMP;
            boardAtk[num].text = unitInfo.attackDamage.ToString();
    }

    public void SetSkillUI(int num,  UnitInfo unitInfo)
    {
        lvUnitInfo = unitInfo;
        BackLvBox.SetActive(true);
    }

    public void BtnSkill(int num)
    {
        switch (num)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                switch (lvUnitInfo.playerType)
                {
                    case PlayerType.Mele:
                        {
                            int tempNum = -1;
                            for (int i = 0; i < lvUnitInfo.meleSkill.Length; i++)
                            {
                                if (lvUnitInfo.meleSkill[i] == (MeleSkill)(num + 1))
                                {
                                    tempNum = i;
                                    break;
                                }
                            }
                            if (tempNum == -1)
                            {
                                for (int i = 0; i < lvUnitInfo.skillLev.Length; i++)
                                {
                                    if (lvUnitInfo.skillLev[i] == 0)
                                    {
                                        tempNum = i;
                                        break;
                                    }
                                }
                                lvUnitInfo.meleSkill[tempNum] = (MeleSkill)(num + 1);
                                Time.timeScale = 1;
                                BackLvBox.SetActive(false);
                                lvUnitInfo.skillLev[tempNum]++;
                                Skill1[tempNum].text = ((MeleSkill)(num + 1)).ToString() + "Lv." + lvUnitInfo.skillLev[tempNum];

                                return;
                            }
                            if (lvUnitInfo.skillLev[tempNum] >= 5)
                            {
                                return;
                            }
                            lvUnitInfo.skillLev[tempNum]++;
                            Skill1[tempNum].text = ((MeleSkill)(num + 1)).ToString() + "Lv." + lvUnitInfo.skillLev[tempNum];


                        }

                        break;
                    case PlayerType.Ranged:
                        {
                            int tempNum = -1;
                            for (int i = 0; i < lvUnitInfo.rangeSkill.Length; i++)
                            {
                                if (lvUnitInfo.rangeSkill[i] == (RangeSkill)(num + 1))
                                {
                                    tempNum = i;
                                    break;
                                }
                            }
                            if (tempNum == -1)
                            {
                                for (int i = 0; i < lvUnitInfo.skillLev.Length; i++)
                                {
                                    if (lvUnitInfo.skillLev[i] == 0)
                                    {
                                        tempNum = i;
                                        break;
                                    }
                                }
                                lvUnitInfo.rangeSkill[tempNum] = (RangeSkill)(num + 1);
                                Time.timeScale = 1;
                                BackLvBox.SetActive(false);
                                lvUnitInfo.skillLev[tempNum]++;
                                Skill2[tempNum].text = ((MeleSkill)(num + 1)).ToString() + "Lv." + lvUnitInfo.skillLev[tempNum];
                                return;
                            }
                            if (lvUnitInfo.skillLev[tempNum] >= 5)
                            {
                                return;
                            }
                            lvUnitInfo.skillLev[tempNum]++;
                            Skill2[tempNum].text = ((MeleSkill)(num + 1)).ToString() + "Lv." + lvUnitInfo.skillLev[tempNum];

                        }

                        break;
                    case PlayerType.Magic:
                        {
                            int tempNum = -1;
                            for (int i = 0; i < lvUnitInfo.magicSkill.Length; i++)
                            {
                                if (lvUnitInfo.magicSkill[i] == (MagicSkill)(num + 1))
                                {
                                    tempNum = i;
                                    break;
                                }
                            }
                            if (tempNum == -1)
                            {
                                for (int i = 0; i < lvUnitInfo.skillLev.Length; i++)
                                {
                                    if (lvUnitInfo.skillLev[i] == 0)
                                    {
                                        tempNum = i;
                                        break;
                                    }
                                }
                                lvUnitInfo.magicSkill[tempNum] = (MagicSkill)(num + 1);
                                Time.timeScale = 1;
                                BackLvBox.SetActive(false);
                                lvUnitInfo.skillLev[tempNum]++;
                                Skill3[tempNum].text = ((MeleSkill)(num + 1)).ToString() + "Lv." + lvUnitInfo.skillLev[tempNum];
                                return;
                            }
                            if (lvUnitInfo.skillLev[tempNum] >= 5)
                            {
                                return;
                            }
                            lvUnitInfo.skillLev[tempNum]++;
                            Skill3[tempNum].text = ((MeleSkill)(num + 1)).ToString() + "Lv." + lvUnitInfo.skillLev[tempNum];

                        }

                        break;
                }
                break;

            case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                {
                    foreach(Unit u in leaderManager.units)
                    {
                        u.unitInfo.commonSkilLev[(num - 4)]++;
                    }
                }
                break;
            
        }
        Time.timeScale = 1;
        BackLvBox.SetActive (false);
    }


    //Game
    public void SetUILeader(int num, UnitInfo unitInfo)
    {
        for (int i = 0; i < Board.Length; i++)
        {
            if(i == num)
            {

            }
            else
            {

            }
        }
    }


    public void SetEnemyTeam()
    {
        switch (enemyLv)
        {
            case 0:
                int num = UnityEngine.Random.Range(2, 6);
                for (int i = 0; i < num; i++)
                {
                    Unit unit = Instantiate(prefabUnit, new Vector3(5.0f, 0, 0),
                        prefabUnit.transform .rotation).GetComponent<Unit>();
                    unit.unitInfo.team = Team.Enemy;
                    unit.unitInfo.enemyType = EnemyType.mele1;
                    unit.currentTarget = leaderManager.currentLeader.transform;
                    unit.Leader = leaderManager.currentLeader.transform;
                    unit.gameObject.name = EnemyType.mele1.ToString();
                    unit.unitInfo.SetInitEnemyStatus(unit.unitInfo.enemyType);
                    enemys.Add(unit);
                }
                break;

        }
           
            
        
    }


}
