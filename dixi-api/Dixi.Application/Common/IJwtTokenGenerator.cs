using Dixi.Domain.Entities;

namespace Dixi.Application.Common;

public interface IJwtTokenGenerator
{
    string GenerateToken(AppUser user);
}