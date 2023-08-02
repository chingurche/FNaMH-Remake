using UnityEngine;

public class WindowDoorGetter : MonoBehaviour, IRayGetter
{
    void IRayGetter.SendToReceiver()
    {
        if (FindObjectOfType<Telegram>().isTelegramWorking
        && !GetComponent<Rotator>().isOpening) { return; }
        
        GetComponent<Rotator>().isClosing = true;
    }
}
