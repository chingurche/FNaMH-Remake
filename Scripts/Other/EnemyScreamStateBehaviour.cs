using UnityEngine;

public class EnemyScreamStateBehaviour : StateMachineBehaviour, IOffable
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FindAnyObjectByType<PlayerDeath>().Death();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        FindAnyObjectByType<PlayerDeath>().ExitMenu();
    }
}
