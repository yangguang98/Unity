using UnityEngine;
using System.Collections;

public class GameOverPanel : MonoBehaviour {

    private static GameOverPanel _instance;
    private TweenScale tweenScale;
    private UILabel label;
    public static GameOverPanel Instance
    {
        get
        {
            return _instance;
        }
    }

    void Start()
    {
        _instance = this;
        tweenScale = this.GetComponent<TweenScale>();
        label = transform.Find("Label").GetComponent<UILabel>();
    }

    public void Show(string str)
    {
        tweenScale.PlayForward();
        label.text = str;
    }

    public void Hide()
    {
        tweenScale.PlayReverse();
    }

    public void OnBackButtonClick()
    {
        Hide();
        Destroy(GameController.Instance.gameObject);//回到第二个场景，由于第二个场景中有GameController脚本，因此要将第三个场景中的GameController的游戏物体消除；??????????????????????????????
        AsyncOperation operation=Application.LoadLevelAsync(1);
        LoadSceneProgressBar._instance.Show(operation);
    }
}
