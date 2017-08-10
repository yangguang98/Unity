using UnityEngine;
using System.Collections;
using System.Collections .Generic ;

public class NPCManager : MonoBehaviour {

    public GameObject[] npcArray;
    public static NPCManager _instance;
    private Dictionary<int, GameObject> npcDict = new Dictionary<int, GameObject>();

    public GameObject transcriptGo;
    void Awake()
    {
        _instance = this;

        Init();
    }
    void Init()
    {
        foreach(GameObject go in npcArray)
        {
            int id = int.Parse(go.name.Substring(0, 4));//获得前四个字符
            npcDict.Add(id, go);
        }
    }

    public GameObject GetNpcById(int id)
    {
        GameObject go = null;
        npcDict.TryGetValue(id, out go);//通过一个id得到一个物体

        return go;
    }
}
