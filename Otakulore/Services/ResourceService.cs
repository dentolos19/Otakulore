namespace Otakulore.Services;

public class ResourceService
{

    public string Credits { get; private init; }

    public static ResourceService Initialize()
    {
        var task = Task.Run(async () =>
        {
            await using var creditsStream = await FileSystem.OpenAppPackageFileAsync("Credits.txt");
            using var reader = new StreamReader(creditsStream);
            return new ResourceService
            {
                Credits = await reader.ReadToEndAsync()
            };
        });
        task.Wait();
        return task.Result;

    }

}