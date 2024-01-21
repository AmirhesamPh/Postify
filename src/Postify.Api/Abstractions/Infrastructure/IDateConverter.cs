namespace Postify.Abstractions.Infrastructure;

public interface IDateConverter
{
    string ToPersianDateTime(DateTime dateTime);
}
