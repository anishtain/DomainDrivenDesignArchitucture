namespace DomainDrivenDesignArchitucture.Domain.Models.commons.primitives;
public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetAtomicValues();


    public override bool Equals(object? obj) => 
        obj is ValueObject valueObject && ValuesAreEquals(valueObject);

    public override int GetHashCode() =>
        GetAtomicValues().Aggregate(default(int), HashCode.Combine);

    public bool Equals(ValueObject? other) =>
        other is ValueObject && ValuesAreEquals(other);

    private bool ValuesAreEquals(ValueObject other)=>
        GetAtomicValues().SequenceEqual(other.GetAtomicValues());
}
