using AutoFixture;
using DespesaSimples_API.Abstractions.Services;
using DespesaSimples_API.Controllers;
using DespesaSimples_API.Dtos;
using DespesaSimples_API.Dtos.Auth;
using DespesaSimples_API.Dtos.Tag;
using DespesaSimples_API.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Moq;

namespace DespesaSimples_API.Tests.Controllers;

public class TagControllerTests
{
    private readonly Fixture _fixture = new();

    [Fact]
    public async Task GetTagsAsync_DeveRetornarSuccess_QuandoOk()
    {
        var service = new Mock<ITagService>();
        var logger = new Mock<ILogger<TagDto>>();
        var response = _fixture.Create<TagResponseDto>();
        service.Setup(s => s.ObterTodasTagsAsync()).ReturnsAsync(response);

        var result = await TagController.GetTagsAsync(logger.Object, service.Object);

        Assert.IsType<Ok<ApiResponse<TagResponseDto, List<ApiError>>>>(result);
    }

    [Fact]
    public async Task GetTagsAsync_DeveRetornarBadRequest_QuandoException()
    {
        var service = new Mock<ITagService>();
        var logger = new Mock<ILogger<TagDto>>();
        service.Setup(s => s.ObterTodasTagsAsync()).ThrowsAsync(new Exception());

        var result = await TagController.GetTagsAsync(logger.Object, service.Object);

        Assert.IsType<JsonHttpResult<ApiResponse<object, List<ApiError>>>>(result);
    }

    [Fact]
    public async Task GetTagPorIdAsync_DeveRetornarSuccess_QuandoOk()
    {
        var service = new Mock<ITagService>();
        var logger = new Mock<ILogger<TagDto>>();
        var response = _fixture.Create<TagResponseDto>();
        service.Setup(s => s.ObterTagPorIdAsync(It.IsAny<int>())).ReturnsAsync(response);

        var result = await TagController.GetTagPorIdAsync(logger.Object, service.Object, 1);

        Assert.IsType<Ok<ApiResponse<TagResponseDto, List<ApiError>>>>(result);
    }

    [Fact]
    public async Task GetTagPorIdAsync_DeveRetornarNotFound_QuandoNotFoundException()
    {
        var service = new Mock<ITagService>();
        var logger = new Mock<ILogger<TagDto>>();
        service.Setup(s => s.ObterTagPorIdAsync(It.IsAny<int>())).ThrowsAsync(new NotFoundException());

        var result = await TagController.GetTagPorIdAsync(logger.Object, service.Object, 1);

        Assert.IsType<JsonHttpResult<ApiResponse<object, List<ApiError>>>>(result);
    }

    [Fact]
    public async Task GetTagPorIdAsync_DeveRetornarBadRequest_QuandoException()
    {
        var service = new Mock<ITagService>();
        var logger = new Mock<ILogger<TagDto>>();
        service.Setup(s => s.ObterTagPorIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception());

        var result = await TagController.GetTagPorIdAsync(logger.Object, service.Object, 1);

        Assert.IsType<JsonHttpResult<ApiResponse<object, List<ApiError>>>>(result);
    }

    [Fact]
    public async Task DeleteTagPorIdAsync_DeveRetornarSuccess_QuandoRemovido()
    {
        var service = new Mock<ITagService>();
        var logger = new Mock<ILogger<TagDto>>();
        service.Setup(s => s.RemoverTagPorIdAsync(It.IsAny<int>())).ReturnsAsync(true);

        var result = await TagController.DeleteTagPorIdAsync(logger.Object, service.Object, 1);

        Assert.IsType<Ok<ApiResponse<object, List<ApiError>>>>(result);
    }

    [Fact]
    public async Task DeleteTagPorIdAsync_DeveRetornarNotFound_QuandoNaoRemovido()
    {
        var service = new Mock<ITagService>();
        var logger = new Mock<ILogger<TagDto>>();
        service.Setup(s => s.RemoverTagPorIdAsync(It.IsAny<int>())).ReturnsAsync(false);

        var result = await TagController.DeleteTagPorIdAsync(logger.Object, service.Object, 1);

        Assert.IsType<JsonHttpResult<ApiResponse<object, List<ApiError>>>>(result);
    }

