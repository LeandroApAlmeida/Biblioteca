using Library.Services.Authentication;
using Library.Services.Model.Dto;
using Library.Services.Session;
using Library.Services.User;
using Library.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.InteropServices;


namespace Library.Controllers {


    public class LoginController : Controller {

        
        private readonly ILoginService _loginService;

        private readonly ISessionService _sessionService;

        private readonly IUserService _userService;


        public LoginController(ILoginService loginService, ISessionService sessionService,
        IUserService userService) {
            _loginService = loginService;
            _sessionService = sessionService;
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> Login() {

            if (_sessionService.IsSessionActive()) {
                return RedirectToAction("Index", "Home");
            }

            var registeredAdminResp = await _userService.RegisteredAdmin();

            if (registeredAdminResp.Successful) {
                ViewBag.RegisteredAdmin = registeredAdminResp.Data;
            } else {
                ViewBag.RegisteredAdmin = true;
                TempData[Constants.ERROR_MESSAGE] = registeredAdminResp.Message;
            }

            return View();
        }


        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(int destIP, int srcIP, byte[] macAddr, ref uint macAddrLen);

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto login) {

            if (ModelState.IsValid) {

                string hostMetadata;

                try {

                    IPAddress? remoteIp = HttpContext.Connection.RemoteIpAddress ?? 
                    throw new Exception("Endereço IP não encontrado");

                    if (!IPAddress.IsLoopback(remoteIp!)) {

                        var hostIp = remoteIp.ToString();
                        
                        var hostPort = HttpContext.Connection.RemotePort;

                        var hostMac = "";

                        IPAddress ipAddress = IPAddress.Parse(hostIp);
                        byte[] macAddr = new byte[6];
                        uint macAddrLen = (uint)macAddr.Length;

                        int result = SendARP((int)ipAddress.Address, 0, macAddr, ref macAddrLen);

                        if (result == 0)
                            hostMac = string.Join(
                            "-", macAddr.Take((int)macAddrLen).Select(b => b.ToString("X2"))
                        );

                        hostMetadata = hostIp + ":" + hostPort.ToString() + "#" + hostMac;

                    } else {

                        hostMetadata = "[localhost]";
                    
                    }

                } catch (Exception ex) {

                    hostMetadata = "[Erro: " + ex.Message + "]";

                }
                 
                var loginResp = await _loginService.Login(login, hostMetadata);

                if (loginResp.Successful) {

                    if (loginResp.Data != null) {

                        return RedirectToAction("Index", "Home");

                    } else {

                        TempData[Constants.ERROR_MESSAGE] = loginResp.Message;

                        return RedirectToAction("Login");

                    }

                } else {

                    TempData[Constants.ERROR_MESSAGE] = loginResp.Message;

                    return RedirectToAction("Login");

                }

            } else {

                TempData[Constants.ERROR_MESSAGE] = "Credenciais inválidas!";

                return RedirectToAction("Login");

            }

        }


        [HttpPost]
        public async Task<IActionResult> Logout() {

            if (!_sessionService.IsSessionActive()) {
                return RedirectToAction("Login", "Login");
            }

            var logoutResp = await _loginService.Logout();

            if (logoutResp.Successful) {

                return RedirectToAction("Login");

            } else {

                TempData[Constants.ERROR_MESSAGE] += logoutResp.Message;

                return RedirectToAction("Index", "Home");

            }

        }


    }


}