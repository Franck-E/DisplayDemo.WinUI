using Microsoft.Windows.System.Power;
using System.Threading.Tasks;

namespace DisplayExtensions;

public class Checks
{
    //private Task CheckDeviceName(Screen screen)
    //{
    //    string tmp = GetDeviceName(screen);
    //    if (Details.DeviceName != tmp)
    //    {
    //        logger.LogInformation(string.Format("{0} name changed to {1}", Details.Name, tmp));
    //        Details.DeviceName = tmp;
    //        hasChanged = true;
    //    }

    //    return Task.CompletedTask;
    //}

    //private Task CheckPrimary(Screen screen)
    //{
    //    bool tmp = GetPrimary(screen);
    //    if (Details.Primary != tmp)
    //    {
    //        string txt = tmp ? string.Format("{0} set as primary display", Details.Name) : string.Format("{0} is no longer set as primary display", Details.SafeName);
    //        logger.LogInformation(txt);
    //        Details.Primary = tmp;
    //        hasChanged = true;
    //    }
    //    if (!Details.Primary && Details.Status == (int)DisplayStatus.Primary) Details.Status = (int)DisplayStatus.Online;

    //    return Task.CompletedTask;
    //}

    //private Task CheckIsAttachedToDesktop(Screen screen)
    //{
    //    bool tmp = IsAttachedToDesktop(screen);
    //    if (Details.Attached != tmp)
    //        if (Details.Attached != tmp)
    //        {
    //            string txt = tmp ? string.Format("{0} is connected to environment", Details.Name) : string.Format("{0} is no longer connected to environment", Details.Name);
    //            logger.LogInformation(txt);
    //            Details.Attached = tmp;
    //            hasChanged = true;
    //        }

    //    return Task.CompletedTask;
    //}

    //private Task CheckResolution(Screen screen)
    //{
    //    string tmp = GetResolution(screen);
    //    if (Details.Resolution != tmp)
    //    {
    //        logger.LogInformation(string.Format("{0} changed resolution from {1} to {2}", Details.Name, Details.Resolution, tmp));
    //        Details.Resolution = tmp;
    //        hasChanged = true;
    //    }

    //    return Task.CompletedTask;
    //}

    //private Task CheckOrientation(Screen screen)
    //{
    //    string tmp = GetOrientation(screen);
    //    if (Details.Orientation != tmp)
    //    {
    //        logger.LogInformation(string.Format("{0} changed orientation from {1} to {2}", Details.Name, Details.Orientation, tmp));
    //        Details.Orientation = tmp;
    //        hasChanged = true;
    //    }

    //    return Task.CompletedTask;
    //}

    //private Task CheckFrequency(Screen screen)
    //{
    //    string tmp = GetFrequency(screen);
    //    if (Details.Frequency != tmp)
    //    {
    //        logger.LogInformation(string.Format("{0} changed frequency from {1} to {2}", Details.Name, Details.Frequency, tmp));
    //        Details.Frequency = tmp;
    //        hasChanged = true;
    //    }

    //    return Task.CompletedTask;
    //}

    //private Task CheckUID(Screen screen)
    //{
    //    string tmp = GetUID(screen);
    //    if (Details.UID != tmp)
    //    {
    //        logger.LogInformation(string.Format("{0} changed UID from {1} to {2}", Details.Name, Details.UID, tmp));
    //        Details.UID = tmp;
    //        hasChanged = true;
    //    }

    //    return Task.CompletedTask;
    //}

    //private Task CheckPosition(Screen screen)
    //{
    //    System.Drawing.Rectangle tmp = GetPosition(screen);
    //    if (Position != tmp)
    //    {
    //        logger.LogInformation(string.Format("{0} position changed", Details.Name));
    //        Position = tmp;
    //        hasChanged = true;
    //    }

    //    return Task.CompletedTask;
    //}
}
