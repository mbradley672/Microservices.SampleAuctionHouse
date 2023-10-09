using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

namespace IdentityService.Pages.Diagnostics;

[SecurityHeaders]
[Authorize]
public class Index : PageModel
{
    public ViewModel View { get; set; }
        
    public async Task<IActionResult> OnGet() {
        var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
        var localAddresses = new string[] { "127.0.0.1", "::1", HttpContext.Connection.LocalIpAddress?.ToString() };

        foreach (var @interface in networkInterfaces)
        {
            if (@interface.NetworkInterfaceType == NetworkInterfaceType.Loopback) continue;

            var ipProperties = @interface.GetIPProperties();
            var ipv4AddressInfo =
                ipProperties.UnicastAddresses.Where(ua => ua.Address.AddressFamily == AddressFamily.InterNetwork);

            localAddresses = ipv4AddressInfo.Aggregate(localAddresses, (current, addressInformation) => current.Concat(new[] { $"::ffff:{addressInformation.Address.ToString().Substring(0,addressInformation.Address.ToString().Length - 1)}1" }).ToArray());
        }
        if (!localAddresses.Contains(HttpContext.Connection.RemoteIpAddress?.ToString()))
        {
            return NotFound();
        }

        View = new ViewModel(await HttpContext.AuthenticateAsync());
            
        return Page();
    }
}