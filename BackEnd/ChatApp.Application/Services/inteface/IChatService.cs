using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatApp.Application.CQRS.Requests.Chat.Models;
using ChatApp.Application.CQRS.ChatMessage.Commands.Models;
using ChatApp.Application.CQRS.ChatMessage.Commands.Response;
using ChatApp.Application.CQRS.ChatMessage.Queries.Response;
using ChatApp.Application.CQRS.ChatRoom.Commands.Models;
using ChatApp.Application.CQRS.ChatRoom.Queries.Models;
using ChatApp.Application.CQRS.ChatRoom.Queries.Response;
using ChatApp.Application.Utilities;
using ChatApp.Application.Utilities.Class;

namespace ChatApp.Application.Services.inteface
{
    public interface IChatService
    {
        Task<ApiResponse<ChatMessageResponse>> SendMessageAsync(SendMessageRequest request);
        Task<ApiResponse<EditMessageResponse>> EditMessageAsync(EditMessageRequest request);
        Task<ApiResponse<DeleteMessageResponse>> DeleteMessageAsync(DeleteMessageRequest request);
        Task<ApiResponse<IList<ChatMessageResponse>>> GetChatRoomMessagesAsync(Guid chatRoomId);
        Task<string> JoinRoomAsync(JoinRoomRequest request);
        Task<ApiResponse<ChatRoomResponse>> LeaveRoomAsync(LeaveRoomRequest request);
        Task<ApiResponse<List<ChatRoomAllResponse>>> GetAllRoomAsync(GetAllRoomsRequest request);
    }
}
