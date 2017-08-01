using UnityEngine;
using System.Collections;

public class DialogDataFormat : MonoBehaviour {

    private int dialogSecNum;
    private string dialogSecName;
    private int sectionIndex;
    private string dialogSide;
    private string dialogPerson;
    private string dialogContent;

    public string DialogContent
    {
        get { return dialogContent; }
        set { dialogContent = value; }
    }

    public string DialogPerson
    {
        get { return dialogPerson; }
        set { dialogPerson = value; }
    }

    public string DialogSide
    {
        get { return dialogSide; }
        set { dialogSide = value; }
    }


    public int SectionIndex
    {
        get { return sectionIndex; }
        set { sectionIndex = value; }
    }

    public string DialogSecName
    {
        get { return dialogSecName; }
        set { dialogSecName = value; }
    }

    public int DialogSecNum
    {
        get { return dialogSecNum; }
        set { dialogSecNum = value; }
    }
}
