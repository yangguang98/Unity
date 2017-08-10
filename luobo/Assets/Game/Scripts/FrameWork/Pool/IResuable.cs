using UnityEngine;
using System.Collections;

public interface IResuable {

     void OnSpawn();//当取出时调用
     void OnUnspawn();//当回收时调用
}
