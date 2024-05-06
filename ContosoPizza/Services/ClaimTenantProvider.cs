public class ClaimTenantProvider : ITenantProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ClaimTenantProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int GetTenantId()
    {
        var tenantIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("tenant_id");

        if (tenantIdClaim == null)
        {
            throw new Exception("Tenant ID not found in user claims.");
        }

        if (!int.TryParse(tenantIdClaim.Value, out var tenantId))
        {
            throw new Exception("Invalid tenant ID.");
        }

        return tenantId;
    }
}
