using UnityEngine;
using System.Collections;
using TaidouCommon;
using System.Collections.Generic;
using TaidouCommon.Tools;
using TaidouCommon.Model;


public class InventoryItemDBController : ControllerBase {


    public override TaidouCommon.OperationCode OpCode
    {
        get { return OperationCode.InventoryItemDB ; }
    }

    public void GetInventoryItemDB()
    {
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.GetInventoryItemDB, new Dictionary<byte, object>());
    }

    public void AddInventoryItemDB(InventoryItemDB itemDB)
    {
        itemDB.Role = null;
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        ParameterTool.AddParameter(parameters, ParameterCode.InventoryItemDB, itemDB);
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.AddInventoryItemDB, parameters);
    }

    public void UpdateInventoryItemDB(InventoryItemDB itemDB)
    {
        itemDB.Role = null;
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        ParameterTool.AddParameter(parameters, ParameterCode.InventoryItemDB, itemDB);
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.UpdateInventoryItemDB, parameters);
    }

    public void UpdateInventoryItemDBList(InventoryItemDB itemDBDressOn,InventoryItemDB itemDBDressOff)
    {
        itemDBDressOff.Role = null;
        itemDBDressOn.Role = null;
        List<InventoryItemDB> list = new List<InventoryItemDB>();
        list.Add(itemDBDressOff);
        list.Add(itemDBDressOn);
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        ParameterTool.AddParameter<List<InventoryItemDB>>(parameters, ParameterCode.InventoryItemDBList, list);
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.UpdateInventoryItemDBList, parameters);
    }

    public void UpgradeEquip(InventoryItemDB it)
    {
        //更新装备和角色
        it.Role = null;
        Role role = PhotonEngine.Instance.role;
        role.User = null;
        Dictionary<byte, object> parameters = new Dictionary<byte, object>();
        ParameterTool.AddParameter<Role>(parameters, ParameterCode.Role, role);
        ParameterTool.AddParameter<InventoryItemDB>(parameters, ParameterCode.InventoryItemDB, it);
        PhotonEngine.Instance.SendRequest(OpCode, SubCode.UpgradeEquip, parameters);
    }

    public override void OnOperationResponse(ExitGames.Client.Photon.OperationResponse reponse)
    {
        SubCode subCode=ParameterTool.GetParameter<SubCode >(reponse .Parameters ,ParameterCode.SubCode,false);
        switch (subCode )
        {
            case SubCode .GetInventoryItemDB :
                List<InventoryItemDB> list = ParameterTool.GetParameter<List<InventoryItemDB>>(reponse.Parameters, ParameterCode.InventoryItemDBList);
                if(OnGetInventoryItemDBList!=null)
                {
                    OnGetInventoryItemDBList(list);
                }
                break;
            case SubCode .AddInventoryItemDB :

                //先将数据添加到服务器端，然后将此数据返回，利用返回的数据更新UI.防止没网等情况
                InventoryItemDB itemDB = ParameterTool.GetParameter<InventoryItemDB>(reponse.Parameters, ParameterCode.InventoryItemDB);
                Debug.Log(itemDB.ID);
                if(OnAddInventoryItemDB !=null)
                {
                    OnAddInventoryItemDB(itemDB);
                }
                break;
            case SubCode.UpdateInventoryItemDB:
                if(OnUpdateInventoryItemDB !=null)
                {
                    OnUpdateInventoryItemDB();
                }
                break;
            case SubCode .UpdateInventoryItemDBList :
                if(OnUpdateInventoryItemDBList!=null)
                {
                    OnUpdateInventoryItemDBList();
                }
                break;
            case SubCode .UpgradeEquip :
                if(OnUpgradeEquip !=null)
                {
                    OnUpgradeEquip();
                }
                break;
        }
    }


    public event OnGetInventoryItemDBListEvent OnGetInventoryItemDBList;
    public event OnAddInventoryItemDBEvent OnAddInventoryItemDB;
    public event OnUpdateInventoryItemDBEvent OnUpdateInventoryItemDB;
    public event OnUpdateInventoryItemDBListEvent OnUpdateInventoryItemDBList;
    public event OnUpgradeEquipEvent OnUpgradeEquip;
}
