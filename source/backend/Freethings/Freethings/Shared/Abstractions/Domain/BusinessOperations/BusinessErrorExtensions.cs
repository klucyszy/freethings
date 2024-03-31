using System.Text.RegularExpressions;

namespace Freethings.Shared.Abstractions.Domain.BusinessOperations;

public static class BusinessErrorExtensions
{
    private static readonly Regex _regex = new(@"{\w+}");
    
    public static BusinessError Format(this BusinessError definition, params object[] args)
    {
        MatchCollection matches = _regex.Matches(definition.Message);

        if (matches.Count != args.Length)
        {
            throw new ArgumentException("Definition message contains different number of parameters than passed");
        }
        
        if (!args.Any())
        {
            return definition;
        }
        
        string formattedMessage = string.Format(definition.Message, args);

        return BusinessError.Create(formattedMessage, definition.Module);
    }

    public static BusinessException AsBusinessException(this BusinessError definition)
    {
        return new BusinessException(definition.Message);
    }

    public static BusinessResult AsBusinessResult(this BusinessError definition)
    {
        return BusinessResult.Failure(definition.Message);
    }
}