    [Fact]
    public async Task DeleteTagPorIdAsync_DeveRetornarBadRequest_QuandoException()
    {
        var service = new Mock<ITagService>();
        var logger = new Mock<ILogger<TagDto>>();
        service.Setup(s => s.RemoverTagPorIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception());

        var result = await TagController.DeleteTagPorIdAsync(logger.Object, service.Object, 1);

        Assert.IsType<JsonHttpResult<ApiResponse<object, List<ApiError>>>>(result);
    }

    [Fact]
    public async Task CriarTagAsync_DeveRetornarSuccess_QuandoCriado()
    {
        var service = new Mock<ITagService>();
        var logger = new Mock<ILogger<TagDto>>();
        var tagDto = _fixture.Create<TagDto>();
        service.Setup(s => s.CriarTagAsync(tagDto)).ReturnsAsync(true);

        var result = await TagController.CriarTagAsync(logger.Object, service.Object, tagDto);

        Assert.IsType<Ok<ApiResponse<object, List<ApiError>>>>(result);
    }

    [Fact]
    public async Task CriarTagAsync_DeveRetornarBadRequest_QuandoNaoCriado()
    {
        var service = new Mock<ITagService>();
        var logger = new Mock<ILogger<TagDto>>();
        var tagDto = _fixture.Create<TagDto>();
        service.Setup(s => s.CriarTagAsync(tagDto)).ReturnsAsync(false);

        var result = await TagController.CriarTagAsync(logger.Object, service.Object, tagDto);

        Assert.IsType<JsonHttpResult<ApiResponse<object, List<ApiError>>>>(result);
    }

    [Fact]
    public async Task CriarTagAsync_DeveRetornarBadRequest_QuandoException()
    {
        var service = new Mock<ITagService>();
        var logger = new Mock<ILogger<TagDto>>();
        var tagDto = _fixture.Create<TagDto>();
        service.Setup(s => s.CriarTagAsync(tagDto)).ThrowsAsync(new Exception());

        var result = await TagController.CriarTagAsync(logger.Object, service.Object, tagDto);

        Assert.IsType<JsonHttpResult<ApiResponse<object, List<ApiError>>>>(result);
    }

    [Fact]
    public async Task AtualizarTagAsync_DeveRetornarSuccess_QuandoAtualizado()
    {
        var service = new Mock<ITagService>();
        var logger = new Mock<ILogger<TagDto>>();
        var tagDto = _fixture.Create<TagDto>();
        service.Setup(s => s.AtualizarTagAsync(It.IsAny<int>(), tagDto)).ReturnsAsync(true);

        var result = await TagController.AtualizarTagAsync(logger.Object, service.Object, 1, tagDto);

        Assert.IsType<Ok<ApiResponse<object, List<ApiError>>>>(result);
    }

    [Fact]
    public async Task AtualizarTagAsync_DeveRetornarBadRequest_QuandoNaoAtualizado()
    {
        var service = new Mock<ITagService>();
        var logger = new Mock<ILogger<TagDto>>();
        var tagDto = _fixture.Create<TagDto>();
        service.Setup(s => s.AtualizarTagAsync(It.IsAny<int>(), tagDto)).ReturnsAsync(false);

        var result = await TagController.AtualizarTagAsync(logger.Object, service.Object, 1, tagDto);

        Assert.IsType<JsonHttpResult<ApiResponse<object, List<ApiError>>>>(result);
    }

    [Fact]
    public async Task AtualizarTagAsync_DeveRetornarNotFound_QuandoNotFoundException()
    {
        var service = new Mock<ITagService>();
        var logger = new Mock<ILogger<TagDto>>();
        var tagDto = _fixture.Create<TagDto>();
        service.Setup(s => s.AtualizarTagAsync(It.IsAny<int>(), tagDto)).ThrowsAsync(new NotFoundException());

        var result = await TagController.AtualizarTagAsync(logger.Object, service.Object, 1, tagDto);

        Assert.IsType<JsonHttpResult<ApiResponse<object, List<ApiError>>>>(result);
    }

    [Fact]
    public async Task AtualizarTagAsync_DeveRetornarBadRequest_QuandoException()
    {
        var service = new Mock<ITagService>();
        var logger = new Mock<ILogger<TagDto>>();
        var tagDto = _fixture.Create<TagDto>();
        service.Setup(s => s.AtualizarTagAsync(It.IsAny<int>(), tagDto)).ThrowsAsync(new Exception());

        var result = await TagController.AtualizarTagAsync(logger.Object, service.Object, 1, tagDto);

        Assert.IsType<JsonHttpResult<ApiResponse<object, List<ApiError>>>>(result);
    }
}