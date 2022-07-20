namespace Otakulore.Services;

public class ResourceService
{

    public string Credits { get; private init; }

    public static ResourceService Initialize()
    {
        var task = Task.Run(async () =>
        {
            return new ResourceService
            {
                Credits = await GetStringResource("Credits.txt")
            };
        });
        task.Wait();
        return task.Result;
    }

    private static async Task<string> GetStringResource(string name)
    {
        await using var stream = await FileSystem.OpenAppPackageFileAsync(name);
        using var reader = new StreamReader(stream);
        return await reader.ReadToEndAsync();
    }

}