using UnityEngine;
using System.Collections;
using TaidouCommon.Model;
using System.Collections.Generic;

public class StartMenu : MonoBehaviour {

    public static StartMenu _instance;

    public TweenScale startpanelTween;
    public TweenScale loginpanelTween;
    public TweenScale registepanelTween;
    public TweenScale serverpanelTween;

    public TweenPosition startpanelTweenPos;
    public TweenPosition characterselectTween;
    public TweenPosition charactershowTween;

    public static string username;//登录时储存的密码和用户名
    public static string password;
    public static ServerProperty1 sp;
    public static List<Role> roleList = null;//一个用户拥有的所有的角色,需要实时更新，当一个用户添加一个角色后，要更新这个roleList


    public UIInput usernameInputLogin;
    public UIInput passwordInputLogin;
    public UIInput characternameInput;

    public UILabel usernameLabelStart;
    public UILabel servernameLabelStart;
    public UILabel nameLabelCharacterselect;//掌握命名规则，后面要加上对应的面板
    public UILabel levelLabelCharacterselect;

    public UIInput usernameInputRegiste;
    public UIInput passwordInputRegiste;
    public UIInput repasswordInputRegiste;

    public UIGrid serverlistGrid;

    public GameObject serverItemGreen;
    public GameObject serverItemRed;

    private bool haveInitServerlsit = false;

    public GameObject serverSelectedGo;

    public GameObject[] characterArray;//用来展示已经选择的角色
    public GameObject[] characterSelectedArray;//用来供玩家选择的

    private GameObject characterSelected;//当前选择的角色

    public Transform CharacterSelectedParent;

    private LoginController loginController;
    private RegisterController registerController;
    private RoleController roleController;

    
    void Awake()
    {
        _instance = this;//单例模式
        loginController = this.GetComponent<LoginController>();
        registerController = this.GetComponent<RegisterController>();
        roleController = this.GetComponent<RoleController>();

        roleController.OnGetRole += OnGetRole;
        roleController.OnAddRole += OnAddRole;
        roleController.OnSelectRole += OnSelectRole;
    }
    void Start()
    {
        //InitServerlist();
    }

    void OnDestroy()
    {
        if(roleController !=null)
        {
            roleController.OnGetRole -= OnGetRole;
            roleController.OnAddRole -= OnAddRole;
        }
        
    }

    public void OnGetRole(List<Role> roleList)
    {
        //从服务器得到角色的信息后的处理
        Debug.Log("OnGetRole");
        StartMenu.roleList = roleList;
        if(roleList!=null&&roleList.Count >0)
        {
            //进入角色显示的界面
            Role role = roleList[0];
            ShowRole(role);
        }
        else
        {
            //进入角色创建的界面
            ShowRoleAddPanel();
        }
    }
    public void OnAddRole(Role role)
    {
        if(roleList ==null)
        {
            roleList = new List<Role>();
            roleList.Add(role);
        }
        else
        {
            roleList.Add(role);
        }
        ShowRole(role);

    }

    public void OnSelectRole()
    {
        characterselectTween.gameObject.SetActive(false);
        AsyncOperation operaion=Application.LoadLevelAsync(1);
        LoadSceneProgressBar._instance.Show(operaion);
    }

    public void ShowRole(Role role)//一个角色的显示界面
    {
        //在显示面板中显示role
        PhotonEngine.Instance.role = role;//这个role不是服务器返回来的。
        ShowCharacterselect();

        nameLabelCharacterselect.text = role.Name;
        levelLabelCharacterselect.text = "Lv" + role.Level;
        int index = -1;
        //得到角色的索引
        for (int i = 0; i < characterArray.Length; i++)
        {
            if ((characterArray[i].name.IndexOf ("boy")>=0&&role.IsMan)||(characterArray[i].name.IndexOf ("girl")>=0&&role.IsMan==false))
            {

                index = i;
                break;
            }
        }

        //通过寻找子物体的组件来得到子物体
        GameObject.Destroy(CharacterSelectedParent.GetComponentInChildren<Animation>().gameObject);//销毁存在的物体
        //创建新的角色
        GameObject go = GameObject.Instantiate(characterSelectedArray[index], Vector3.zero, Quaternion.identity) as GameObject;//看看   
        go.transform.parent = CharacterSelectedParent;//如何去设置父物体
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
    }


