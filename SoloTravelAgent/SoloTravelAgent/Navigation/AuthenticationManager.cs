

using SoloTravelAgent.Model.Entities;

namespace SoloTravelAgent.Navigation
{
    public static class AuthenticationManager
    {
        public static User CurrentUser { get; set; }

        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}
