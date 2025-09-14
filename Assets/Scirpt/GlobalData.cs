using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum Team
    {
        Player,

        Enemy,

        Neutral
    }

    public enum PlayerType
    {
        Mele,

        Ranged,

        Magic

    }


    public enum EnemyType
    {
        mele1,

        mele2,

        mele3,

        range1,

        range2,

        range3,

        boss1,
        
        boos2,

        boss3
    }

    public enum Boss1Skill
    {
        summonMele1,

        summonMele2
    }

    public enum Boss2Skill
    {
        summonMele2,

        summonMele3,

        summonRange2,

        summonRange3
    }

    public enum Boss3Skill
    {
        summonMele3,

        summonRange3,

        HideUI,

        ChoseScreen
    }

    public enum Status
    {
        idle,

        Follow,
        
        MoveTo,

        Chase,

        Attack,

        Dead
    }

    public enum MeleSkill
    {
        none,

        mele,

        multi,

        piercing,

        stun
    }

    public enum RangeSkill
    {
        none,

        conbo,
        
        multi,
        
        anglePiercing,

        aoe
    }


    public enum MagicSkill 
    {
        none,

        combo,

        chain,

        heal,

        poison
    }

    public enum CommonSkill
    {
        criticalRateUp,

        attackSpeedUP,

        HPIP,

        moveSpeedUp,

        attackDamageUp
    }

    public enum ItemName
    {
        none,

        HP1,

        HP2,

        HP3,

        MP1,
        
        MP2,

        MP3,

        FlashBattle,

        MentalFocus,

        Revival
    }

    public enum Debuf
    {
        none,

        stun,

        poison
    }

    
    public class PotionInfo
    {
        public int[] Amount = new int[] {10, 30, 50};

        public float Cool = 2;
    }

