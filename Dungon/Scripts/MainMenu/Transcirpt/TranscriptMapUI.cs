using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TaidouCommon.Model;


public class TranscriptMapUI : MonoBehaviour {

    public static TranscriptMapUI _instance;
    private TweenPosition tween;
    private TranscriptMapDialog dialog;
    private BtnTranscript btnTranscriptCurrent;//当前显示的btnTranscript
    private BattleController battleController;
    private TimerDialog timerDialog;
    private Dictionary<int, BtnTranscript> transcriptDict = new Dictionary<int, BtnTranscript>();

    void Awake()
    {
        _instance = this;
        tween = this.GetComponent<TweenPosition>();
        dialog = transform.Find("TranscriptMapDialog").GetComponent<TranscriptMapDialog>();
        timerDialog = transform.Find("TimerDialog").GetComponent<TimerDialog>();
        BtnTranscript[] array = this.GetComponentsInChildren<BtnTranscript>();
        foreach (var temp in array)
        {
            //将所有的BtnTranscript加入到字典中
            transcriptDict.Add(temp.id, temp);
        }
    }

    void Start()
    {
        battleController = GameController.Instance.gameObject.GetComponent<BattleController>();
        battleController.OnGetTeam += this.OnGetTeam;
        battleController.OnCancelTeam += this.OnCancelTeamSuccess;
        battleController.OnWaitingTeam += this.OnWaitingTeam;
    }

    public void Show()
    {
        tween.PlayForward();
    }

    public void Hide()
    {
        tween.PlayReverse();
    }

    public void OnBack()
    {
        Hide();
    }

    public void OnBtnTranscript(BtnTranscript transcript)
    {
        //根据transcript中的数据，显示相应的内容
        btnTranscriptCurrent = transcript;//存储当前显示的btnTranscript的信息
        PlayerInfo info = PlayerInfo._instance;
        if(info.Level >=transcript.needLevel )
        {
            dialog.ShowDialog(transcript);
        }
        else
        {
            dialog.ShowWarn();
        }
    }

    public void ShowTranscriptEnter(int transcriptId)
    {
        //根据btntranscript 的ID自动显示某个DES
        BtnTranscript btnTranscript;
        transcriptDict.TryGetValue(transcriptId, out btnTranscript);//根据任务的transcriptid来决定显示那个transcript的内容
        OnBtnTranscript(btnTranscript);
    }

    public void OnEnterPerson()
    {
        if(PlayerInfo._instance .GetEnergy (btnTranscriptCurrent .needEnergy ))
        {
            GameController.Instance.battleType = BattleType.Person;
            GameController.Instance.transcriptId = btnTranscriptCurrent.id;//保存当前副本的id,方便任务计算和结果计算
            AsyncOperation operation = Application.LoadLevelAsync(btnTranscriptCurrent.sceneName);//不同的btnTranscript加载不同的场景
            LoadSceneProgressBar._instance.Show(operation);
        }
        else
        {
            MessageManager._instance.ShowMessage("体力不足，请稍后再试");
        }
        
    }

    public void OnEnterTeam()
    {
        //GameController.Instance.battleType = BattleType.Team;
        dialog.HideDialog();//隐藏TranscriptMapDialog,显示TimerDialog
        timerDialog.StartTimer();
        battleController.SendTeam();//发起组队的请求
    }

    public void OnCancelTeam()
    {
        //点击取消组队按钮
        battleController.CancelTeam();//向服务器发起取消组队请求
    }

    public void OnGetTeam(List<Role> roleList,int roleMasterId)
    {
        //响应服务器端组队成功
        //TODO
        //组队成功的回调响应
        if(PhotonEngine.Instance .role .ID ==roleMasterId )
        {
            GameController.Instance.isMaster = true;//当前客户端是否是主机，用于后面敌人的产生（敌人首先在主机上产生，然后在同步到客户端
        }
        if (PlayerInfo._instance.GetEnergy(btnTranscriptCurrent.needEnergy))
        {
            GameController.Instance.battleType = BattleType.Team;
            GameController.Instance .teamRoleList = roleList;
            GameController.Instance.transcriptId = btnTranscriptCurrent.id;//保存当前副本的id,方便任务计算和结果计算
            AsyncOperation operation = Application.LoadLevelAsync(btnTranscriptCurrent.sceneName);//不同的btnTranscript加载不同的场景
            LoadSceneProgressBar._instance.Show(operation);
        }
        else
        {
            MessageManager._instance.ShowMessage("体力不足，请稍后再试");
        }
    }

    public void OnWaitingTeam()
    {
        //响应服务器端正在等待组队
        print("OnWaitingTeam....");
    }

    public void OnCancelTeamSuccess()
    {
        //响应服务器端取消组队成功
        print("OnCancelTeam Success");
    }


    void OnDestroy()
    {
        if(battleController !=null)
        {
            battleController.OnGetTeam -= this.OnGetTeam;
            battleController.OnCancelTeam -= this.OnCancelTeamSuccess;
            battleController.OnWaitingTeam -= this.OnWaitingTeam;
        }
        
    }
}
