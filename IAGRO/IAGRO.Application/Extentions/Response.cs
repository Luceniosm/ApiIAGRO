namespace IAGRO.Application.Extentions;

public record Response<T>(bool Success, T? Data, List<string>? Erros = null);