    public void OnUsernameClick()
    {
        //登录，输入账号
        startpanelTween.PlayForward();
        StartCoroutine(HidePanel(startpanelTween.gameObject));
        loginpanelTween.gameObject.SetActive(true);
        loginpanelTween.PlayForward();
    }
    public void OnserverClick()
    {
        //选择服务器
         startpanelTween.PlayForward();
         StartCoroutine(HidePanel(startpanelTween.gameObject));
         serverpanelTween.gameObject.SetActive(true);
         serverpanelTween.PlayForward();

         //InitServerlist();//初始化服务器列表
    }
    public void OnEnterGameClick()
    {
        loginController.Login(username, password);//向服务器发起登录请求，服务器返回信息，然后对返回的信息做相应的处理
        //2.进入角色选择界面
       
        //startpanelTweenPos.PlayForward();
        //HidePanel(startpanelTweenPos.gameObject);
        //characterselectTween.gameObject.SetActive(true);
        //characterselectTween.PlayForward();
    }

    public void OnGamePlay()
    {
        //点击进入游戏的按钮，和服务器进行交互，保存当前选择的角色
        roleController.SelectRole(PhotonEngine .Instance .role );
    }

    public void ShowCharacterselect()
    {
        characterselectTween.gameObject.SetActive(true);
        characterselectTween.PlayForward();
    }

    public void hideStartPanel()
    {
        startpanelTweenPos.PlayForward();
        StartCoroutine(HidePanel(startpanelTweenPos.gameObject));
    }

    IEnumerator HidePanel(GameObject go)
    {
        yield return new WaitForSeconds(0.4f);
        go.SetActive(false);

    }

    public void OnLoginClick()
    {
        //得到用户名和密码
        username = usernameInputLogin.value;
        password = passwordInputLogin.value;

        //返回开始界面
        loginpanelTween.PlayReverse();
        StartCoroutine(HidePanel(loginpanelTween.gameObject));
        startpanelTween.gameObject.SetActive(true);
        startpanelTween.PlayReverse();

        usernameLabelStart.text = username;
    }
    public void OnRegisteShowClick()
    {
        //隐藏当前面板，显示注册面板
        loginpanelTween.PlayReverse();
        StartCoroutine(HidePanel(loginpanelTween.gameObject));
        registepanelTween.gameObject.SetActive(true);
        registepanelTween.PlayForward();

    }
    public void OnLoginCloseClick()
    {
        loginpanelTween.PlayReverse();
        StartCoroutine(HidePanel(loginpanelTween.gameObject));
        startpanelTween.gameObject.SetActive(true);
        startpanelTween.PlayReverse();
    }

    public void OnCancelClick()
    {
        registepanelTween.PlayReverse();
        StartCoroutine(HidePanel(registepanelTween.gameObject));
        loginpanelTween.gameObject.SetActive(true);
        loginpanelTween.PlayForward();
    }

    public void OnRegisteCloseClick()
    {
        OnCancelClick();
    }

    public void OnRegisteAndLoginClick()
    {
        username = usernameInputRegiste.value;
        password = passwordInputRegiste.value;
        string rePassword = repasswordInputRegiste.value;//重复输入的密码
        //1.本地校验，连接服务器进行校验
        
        if(username ==null||username.Length <=3)
        {
            MessageManager._instance.ShowMessage("用户名不能少于三个字符");
            return;
        }
        if(password ==null||password .Length <=3)
        {
            MessageManager._instance.ShowMessage("密码不能少于三个字符");
            return;
        }
        if(password !=rePassword )
        {
            MessageManager._instance.ShowMessage("密码输入不一致");
            return;
        }
        registerController.Register(username, password);
      
    }

    public void HideRegisterPanel()
    {
        registepanelTween.PlayReverse();
        StartCoroutine(HidePanel(registepanelTween.gameObject));
    }

    public void ShowStartPanel()
    {
        startpanelTween.gameObject.SetActive(true);
        startpanelTween.PlayReverse();
    }

    public void InitServerlist()
    {
        if (haveInitServerlsit)
        {
            return;
        }

        //1.连接服务器 取得游戏服务器列表信息
        //TODO

        //2.根据上面的信息添加服务器列表

        for (int i = 0; i < 20;i++)
        {
            string ip = "192.0.01";
            string name = (i + 1) + "区 马达加斯加";
            int count = Random.Range(0, 100);
            GameObject go = null;
            if(count>50)
            {
                //火爆
                go=NGUITools.AddChild(serverlistGrid.gameObject, serverItemRed);//如何有格子得到物体

            }
            else
            {
                //流畅
                go = NGUITools.AddChild(serverlistGrid.gameObject, serverItemGreen);

            }
            ServerProperty1 sp = go.GetComponent<ServerProperty1>();
            sp.ip = ip;
            sp.name = name;
            sp.count = count;

            serverlistGrid.AddChild(go.transform);//看看
        }
            haveInitServerlsit = true;
    }

    
    public void OnServerselect(GameObject servergo)
    {
        //选中了某个服务器
        sp = servergo.GetComponent<ServerProperty1>();
        serverSelectedGo.GetComponent<UISprite>().spriteName = servergo.GetComponent<UISprite>().spriteName;
        serverSelectedGo.transform.Find("Label").GetComponent<UILabel>().text = sp.name;
        serverSelectedGo.transform.Find("Label").GetComponent<UILabel>().color = servergo.transform.Find("Label").GetComponent<UILabel>().color;
    }


