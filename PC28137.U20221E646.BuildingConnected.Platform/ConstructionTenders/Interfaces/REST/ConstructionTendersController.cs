using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Domain.Services;
using PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Interfaces.REST.Resources;
using PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Interfaces.REST.Transform;
using PC28137.U20221E646.BuildingConnected.Platform.Shared.Infrastructure.Localization;

namespace PC28137.U20221E646.BuildingConnected.Platform.ConstructionTenders.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
public class ConstructionTendersController(IConstructionTenderCommandService commandService)
    : ControllerBase
{
    /// <summary>Registers a construction tender.</summary>
    /// <remarks>Returns the tender with the identifier generated for it.</remarks>
    /// <param name="resource">Data submitted to register the tender.</param>
    /// <param name="cancellationToken">Token that cancels the operation.</param>
    /// <returns>The registered tender, or an error describing why it was rejected.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ConstructionTenderResource))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateConstructionTender(
        [FromBody] CreateConstructionTenderResource resource, CancellationToken cancellationToken)
    {
        var command = CreateConstructionTenderCommandFromResourceAssembler.ToCommandFromResource(resource);
        var tender = await commandService.Handle(command, cancellationToken);

        if (tender is null)
            return Conflict(new { message = MessageCatalog.Get(MessageKey.TenderAlreadyExists) });

        var created = ConstructionTenderResourceFromEntityAssembler.ToResourceFromEntity(tender);
        return StatusCode(StatusCodes.Status201Created, created);
    }
}