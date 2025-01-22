namespace WebApplication1.Interfaces
{
    public interface IWorkingDaysService
    {
        int CalculateWorkingDays(DateTime start, DateTime end);
    }
}
