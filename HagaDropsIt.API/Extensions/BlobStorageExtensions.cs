using Azure.Storage.Blobs;
using HagaDropsIt.Shared.Interfaces;


namespace HagaDropsIt.API.Extensions;

public static class BlobStorageEndpointExtensions 
{
    public static IEndpointRouteBuilder MapBlobStorageEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/blobstorage");

        group.MapGet("/{containerName}/{blobName}", GetBlob);
        group.MapPost("/{containerName}/{blobName}", UploadBlob);
        group.MapDelete("/{containerName}/{blobName}", DeleteBlob);
        group.MapGet("/{containerName}", ListBlobs);
        group.MapGet("/{containerName}/exists/{blobName}", BlobExists);
        group.MapPost("/{containerName}", CreateBlobContainer);
        group.MapDelete("/{containerName}", DeleteBlobContainer);
        group.MapGet("/{containerName}/exists", ContainerExists);
        group.MapGet("", ListBlobContainers);
        group.MapGet("/{containerName}/{blobName}/sas", GetBlobSasUrl);

        return app;
    }

    private static async Task<IResult> GetBlob(IBlobStorageServices blobStorage, string containerName, string blobName)
    {
        var result = await blobStorage.GetBlobAsync(containerName, blobName);
        if (result.Success)
        {
            return Results.File(result.Data, "application/octet-stream", blobName);
        }
        return Results.NotFound(result.ErrorMessage);
    }

    private static async Task<IResult> UploadBlob(IBlobStorageServices blobStorage, HttpRequest req, string containerName, string blobName)
    {
        using (var stream = new MemoryStream())
        {
            await req.Body.CopyToAsync(stream);
            stream.Position = 0;
            var result = await blobStorage.UploadBlobAsync(containerName, blobName, stream);
            if (result.Success)
            {
                return Results.Ok(result.Data);
            }
            return Results.BadRequest(result.ErrorMessage);
        }
    }


    private static async Task<IResult> DeleteBlob(IBlobStorageServices blobStorage, string containerName, string blobName)
    {
        var result = await blobStorage.DeleteBlobAsync(containerName, blobName);
        if (result.Success)
        {
            return Results.Ok(result.Data);
        }
        return Results.NotFound(result.ErrorMessage);
    }

    private static async Task<IResult> ListBlobs(IBlobStorageServices blobStorage, string containerName)
    {
        var result = await blobStorage.ListBlobsAsync(containerName);
        if (result.Success)
        {
            return Results.Ok(result.Data);
        }
        return Results.BadRequest(result.ErrorMessage);
    }

    private static async Task<bool> BlobExists(IBlobStorageServices blobStorage, string containerName, string blobName)
    {
        var result = await blobStorage.BlobExistsAsync(containerName, blobName);
        if (result.Success)
        {
            return true;
        }
        return false;
    }
       



    private static async Task<IResult> CreateBlobContainer(IBlobStorageServices blobStorage, string containerName)
    {
        var result = await blobStorage.CreateBlobContainerAsync(containerName);
        if (result.Success)
        {
            return Results.Ok($"Blob container '{containerName}' created successfully.");
        }
        return Results.BadRequest(result.ErrorMessage);
    }

    private static async Task<IResult> DeleteBlobContainer(IBlobStorageServices blobStorage, string containerName)
    {
        var result = await blobStorage.DeleteBlobContainerAsync(containerName);
        if (result.Success)
        {
            return Results.Ok($"Blob container '{containerName}' deleted successfully.");
        }
        return Results.BadRequest(result.ErrorMessage);
    }

    private static async Task<IResult> ContainerExists(IBlobStorageServices blobStorage, string containerName)
    {
        var result = await blobStorage.ContainerExistsAsync(containerName);
        if (result.Success)
        {
            return result.Data ? Results.Ok($"Blob container '{containerName}' exists.") : Results.NotFound($"Blob container '{containerName}' does not exist.");
        }
        return Results.BadRequest(result.ErrorMessage);
    }

    private static async Task<IResult> ListBlobContainers(IBlobStorageServices blobStorage)
    {
        var result = await blobStorage.ListBlobContainersAsync();
        if (result.Success)
        {
            return Results.Ok(result.Data);
        }
        return Results.BadRequest(result.ErrorMessage);
    }

    private static async Task<IResult> GetBlobSasUrl(IBlobStorageServices blobStorage, string containerName, string blobName)
    {
        
        var result = await blobStorage.GetBlobSasUrl(containerName, blobName);

      
        if (result.Success)
        {
            return Results.Ok(result.Data);  
        }
        else
        {
            return Results.BadRequest(result.ErrorMessage); 
        }
    }





}

