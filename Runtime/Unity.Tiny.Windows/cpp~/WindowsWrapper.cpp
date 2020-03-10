#if UNITY_WINDOWS && UNITY_DOTSPLAYER_IL2CPP_WAIT_FOR_MANAGED_DEBUGGER

#include <Unity/Runtime.h>

#include <windef.h>
#include <winuser.h>

void DialogUpdateCallback()
{
// #if ENABLE_PLAYERCONNECTION
//     PlayerConnection::Get().Poll();
// #endif
}

void CALLBACK DialogTimerCallback(HWND hwnd, UINT uMsg, UINT timerId, DWORD dwTime)
{
    DialogUpdateCallback();
}

DOTS_EXPORT(void)
ShowDebuggerAttachDialog(const char* message)
{
    UINT_PTR timerId = 0;
    timerId = SetTimer(NULL, 0, USER_TIMER_MINIMUM, (TIMERPROC)&DialogTimerCallback);

    MessageBoxA(0, message, "Debug", MB_OK);

    if (timerId != 0)
    {
        KillTimer(NULL, timerId);
        timerId = 0;
    }
}

#endif
