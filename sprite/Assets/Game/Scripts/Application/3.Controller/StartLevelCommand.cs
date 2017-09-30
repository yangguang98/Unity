using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class StartLevelCommand:Controller 
{

    public override void Execute(object data)
    {
        StartLevelArgs e = data as StartLevelArgs;

        //跳场景之前，把相应的数据准备好

        //在玩哪一个关卡
        GameModel gModel = GetModel<GameModel>();
        gModel.StartLevel(e.LevelIndex);//改变一些游戏参数,,改变当前正在玩的关卡

        //在玩那个关卡下的那些回合数
        RoundModel rModel = GetModel<RoundModel>();
        rModel.LoadLevel(gModel.PlayerLevel);


        //进入游戏关卡
        Game.Instance.LoadScene(3);

    }
}
