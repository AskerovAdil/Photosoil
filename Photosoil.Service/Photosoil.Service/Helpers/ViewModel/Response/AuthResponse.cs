using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Photosoil.Service.Helpers.ViewModel.Response
{
    public class AuthResponse
    {
        public string Name{ get; set; }
        public string Role{ get; set; }

        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public string DeadTime { get; set; }

}
}
