using System.Collections.Generic;
using TaidouCommon.Model;
using UnityEngine;

public delegate void OnGetRoleEvent(List<Role> roleList);
public delegate void OnAddRoleEvent(Role role);
public delegate void OnSelectRoleEvent();
public delegate void OnGetTaskDBListEvent(List<TaskDB> list);
public delegate void OnAddTaskDBEvent(TaskDB taskDB);
public delegate void OnUpdateTaskDBEvent();
public delegate void OnSyncTaskCompleteEvent();
public delegate void OnGetInventoryItemDBListEvent(List<InventoryItemDB> inventoryItemDBList);
public delegate void OnAddInventoryItemDBEvent(InventoryItemDB itemDB);
public delegate void OnUpdateInventoryItemDBEvent();
public delegate void OnUpdateInventoryItemDBListEvent();
public delegate void OnUpgradeEquipEvent();
public delegate void OnGetSkillDBListEvent(List<SkillDB> list);
public delegate void OnAddSkillDBEvent(SkillDB skillDB);
public delegate void OnUpdateSkillDBEvent();
public delegate void OnUpgradeSkillDBEvent(SkillDB skillDB);
public delegate void OnSyncSkillCompleteEvent();
public delegate void OnPlayerHpChangeEvent(float hp);
public delegate void OnGetTeamEvent(List<Role> roleList,int roleMasterId);//组队成功
public delegate void OnWaitingTeamEvent();//等待组队
public delegate void OnCancelTeamEvent();//取消组队
public delegate void OnSyncPositionAndRotationEvent(int roleID,Vector3 position,Vector3 eulerAngle);
public delegate void OnSyncMoveAnimationEvent(int roleID,PlayerMoveAnimationModel model);//动画同步回调，第一个参数为同步的模型，第二个数据
public delegate void OnCreateEnemyEvent(CreateEnemyModel model);

public delegate void OnSyncEnemyPositionRotationEvent(EnemyPositionModel model);

public delegate void OnSyncEnemyAnimationEvent(EnemyAnimationModel model);

public delegate void OnSyncPlayerAnimationEvent(PlayerAnimationModel model,int roleId);//同步那个角色

public delegate void OnGameStateChangeEvent(GameStateModel model);//游戏状态

public delegate void OnSyncBossAnimationEvent(BossAnimationModel model);