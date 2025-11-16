using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorStateView : View
{
    [SerializeField] private DoorUnit doorUnit_1;
    [SerializeField] private DoorUnit doorUnit_2;
    [SerializeField] private DoorUnit doorUnit_3;

    [SerializeField] private float timeWaitActivateDeactivateAll;
    [SerializeField] private float timeWait;

    private IEnumerator timerActivator;

    public void ActivateAll()
    {
        if (timerActivator != null) Coroutines.Stop(timerActivator);

        timerActivator = ActivateAll_Coro();
        Coroutines.Start(timerActivator);
    }

    public void DeactivateAll()
    {
        if (timerActivator != null) Coroutines.Stop(timerActivator);

        timerActivator = DeactivateAll_Coro();
        Coroutines.Start(timerActivator);
    }

    public void Open(int door)
    {
        if (timerActivator != null) Coroutines.Stop(timerActivator);

        timerActivator = Open_Coro(door);
        Coroutines.Start(timerActivator);
    }

    private IEnumerator ActivateAll_Coro()
    {
        doorUnit_1.Activate();

        yield return new WaitForSeconds(timeWaitActivateDeactivateAll);

        doorUnit_2.Activate();

        yield return new WaitForSeconds(timeWaitActivateDeactivateAll);

        doorUnit_3.Activate();
    }

    private IEnumerator DeactivateAll_Coro()
    {
        doorUnit_1.Deactivate();

        yield return new WaitForSeconds(timeWaitActivateDeactivateAll);

        doorUnit_2.Deactivate();

        yield return new WaitForSeconds(timeWaitActivateDeactivateAll);

        doorUnit_3.Deactivate();
    }

    private IEnumerator Open_Coro(int door)
    {
        if (door == 0)
        {
            doorUnit_2.Deactivate();

            yield return new WaitForSeconds(timeWaitActivateDeactivateAll);

            doorUnit_3.Deactivate();

            yield return new WaitForSeconds(timeWait);

            doorUnit_1.Open();
        }
        else if (door == 1)
        {
            doorUnit_1.Deactivate();

            yield return new WaitForSeconds(timeWaitActivateDeactivateAll);

            doorUnit_3.Deactivate();

            yield return new WaitForSeconds(timeWait);

            doorUnit_2.Open();
        }
        else
        {
            doorUnit_2.Deactivate();

            yield return new WaitForSeconds(timeWaitActivateDeactivateAll);

            doorUnit_1.Deactivate();

            yield return new WaitForSeconds(timeWait);

            doorUnit_3.Open();
        }
    }
}
