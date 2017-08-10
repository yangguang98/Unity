using UnityEngine;
using System.Collections;

public class ServerProperty1 : MonoBehaviour {

    private string _name;
    public string ip = "192.0.0.1";
    public string name
    {
        set
        {
            transform.Find("Label").GetComponent<UILabel>().text = value;//在set当中就更新了显示了
            _name = value;
        }
        get
        {
            return _name;
        }
    }

    public int count = 100;

    public void OnPress(bool isPress)
    {
       
        if(isPress==false)
        {
            //选择了当前的服务器
            transform.root.SendMessage("OnServerselect", this.gameObject);
        }
        

    }

    

}
