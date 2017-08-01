using UnityEngine;
using System.Collections;
using System;
/// <summary>
/// 模型层：玩家核心数值
/// 功能：本类提供玩家核心数据的存取
/// </summary>
[Serializable]
public class PlayerKernalData
{

    #region 字段
    private float health;      //健康值

    private float magic;       //魔法数值

    private float attack;      //攻击力

    private float defence;     //防御力

    private float dexterity;   //敏捷度 

    private float maxHealth;                //最大值   每一个等级
    private float maxMagic;
    private float maxAttack;
    private float maxDefence;
    private float maxDexterity;



    private float attackByPro;               //道具增加的属性
    private float defenceByPro;
    private float dexterityByPro;

    //定义事件:玩家核心数值


    public static event PlayerKernalDataDelegate PlayerKernalDataEvent;            //玩家核心数值


    #endregion

    #region 属性
    public float Health
    {
        get { return health; }
        set
        {
            health = value;

            //事件调用
            if (PlayerKernalDataEvent != null)
            {
                KeyValuesUpdate e = new KeyValuesUpdate("Health", Health);
                PlayerKernalDataEvent(e);
            }
        }
    }

    public float Magic
    {
        get { return magic; }
        set
        {
            magic = value;
            //事件调用
            if (PlayerKernalDataEvent != null)
            {
                KeyValuesUpdate e = new KeyValuesUpdate("Magic", Magic);
                PlayerKernalDataEvent(e);
            }
        }
    }

    public float Attack
    {
        get { return attack; }
        set
        {
            attack = value;
            //事件调用
            if (PlayerKernalDataEvent != null)
            {
                KeyValuesUpdate e = new KeyValuesUpdate("Attack", Attack);
                PlayerKernalDataEvent(e);
            }
        }
    }

    public float Defence
    {
        get { return defence; }
        set
        {
            defence = value;
            //事件调用
            if (PlayerKernalDataEvent != null)
            {
                KeyValuesUpdate e = new KeyValuesUpdate("Defence", Defence);
                PlayerKernalDataEvent(e);
            }
        }
    }

    public float Dexterity
    {
        get { return dexterity; }
        set
        {
            dexterity = value;
            //事件调用
            if (PlayerKernalDataEvent != null)
            {
                KeyValuesUpdate e = new KeyValuesUpdate("Dexterity", Dexterity);
                PlayerKernalDataEvent(e);
            }
        }
    }

    public float MaxHealth
    {
        get { return maxHealth; }
        set
        {
            maxHealth = value;
            //事件调用
            if (PlayerKernalDataEvent != null)
            {
                KeyValuesUpdate e = new KeyValuesUpdate("MaxHealth", MaxHealth);
                PlayerKernalDataEvent(e);
            }
        }
    }


    public float MaxMagic
    {
        get { return maxMagic; }
        set
        {
            maxMagic = value;
            //事件调用
            if (PlayerKernalDataEvent != null)
            {
                KeyValuesUpdate e = new KeyValuesUpdate("MaxMagic", MaxMagic);
                PlayerKernalDataEvent(e);
            }
        }
    }


    public float MaxAttack
    {
        get { return maxAttack; }
        set
        {
            maxAttack = value;
            //事件调用
            if (PlayerKernalDataEvent != null)
            {
                KeyValuesUpdate e = new KeyValuesUpdate("MaxAttack", MaxAttack);
                PlayerKernalDataEvent(e);
            }
        }
    }


    public float MaxDefence
    {
        get { return maxDefence; }
        set
        {
            maxDefence = value;
            //事件调用
            if (PlayerKernalDataEvent != null)
            {
                KeyValuesUpdate e = new KeyValuesUpdate("MaxDefence", MaxDefence);
                PlayerKernalDataEvent(e);
            }
        }
    }


    public float MaxDexterity
    {
        get { return maxDexterity; }
        set
        {
            maxDexterity = value;
            //事件调用
            if (PlayerKernalDataEvent != null)
            {
                KeyValuesUpdate e = new KeyValuesUpdate("MaxDexterity", MaxDexterity);
                PlayerKernalDataEvent(e);
            }
        }
    }

    public float AttackByPro
    {
        get { return attackByPro; }
        set { attackByPro = value;
        //事件调用
        if (PlayerKernalDataEvent != null)
        {
            KeyValuesUpdate e = new KeyValuesUpdate("AttackByPro", AttackByPro);
            PlayerKernalDataEvent(e);
        }
        }
    }


    public float DefenceByPro
    {
        get { return defenceByPro; }
        set { defenceByPro = value;
        //事件调用
        if (PlayerKernalDataEvent != null)
        {
            KeyValuesUpdate e = new KeyValuesUpdate("DefenceByPro", DefenceByPro);
            PlayerKernalDataEvent(e);
        }
        }
    }


    public float DexterityByPro
    {
        get { return dexterityByPro; }
        set { dexterityByPro = value;
        //事件调用
        if (PlayerKernalDataEvent != null)
        {
            KeyValuesUpdate e = new KeyValuesUpdate("DexterityByPro", DexterityByPro);
            PlayerKernalDataEvent(e);
        }
        }
    }

    #endregion

    //公有构造函数
    public PlayerKernalData()
    {

    }

    //私有构造函数
    public PlayerKernalData(float health, float magic, float attack, float defence, float dexterity,
        float maxHealth, float maxMagic, float maxAttack, float maxDefence, float maxDexterity,
            float attackByPro, float defenceByPro, float DexteriryByPro)
    {
        this.Health = health;
        this.Magic = magic;
        this.Attack = attack;
        this.Defence = defence;
        this.Dexterity = dexterity;
        this.MaxMagic = maxMagic;
        this.MaxHealth = maxHealth;
        this.MaxAttack = maxAttack;
        this.MaxDefence = maxDefence;
        this.MaxDexterity = maxDexterity;
        this.AttackByPro = attackByPro;
        this.DefenceByPro = defenceByPro;
        this.DexterityByPro = DexterityByPro;
    }

    //public void Add(Object obj)
    //{

    //}
}
