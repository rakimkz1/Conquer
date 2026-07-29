using Monsters;
using Monsters.MonsterState;
using UnityEngine;

[CreateAssetMenu(fileName = "EggIdelState", menuName = "ScriptableObjects/Monster/States/EggIdelState")]
public class EggStateIdel : IdelStateBase
{
    private MonsterIdel _target;
    public override void OnEnter(MonsterIdel target)
    {
        _target = target;
    }

    public override void OnExit()
    {
        Debug.Log("Egg Exit");
    }

    public override void OnWork()
    {
        _target.eggIdelHandler.CheckEgg();
    }
}
