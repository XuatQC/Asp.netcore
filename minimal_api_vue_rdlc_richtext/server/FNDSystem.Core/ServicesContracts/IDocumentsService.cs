using FNDSystem.Core.Domain.Entities;
using FNDSystem.Core.Dto;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace FNDSystem.Core.ServicesContracts;

public interface IDocumentsService
{
    StringBuilder GetWordFileContent(string filePath);
}