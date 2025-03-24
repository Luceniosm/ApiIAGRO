namespace IAGRO.Domain.Dtos;

public record BookFilterRequest
(
    string? Author,
    string? NameBook,
    string? Genre,
    bool Asc
);
    
