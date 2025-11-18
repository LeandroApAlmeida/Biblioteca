using Library.Services.Authentication;
using Library.Services.Model.Dto;
using Library.Services.Session;
using Library.Services.User;
using Library.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.InteropServices;


namespace Library.Controllers {


    /// <summary>
    /// Controlador para gerenciamento de login.
    /// </summary>
    public class LoginController(ILoginService loginService, ISessionService sessionService,
    IUserService userService) : Controller {

        
        /// <summary> Objeto para gerenciamento do login. </summary>
        private readonly ILoginService _loginService = loginService;

        /// <summary> Objeto para gerenciamento de sessão do usuário. </summary>
        private readonly ISessionService _sessionService = sessionService;

        /// <summary> Objeto para manutenção de usuários. </summary>
        private readonly IUserService _userService = userService;


        /// <summary>
        /// Retornar a página de login.
        /// </summary>
        /// <returns>Página de login.</returns>
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


        /// <summary>
        /// Função da API do Windows que executa um protocolo ARP no roteador para obter informações
        /// sobre o host remoto conectado.
        /// </summary>
        /// <param name="destIP">IP de destino, obtido da conexão.</param>
        /// <param name="srcIP">IP de origem do host remoto.</param>
        /// <param name="macAddr">Número MAC do host remoto.</param>
        /// <param name="macAddrLen">Comprimento do número MAC do host remoto.</param>
        /// <returns></returns>
        [DllImport("iphlpapi.dll", ExactSpelling = true)]
        public static extern int SendARP(int destIP, int srcIP, byte[] macAddr, ref uint macAddrLen);

        /// <summary>
        /// Fazer o login para iniciar uma sessão de usuário. Neste processo, registra os dados
        /// sobre o host remoto que se conectou para o login.
        /// </summary>
        /// <param name="login">Dados para o login.</param>
        /// <returns>Página de redirecionamento.</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto login) {

            if (ModelState.IsValid) {

                string hostMetadata;

                try {

                    // Obtém o endereço IP do host remoto.
                    IPAddress? remoteIp = HttpContext.Connection.RemoteIpAddress ?? 
                    throw new Exception("Endereço IP não encontrado");

                    if (!IPAddress.IsLoopback(remoteIp!)) {

                        // O endereço do host é diferente do loopback (127.0.0.1).

                        // Endereço IP do host remoto.
                        var hostIp = remoteIp.ToString(); 
                        
                        // Porta do host remoto.
                        var hostPort = HttpContext.Connection.RemotePort;

                        // Número MAC do host remoto.
                        var hostMac = "";

                        IPAddress ipAddress = IPAddress.Parse(hostIp);
                        byte[] macAddr = new byte[6];
                        uint macAddrLen = (uint)macAddr.Length;

                        // Executa o protocolo ARP para obter o MAC do host remoto.
                        int result = SendARP((int)ipAddress.Address, 0, macAddr, ref macAddrLen);

                        if (result == 0) hostMac = string.Join(
                            "-", macAddr.Take((int)macAddrLen).Select(b => b.ToString("X2"))
                        );

                        hostMetadata = hostIp + ":" + hostPort.ToString() + "#" + hostMac;

                    } else {

                        // O endereço do host é do loopback (127.0.0.1).

                        hostMetadata = "[localhost]";
                    
                    }

                } catch (Exception ex) {

                    hostMetadata = "[Erro: " + ex.Message + "]";

                }
                
                // Faz o login, passando os dados da conexão para gerar o log de sessão.
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


        /// <summary>
        /// Fazer o logout, encerrando a sessão de usuário.
        /// </summary>
        /// <returns>Página de redirecionamento.</returns>
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