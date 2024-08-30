using ErrorOr;

namespace BuberDiner.Domain.Errors;

public static partial class Errors{
    public static class InvalidCredentials{
        public static Error InvalidCredential = Error.Validation(code:"Invalid.Credential",description:"Invalid credentials");
        
    }
}