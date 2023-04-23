namespace DomainDrivenDesignArchitucture.Domain.Models.commons.permissions;
public static class GeneralPermission
{
    public static IList<string> Permissions => GetAllPermission();

    private static IList<string> GetAllPermission()
    {
        var permissionFiles = AssemblyReferences.ASSEMBLY.GetTypes();

        var permissions =
            permissionFiles
            .SelectMany(x => x.GetProperties(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public))
            .Where(x => x.GetType() == typeof(string))
            .Select(x => (string)x.GetValue(null))
            .ToList();

        return permissions;
    }
}
