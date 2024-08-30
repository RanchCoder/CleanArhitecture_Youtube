using ErrorOr;

namespace BuberDiner.Domain.Errors;

public static partial class Errors{
    public static class User{
        public static Error DuplicateEmail = Error.Conflict(code:"Duplicate.Email",description:"Invalid Email");
        
    }
}