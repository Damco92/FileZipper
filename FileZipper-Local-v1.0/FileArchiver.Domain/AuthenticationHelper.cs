using System;
using System.DirectoryServices.AccountManagement;

namespace FileArchiver.Domain
{
    public static class AuthenticationHelper
    {
        public static AuthenticationResult AuthenticateDomain(string username, string password)
        {
            AuthenticationResult result = new AuthenticationResult();
            try
            {
                using (PrincipalContext pc = new PrincipalContext(ContextType.Domain, "QUIPU"))
                {
                    result.IsValidLogIn = pc.ValidateCredentials(username, password, ContextOptions.Negotiate);
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }

    public class AuthenticationResult
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool IsValidLogIn { get; set; }
    }
}
