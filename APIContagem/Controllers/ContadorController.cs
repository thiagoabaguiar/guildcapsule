using System;
using System.Reflection;
using System.Runtime.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
/* isso é um comentário */
namespace APIContagem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContadorController : ControllerBase
    {
        private static Contador _CONTADOR = new Contador();

        [HttpGet]
        public object Get([FromServices]IConfiguration configuration)
        {
            lock (_CONTADOR)
            {
                _CONTADOR.Incrementar();

                return new
                {
                    _CONTADOR.ValorAtual,
                    Environment.MachineName,
                    Local = "eu podia tá matando, eu podia tá robano, mas to aqui rodano...",
                    Sistema = Environment.OSVersion.VersionString,
                    Variavel = configuration["TesteAmbiente"],
                    TargetFramework = Assembly
                        .GetEntryAssembly()?
                        .GetCustomAttribute<TargetFrameworkAttribute>()?
                        .FrameworkName
                };
            }
        }
    }
}
