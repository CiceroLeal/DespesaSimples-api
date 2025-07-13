using AutoFixture;
using DespesaSimples_API.Abstractions.Repositories;
using DespesaSimples_API.Dtos.Tag;
using DespesaSimples_API.Entities;
using DespesaSimples_API.Exceptions;
using DespesaSimples_API.Services;
using Moq;

namespace DespesaSimples_API.Tests.Services;

public class TagServiceTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public async Task ObterTodasTagsAsync_DeveRetornarTags()
    {
        var repo = new Mock<ITagRepository>();
        var tags = new List<Tag>
        {
            new() { IdTag = 1, Nome = "Tag1" },
            new() { IdTag = 2, Nome = "Tag2" },
            new() { IdTag = 3, Nome = "Tag3" }
        };
        repo.Setup(r => r.ObterTodasTagsAsync()).ReturnsAsync(tags);

        var service = new TagService(repo.Object);

        var result = await service.ObterTodasTagsAsync();

        Assert.NotNull(result);
        Assert.Equal(3, result.Tags.Count());
        Assert.All(result.Tags, t => Assert.IsType<TagDto>(t));
    }

    [Fact]
    public async Task ObterTagPorIdAsync_DeveRetornarTag_QuandoExiste()
    {
        var repo = new Mock<ITagRepository>();
        var tag = new Tag { IdTag = 1, Nome = "Teste" };
        repo.Setup(r => r.ObterTagPorIdAsync(tag.IdTag)).ReturnsAsync(tag);

        var service = new TagService(repo.Object);

        var result = await service.ObterTagPorIdAsync(tag.IdTag);

        Assert.NotNull(result);
        Assert.Single(result.Tags);
        Assert.Equal(tag.IdTag, result.Tags.First().IdTag);
    }

    [Fact]
    public async Task RemoverTagPorIdAsync_DeveRetornarTrue_QuandoRemovido()
    {
        var repo = new Mock<ITagRepository>();
        repo.Setup(r => r.RemoverTagAsync(It.IsAny<int>())).ReturnsAsync(true);

        var service = new TagService(repo.Object);

        var result = await service.RemoverTagPorIdAsync(1);

        Assert.True(result);
    }

    [Fact]
    public async Task RemoverTagPorIdAsync_DeveRetornarFalse_QuandoNaoRemovido()
    {
        var repo = new Mock<ITagRepository>();
        repo.Setup(r => r.RemoverTagAsync(It.IsAny<int>())).ReturnsAsync(false);

        var service = new TagService(repo.Object);

        var result = await service.RemoverTagPorIdAsync(1);

        Assert.False(result);
    }

    [Fact]
    public async Task CriarTagAsync_DeveRetornarTrue_QuandoCriado()
    {
        var repo = new Mock<ITagRepository>();
        repo.Setup(r => r.CriarTagAsync(It.IsAny<Tag>())).ReturnsAsync(true);

        var service = new TagService(repo.Object);
        var tagDto = _fixture.Create<TagDto>();

        var result = await service.CriarTagAsync(tagDto);

        Assert.True(result);
    }

    [Fact]
    public async Task CriarTagAsync_DeveRetornarFalse_QuandoFalha()
    {
        var repo = new Mock<ITagRepository>();
        repo.Setup(r => r.CriarTagAsync(It.IsAny<Tag>())).ReturnsAsync(false);

        var service = new TagService(repo.Object);
        var tagDto = _fixture.Create<TagDto>();

        var result = await service.CriarTagAsync(tagDto);

        Assert.False(result);
    }

    [Fact]
    public async Task AtualizarTagAsync_DeveRetornarTrue_QuandoAtualizado()
    {
        var repo = new Mock<ITagRepository>();
        var tag = new Tag { IdTag = 1, Nome = "Original" };
        repo.Setup(r => r.ObterTagPorIdAsync(tag.IdTag)).ReturnsAsync(tag);
        repo.Setup(r => r.AtualizarTagAsync(It.IsAny<Tag>())).ReturnsAsync(true);

        var service = new TagService(repo.Object);
        var tagDto = _fixture.Create<TagDto>();

        var result = await service.AtualizarTagAsync(tag.IdTag, tagDto);

        Assert.True(result);
    }

    [Fact]
    public async Task AtualizarTagAsync_DeveLancarNotFoundException_QuandoTagNaoExiste()
    {
        var repo = new Mock<ITagRepository>();
        repo.Setup(r => r.ObterTagPorIdAsync(It.IsAny<int>())).ReturnsAsync((Tag?)null);

        var service = new TagService(repo.Object);
        var tagDto = _fixture.Create<TagDto>();

        await Assert.ThrowsAsync<NotFoundException>(() => service.AtualizarTagAsync(1, tagDto));
    }
}