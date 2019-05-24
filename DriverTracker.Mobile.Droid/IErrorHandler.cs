using System;
namespace DriverTracker.Mobile.Droid
{
    // from https://johnthiriet.com/removing-async-void/
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
