using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class StartUpCommand:Controller 
{

    public override void Execute(object data)
    {
        //注册模型（Model)
        RegisterModel(new GameModel());//模型中的数据需要常驻内存，因此需要实例化
        RegisterModel(new RoundModel());


        //注册控制器（Controller)
       
        RegisterController(Consts.E_EnterScene, typeof(EnterSceneCommand));//进入到场景中，在控制器这里注册了
        RegisterController(Consts.E_ExitScene , typeof(ExitSceneCommand));
        RegisterController(Consts.E_StartLevel, typeof(StartLevelCommand));
        RegisterController(Consts.E_EndLevel, typeof(EndLevelCommand));
        RegisterController(Consts.E_CountDownComplete, typeof(CountDownCompleteCommand));

        RegisterController(Consts.E_UpgradeTower, typeof(UpgradeTowerCommand));
        RegisterController(Consts.E_SaleTower, typeof(SaleTowerCommand));


        //初始化
        GameModel gModel = GetModel<GameModel>();
        gModel.Initialize();//模型初始化


        //进入到开始界面
        Game.Instance.LoadScene(1);
    }
}
