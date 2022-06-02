namespace Kdz.Gateway.Services;

public class SimpleTimeService : ITimeService
{
    #region Implementation of ITimeService

    public string Time => DateTime.Now.ToString("HH:mm:ss");

    #endregion
}