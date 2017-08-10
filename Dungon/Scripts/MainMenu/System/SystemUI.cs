using UnityEngine;
using System.Collections;

public class SystemUI : MonoBehaviour {

    private static SystemUI _instance;
    private TweenPosition tweenPos;
    private UIButton audioButton;
    private UILabel audioLabel;
    public bool isAudioOpen = true;

    public static SystemUI Instance
    {
        get
        {
            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
        tweenPos = this.GetComponent<TweenPosition>();
        audioButton = transform.Find("AudioButton").GetComponent<UIButton>();
        audioLabel = transform.Find("AudioLabel").GetComponent<UILabel>();
    }

    public void Show()
    {
        tweenPos.PlayForward();
    }

    public void Hide()
    {
        tweenPos.PlayReverse();
    }

    public void OnAudioButtonClick()
    {
        if(isAudioOpen )
        {
            isAudioOpen = false;
            audioButton.gameObject.GetComponent<UISprite>().spriteName = "pic_音效关闭";
            audioLabel.text = "音效关闭";
        }
        else
        {
            isAudioOpen = true;
            audioButton.gameObject.GetComponent<UISprite>().spriteName = "pic_音效开启";
            audioLabel.text = "音效开启";
        }
    }

    public void OnContactButtonClick()
    {
        Application.OpenURL("www.taikr.com");//打开网址
    }

    public void OnChangeRoleButtonClick()
    {
        Destroy(PhotonEngine.Instance.gameObject);
        AsyncOperation operation=Application.LoadLevelAsync(0);
        LoadSceneProgressBar._instance.Show(operation);
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }

    public void OnCloseButtonClick()
    {
        Hide();
    }
	
}
