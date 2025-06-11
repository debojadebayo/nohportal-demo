using System.Reflection;

namespace ComposedHealthBase.Server.Enumerations;

public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>>
    where TEnum : Enumeration<TEnum>
{
    private static readonly Lazy<Dictionary<long, TEnum>> EnumerationsDictionary =
        new(() => CreateEnumerationDictionary(typeof(TEnum)));

    protected Enumeration(long id, string name)
    {
        Id = id;
        Name = name;
    }
    protected Enumeration() => Name = string.Empty;
    public long Id { get; protected init; }
    public string Name { get; protected init; }

    public static bool operator ==(Enumeration<TEnum>? a, Enumeration<TEnum>? b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Enumeration<TEnum> a, Enumeration<TEnum> b) => !(a == b);

    public static IReadOnlyCollection<TEnum> GetValues() => EnumerationsDictionary.Value.Values.ToList();
    public static TEnum? FromId(long id) => EnumerationsDictionary.Value.TryGetValue(id, out TEnum? enumeration) ? enumeration : null;
    public static TEnum? FromName(string name) => EnumerationsDictionary.Value.Values.SingleOrDefault(x => x.Name == name);

    public static bool Contains(long id) => EnumerationsDictionary.Value.ContainsKey(id);

    public virtual bool Equals(Enumeration<TEnum>? other)
    {
        if (other is null)
        {
            return false;
        }

        return GetType() == other.GetType() && other.Id.Equals(Id);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (GetType() != obj.GetType())
        {
            return false;
        }

        return obj is Enumeration<TEnum> otherValue && otherValue.Id.Equals(Id);
    }
    public override int GetHashCode() => Id.GetHashCode();

    private static Dictionary<long, TEnum> CreateEnumerationDictionary(Type enumType) => GetFieldsForType(enumType).ToDictionary(t => t.Id);

    private static IEnumerable<TEnum> GetFieldsForType(Type enumType) =>
    enumType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
        .Where(fieldInfo => enumType.IsAssignableFrom(fieldInfo.FieldType))
        .Select(fieldInfo => (TEnum)fieldInfo.GetValue(default)!);
}