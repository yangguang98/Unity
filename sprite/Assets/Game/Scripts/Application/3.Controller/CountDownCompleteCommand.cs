using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class CountDownCompleteCommand:Controller 
{

    public override void Execute(object data)
    {

        GameModel gm = GetModel<GameModel>();
        gm.IsPlaying = true;

        //开始出怪
        RoundModel rModel = GetModel<RoundModel>();
        rModel.StartRound();
        
    }
}
