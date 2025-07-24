using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Entities;
using MediatR;

namespace DespesaSimples_API.Commands;

public record BuscarAtualizarTagsCommand(List<string> NomesTags) : IRequest<List<Tag>>;

public class BuscarAtualizarTagsCommandHandler(ITagService tagService)
    : IRequestHandler<BuscarAtualizarTagsCommand, List<Tag>>
{
    public async Task<List<Tag>> Handle(BuscarAtualizarTagsCommand command, CancellationToken ct)
    {
        return await tagService.BuscarAtualizarTagsAsync(command.NomesTags);
    }
}