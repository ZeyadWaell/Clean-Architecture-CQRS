namespace ChatApp.Routes
{
    public abstract class BaseRoute
    {
        public const string Base = "api/v1";
    }

    public class AccountRoutes : BaseRoute
    {
        public const string Controller = Base + "/account";
        public const string Login =  "login";
        public const string Register = "register";  
    }

    public class ChatRoutes : BaseRoute
    {
        public const string Controller = Base + "/chat";
        public const string SendMessage = "send";

        public const string EditMessage = "edit";
        public const string DeleteMessage = "delete";
        public const string GetMessages = "messages";
    }

    public class  ChatRoomsRoutes : BaseRoute
    {
        public const string Controller = Base + "/chatRooms";
        public const string JoinRoom = "join";
        public const string LeaveRoom = "leave";
        public const string GetAllRoom = "getall";


    }
    public class RoomRoutes : BaseRoute
    {
        public const string Controller = Base + "/room";
    }

    public class TeamRoutes : BaseRoute
    {
        public const string Controller = Base + "/team";
    }

    public class ChatHubRoutes : BaseRoute
    {
        public const string Hub = "chathub";
    }
}
