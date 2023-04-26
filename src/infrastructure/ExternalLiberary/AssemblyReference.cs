using System.Reflection;

namespace DomainDrivenDesignArchitucture.Infrastructure.ExternalLiberary;

public static class AssemblyReference
{
    public static Type ASSEMBLY_TYPE => typeof(AssemblyReference).GetType();

    public static Assembly ASSEMBLY => ASSEMBLY_TYPE.Assembly;
}
