using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TaidouCommon.Model;

public class SkillManager : MonoBehaviour {

    public static SkillManager _instance;
    public TextAsset skillInfoText;
    private ArrayList skillList = new ArrayList();
    private Dictionary<int,Skill> skillDict = new Dictionary<int, Skill>();
    private SkillDBController skillDBController;
    void Awake()
    {
        _instance = this;
        skillDBController = this.GetComponent<SkillDBController>();
        skillDBController.OnGetSkillDBList += OnGetSkillDBList;
        skillDBController.OnUpgradeSkillDB += OnUpgradeSkillDB;
        InitSkill();
        skillDBController.Get();
    }
    void InitSkill()
    {
        string[] skillArray = skillInfoText.ToString().Split('\n');
        foreach(string str in skillArray)
        {
            string[] proArray = str.Split(',');
            Skill skill = new Skill();
            skill.Id = int.Parse(proArray[0]);
            skill.Name = proArray[1];
            skill.Icon = proArray[2];
            switch(proArray [3])
            {
                case "Warrior":
                    skill.PlayerType = PlayerType.Warrior;
                    break;
                case "FemaleAssassin":
                    skill.PlayerType = PlayerType.FemaleAssassin;
                    break;
            }

            switch(proArray [4])
            {
                case "Basic":
                    skill.SkillType = SkillType.Basic;
                    break;
                case "Skill":
                    skill.SkillType = SkillType.Skill;
                    break;
            }

            switch(proArray[5])
            {
                case "Basic":
                    skill.PosType = PosType.Basic;
                    break;
                case "One":
                    skill.PosType = PosType.One;
                    break;
                case "Two":
                    skill.PosType = PosType.Two;
                    break;
                case "Three":
                    skill.PosType = PosType.Three;
                    break;
            }
            skill.ColdTime = int.Parse(proArray[6]);
            skill.Damage = int.Parse(proArray[7]);
            skill.Level = 1;
            skillList.Add(skill);
            skillDict.Add(skill.Id, skill);
        }
    }

    public Skill GetSkillByPosition(PosType posType)
    {
        PlayerInfo info = PlayerInfo._instance;
        foreach(Skill skill in skillList)
        {
            if(skill.PlayerType==info.PlayerType&&skill.PosType==posType)
            {
                //在任务信息和技能信息中都有playerType，则对于给定的位置有且只有一个技能
                
                return skill;
            }
        }

        return null;
    }

    public void OnGetSkillDBList(List<SkillDB> list)
    {
        //得到DBList后的回调
        foreach (var temp in list )
        {
            Skill skill=null;
            if(skillDict .TryGetValue (temp.SkillID ,out skill))
            {
                skill.Sync(temp);//将SKillDB的信息都存储起来
            }
        }

        if(OnSyncSkillComplete!=null)
        {
            OnSyncSkillComplete();//通知UI,作出UI 的显示
        }
    }

    public void SyncDataBase(Skill skill)
    {
        //将升级后的信息同步到数据库
        skillDBController.UpGrade(skill.SkillDB);
    }

    public void OnUpgradeSkillDB(SkillDB skillDB)
    {
        
        Skill skill = null;
        if (skillDict.TryGetValue(skillDB.SkillID, out skill))
        {
            skill.Sync(skillDB);//同步数据，数据的同步：使用服务器返回来的数据进行同步
        }
    }

    public event OnSyncSkillCompleteEvent OnSyncSkillComplete;
}
