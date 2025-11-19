namespace UniversiteDomain.Exceptions.UeExceptions;

public class IntituleUeTooShortException : Exception
{
    public IntituleUeTooShortException() : base() { }
    public IntituleUeTooShortException(string message) : base(message) { }
    public IntituleUeTooShortException(string message, Exception inner) : base(message, inner) { }
}