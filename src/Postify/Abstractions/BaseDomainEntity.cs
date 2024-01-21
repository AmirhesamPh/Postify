namespace Postify.Abstractions;

public abstract class BaseDomainEntity<TKey>
    where TKey : struct
{
    public TKey Id { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime LastModifiedDate { get; set; }

    public string? PersianCreatedDate { get; set; }

    public string? PersianLastModifiedDate { get; set; }
}
