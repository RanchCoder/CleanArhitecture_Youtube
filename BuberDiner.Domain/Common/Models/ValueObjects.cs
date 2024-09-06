namespace BuberDiner.Domain.Common.Models;

public abstract class ValueObject : IEquatable<ValueObject>{
    public abstract IEnumerable<object> GetEqualityComponent();

    public override bool Equals(object? obj)
    {
       if(obj is null || obj.GetType() != GetType()){
        return false;
       }   
       var valueObject  = (ValueObject)obj;
       return GetEqualityComponent().SequenceEqual(valueObject.GetEqualityComponent());
    }

    public static bool operator ==(ValueObject left,ValueObject right){
        return Equals(left, right);
    }
    public static bool operator !=(ValueObject left,ValueObject right){
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return GetEqualityComponent().Select(x=>x?.GetHashCode() ?? 0)
        .Aggregate((x,y)=>x^y);
    }

    public bool Equals(ValueObject? other)
     {
        return Equals((object?)other);
     }
}
public class Price : ValueObject{
    public decimal Amount { get; set;}
    public string Currency{get;set;}

    public Price(decimal amount, string currency){
        this.Amount = amount;
        this.Currency = currency;
    }

    public override IEnumerable<object> GetEqualityComponent()
    {
        yield return Amount;
        yield return Currency;
    }
}