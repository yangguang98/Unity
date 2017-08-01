using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TestDialogInfo : MonoBehaviour {

    public Text personSide;
    public Text personName;
    public Text personcontent;


    public void DisplayNextDialogInfo()
    {
        DialogSide side = DialogSide.None;
        string dialogPersonName;
        string dialogPersonContent;
        bool res=DialogDataMgr.GetInstance().GetNextDialogInfoRecoder(1,out side,out dialogPersonName ,out dialogPersonContent);
        if(res)
        {
            switch (side)
            {
                case DialogSide.Hero:
                    personSide.text = "Hero";
                    break;
                case DialogSide.None:
                    break;
                case DialogSide.NPCSide:
                    personSide.text = "NPC";
                    break;
                default:
                    break;
            }

            personName.text = dialogPersonName;
            personcontent.text = dialogPersonContent;
        }
        else
        {

        }
    }
}
