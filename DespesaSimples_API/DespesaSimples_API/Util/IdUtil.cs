namespace DespesaSimples_API.Util;

public static class IdUtil
{
    public static int? ParseIdToInt(string? id, char sufix)
    {
        if (string.IsNullOrEmpty(id) || id.Equals("0"))
            return null;
        
        return int.Parse(
            id.EndsWith(sufix) ? 
                id[..^1] : 
                id
        );
    }
}