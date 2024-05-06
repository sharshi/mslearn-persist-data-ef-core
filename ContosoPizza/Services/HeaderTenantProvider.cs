public class HeaderTenantProvider : ITenantProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public HeaderTenantProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int GetTenantId()
    {
        var headers = _httpContextAccessor.HttpContext?.Request?.Headers;

        if (headers == null || !headers.ContainsKey("tenant_id"))
        {
            throw new Exception("Tenant ID not found in headers.");
        }

        var tenantIdHeader = headers["tenant_id"].ToString();

        if (!int.TryParse(tenantIdHeader, out var tenantId))
        {
            throw new Exception("Invalid tenant ID.");
        }

        return tenantId;
    }
}
