using FNDSystem.Core.Domain;

public interface IActivityService
{
    IEnumerable<Activity>? GetActivityList(string plantName);
}

