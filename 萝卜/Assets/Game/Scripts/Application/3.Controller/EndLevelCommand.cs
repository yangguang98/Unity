using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class EndLevelCommand:Controller 
{
    public override void Execute(object data)
    {
        EndLevelArgs e = data as EndLevelArgs;
        
        //保存游戏状态
        GameModel gm = GetModel<GameModel>();
        RoundModel rm = GetModel<RoundModel>();

        //停止协程
        rm.StopRound();

        //停止游戏
        gm.StopLevel(e.IsSuccess);

        //弹出UI
        if(e.IsSuccess )
        {
            GetView<UIWin>().Show(); 
        }
        else
        {
            GetView<UILost>().Show();
        }
        

    }
}
