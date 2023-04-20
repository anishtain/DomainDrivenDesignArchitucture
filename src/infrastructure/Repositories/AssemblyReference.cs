using System.Reflection;

namespace DomainDrivenDesignArchitucture.Infrastrcturcture.Repositories;

public static class AssemblyReferences
{
    public static Type ASSEMBLY_TYPE => typeof(AssemblyReferences).GetType();

    public static Assembly ASSEMBLY => ASSEMBLY_TYPE.Assembly;
}