    public void OnServerpanelClose()
    {
        //隐藏服务器列表
        serverpanelTween.PlayReverse();
        StartCoroutine(HidePanel(serverpanelTween.gameObject));
        //显示开始界面
        startpanelTween.gameObject.SetActive(true);
        startpanelTween.PlayReverse();

        servernameLabelStart.text = sp.name;
    }

    public void OnCharacterClick(GameObject go)
    {
        

        if(go==characterSelected)
        {
            
            //选中一个角色后，放大，，当在点击该角色的时候，该角色不会缩小
            return;
        }
        
        iTween.ScaleTo(go,new Vector3(1.5f,1.5f,1.5f),0.5f);//看看
        if(characterSelected!=null)
        {
            iTween.ScaleTo(characterSelected, new Vector3(1f, 1f, 1f), 0.5f);
        }
        characterSelected=go;

        //判断当前选择的角色是否已经存在，通过名字来判断，当角色已经创建则在名字输入框中显示名字，否则不显示
        foreach (var role in roleList )
        {
            if((role.IsMan &&go.name .IndexOf ("boy")>=0)||((role.IsMan ==false)&&go.name .IndexOf ("girl")>=0))
            {
                //角色已经创建
                characternameInput.text = role.Name;
            }
            else
            {
                characternameInput.text = "";
            }
        }
    }

    
    public void OnButtonChangecharacterClick()
    {
        //角色切换按钮

        //隐藏自身面板
        characterselectTween.PlayReverse();
        StartCoroutine(HidePanel(characterselectTween.gameObject));
        //展示角色的面板
        charactershowTween.gameObject.SetActive(true);
        charactershowTween.PlayForward();
    }

    public void ShowRoleAddPanel()
    {
        //角色创建面板的显示
        charactershowTween.gameObject.SetActive(true);
        charactershowTween.PlayForward();
    }

    public void OncharactershowButtonSure()
    {
        //通过玩家选择角色，当选择的角色玩家不拥有，则向服务器发起请求创建相应 的角色，当选择的角色玩家已经拥有，则直接跳转到显示的界面，显示刚刚选择的角色
        if(characternameInput .value.Length <3)
        {
            MessageManager._instance.ShowMessage("角色的名字不能少于三个字符");
            return;
        }

        bool isHave = false;//用来表示玩家是否拥有刚刚选择的角色
        Role roleSelected=null;//已选择的角色对应的role
        //判断当前的角色是否已经创建
        foreach (Role role in roleList)
        {
            if((role.IsMan &&characterSelected .name .IndexOf ("boy")>=0)||(role.IsMan ==false&&characterSelected .name .IndexOf ("girl")>=0))
            {
                characternameInput.value = role.Name;
                roleSelected =role;
                isHave = true;
            }
        }

        if(isHave )
        {
            //拥有选择的角色
            ShowRole(roleSelected);
        }
        else
        {
            //不拥有选择的角色,则向服务器发起添加角色的请求

            //创建一个角色
            Role roleAdd = new Role();
            roleAdd.IsMan = characterSelected.name.IndexOf("boy") >= 0 ? true : false;
            roleAdd.Name = characternameInput.value;
            roleAdd.Level = 1;
            roleAdd.User = null;
            roleAdd.Coin = 20000;
            roleAdd.Toughen = 50;
            roleAdd.Energy = 100;
            roleAdd.Exp = 0;
            roleAdd.Diamond = 1000;
            roleController.AddRole(roleAdd);//向服务器发起请求，添加角色
        }

        OnCharactershowButtonBackClick();
    }

    public void OnCharactershowButtonBackClick()
    {
        charactershowTween.PlayReverse();
        StartCoroutine(HidePanel(charactershowTween.gameObject));
        characterselectTween.gameObject.SetActive(true);
        characterselectTween.PlayForward();
    }
}
