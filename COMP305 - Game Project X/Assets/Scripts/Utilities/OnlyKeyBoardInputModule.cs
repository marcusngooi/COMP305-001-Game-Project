using UnityEngine.EventSystems;

class OnlyKeyBoardInputModule : StandaloneInputModule
{
    //variable to hold current settings
    private bool isMouseInputActive = false;

    //type interface to get actual mouse input status
    public bool GetMouseState
    {
        get { return isMouseInputActive; }
    }

    //mouse switcher interface 
    public void MouseSwitcher()
    {
        isMouseInputActive = isMouseInputActive == false ? true : false;
    }

    //inherited event processing interface (called every frame)
    public override void Process()
    {
        bool usedEvent = SendUpdateEventToSelectedObject();

        if (eventSystem.sendNavigationEvents)
        {
            if (!usedEvent)
                usedEvent |= SendMoveEventToSelectedObject();

            if (!usedEvent)
                SendSubmitEventToSelectedObject();
        }

        if (isMouseInputActive)
            ProcessMouseEvent();
    }
}