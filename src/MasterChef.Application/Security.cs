using System.Text;

namespace MasterChef.Application
{
    public static class Security
    {
        public static byte[] GetKey()
        {
            return Encoding.UTF8.GetBytes("the secret that needs to be at least 16 characeters long for HmacSha256");
        }
    }
}
